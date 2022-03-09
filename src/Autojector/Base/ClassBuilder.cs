using Autojector.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Autojector.Registers.Base;

internal record ClassBuilder(Type InterfaceType)
{
    private static ModuleBuilder _moduleBuilder;
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
    public Type BuildType()
    {
        var typeName = $"{ Constants.AutojectorPrefixKey}{InterfaceType.Name.RemoveInterfacePrefix()}_{Guid.NewGuid():N}";
        var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public);

        typeBuilder.AddInterfaceImplementation(InterfaceType);
        typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

        var properties = InterfaceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        BuildProperties(typeBuilder, properties);

        return typeBuilder.CreateType();

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

        if (property.GetCustomAttribute<NotNullAttribute>() != null)
        {
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
        }
        ilGenerator.Emit(OpCodes.Ret);

        return getterBuilder;
    }

    private static MethodBuilder BuildSetter(TypeBuilder typeBuilder, PropertyInfo property, FieldBuilder fieldBuilder, MethodAttributes attributes)
    {
        var setterBuilder = typeBuilder.DefineMethod($"set_{property.Name}", attributes, null, new Type[] { property.PropertyType });
        var ilGenerator = setterBuilder.GetILGenerator();

        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldarg_1);

        if (property.GetCustomAttribute<NotNullAttribute>() != null)
        {
            var isValueNull = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Dup);
            ilGenerator.Emit(OpCodes.Brtrue_S, isValueNull);
            ilGenerator.Emit(OpCodes.Pop);
            ilGenerator.Emit(OpCodes.Ldstr, property.Name);

            var argumentNullExceptionConstructor = typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) });
            ilGenerator.Emit(OpCodes.Newobj, argumentNullExceptionConstructor);
            ilGenerator.Emit(OpCodes.Throw);

            ilGenerator.MarkLabel(isValueNull);
        }
        ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
        ilGenerator.Emit(OpCodes.Ret);

        return setterBuilder;
    }
}