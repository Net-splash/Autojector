using Autojector.Base;
using Autojector.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Autojector.Base
{
    internal class ModelClassBuilder
    {
        private static ModuleBuilder _moduleBuilder;

        public ModelClassBuilder(Type InterfaceType)
        {
            this.InterfaceType = InterfaceType;
        }

        private static ModuleBuilder ModuleBuilder
        {
            get
            {
                if (_moduleBuilder == null)
                {

                    var typeSignature = Constants.AutojectorDynamicTypesKey;
                    var assemblyName = new AssemblyName(typeSignature);
                    var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                    var moduleBuilder = assemblyBuilder.DefineDynamicModule(Constants.AutojectorGeneratedModuleKey);

                    _moduleBuilder = moduleBuilder;
                }

                return _moduleBuilder;
            }
        }

        public Type InterfaceType { get; }

        public Type BuildType()
        {
            var properties = InterfaceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            ValidateAgainstMethods();
            var typeName = $"{ Constants.AutojectorPrefixKey}{InterfaceType.Name.RemoveInterfacePrefix()}{Guid.NewGuid()}";
            var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public);

            typeBuilder.AddInterfaceImplementation(InterfaceType);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);


            BuildProperties(typeBuilder, properties);

            var typeInfo = typeBuilder.CreateTypeInfo();
            return typeInfo.AsType();
        }

        private void ValidateAgainstMethods()
        {
            var methods = InterfaceType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => !m.IsSpecialName);
            if (methods.Any())
            {
                var methodNames = methods.Select(m => m.Name);
                var joinedMethodNames = string.Join(",", methodNames);
                throw new InvalidOperationException($@"
                        The Autojector class generator is unable to create implement methods. 
                        Please try removing the methods from your code : {joinedMethodNames}");
            }
        }

        private static void BuildProperties(TypeBuilder typeBuilder, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                BuildProperty(typeBuilder, property);
            }
        }

        private static PropertyBuilder BuildProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldName = $"<{property.Name}>k__BackingField";

            var propertyBuilder = typeBuilder.DefineProperty(property.Name, System.Reflection.PropertyAttributes.None, property.PropertyType, Type.EmptyTypes);

            // Build backing-field.
            var fieldBuilder = typeBuilder.DefineField(fieldName, property.PropertyType, FieldAttributes.Private);

            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;

            var getterBuilder = BuildGetter(typeBuilder, property, fieldBuilder, getSetAttributes);
            var setterBuilder = BuildSetter(typeBuilder, property, fieldBuilder, getSetAttributes);

            propertyBuilder.SetGetMethod(getterBuilder);
            propertyBuilder.SetSetMethod(setterBuilder);

            return propertyBuilder;
        }

        private static MethodBuilder BuildGetter(TypeBuilder typeBuilder, PropertyInfo property, FieldBuilder fieldBuilder, MethodAttributes attributes)
        {
            var getterBuilder = typeBuilder.DefineMethod($"get_{property.Name}", attributes, property.PropertyType, Type.EmptyTypes);
            var ilGenerator = getterBuilder.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, fieldBuilder);

            // Build null check
            ilGenerator.Emit(OpCodes.Dup);

            var isFieldNull = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Brtrue_S, isFieldNull);
            ilGenerator.Emit(OpCodes.Pop);
            ilGenerator.Emit(OpCodes.Ldstr, $"{property.Name} isn't set.");

            var invalidOperationExceptionConstructor = typeof(InvalidOperationException).GetConstructor(new Type[] { typeof(string) });
            ilGenerator.Emit(OpCodes.Newobj, invalidOperationExceptionConstructor);
            ilGenerator.Emit(OpCodes.Throw);

            ilGenerator.MarkLabel(isFieldNull);
            ilGenerator.Emit(OpCodes.Ret);

            return getterBuilder;
        }

        private static MethodBuilder BuildSetter(TypeBuilder typeBuilder, PropertyInfo property, FieldBuilder fieldBuilder, MethodAttributes attributes)
        {
            var setterBuilder = typeBuilder.DefineMethod($"set_{property.Name}", attributes, null, new Type[] { property.PropertyType });
            var ilGenerator = setterBuilder.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);

            var isValueNull = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Dup);
            ilGenerator.Emit(OpCodes.Brtrue_S, isValueNull);
            ilGenerator.Emit(OpCodes.Pop);
            ilGenerator.Emit(OpCodes.Ldstr, property.Name);

            var argumentNullExceptionConstructor = typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) });
            ilGenerator.Emit(OpCodes.Newobj, argumentNullExceptionConstructor);
            ilGenerator.Emit(OpCodes.Throw);

            ilGenerator.MarkLabel(isValueNull);
            ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            ilGenerator.Emit(OpCodes.Ret);

            return setterBuilder;
        }
    }
}
