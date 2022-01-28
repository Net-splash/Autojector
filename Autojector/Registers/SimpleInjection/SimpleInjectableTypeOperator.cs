using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.SimpleInjection;
internal record SimpleInjectableTypeOperator(Type Type, ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory) : 
    BaseTypeOperator(Type), ITypeConfigurator
{
    public void ConfigureServices()
    {
        var injectableTypes = GetInjectableInterfaces();

        ValidateUnknownInjectableType(injectableTypes);
        ValidateNotImplementedInterface(injectableTypes);


        foreach (var injectableType in injectableTypes)
        {
            var injectableInterface = injectableType.GetGenericArguments().First();
            var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(injectableType.GetGenericTypeDefinition());
            registerStrategy.Add(injectableInterface, Type);
        }
    }

    private IEnumerable<Type> GetInjectableInterfaces()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i =>
                i.IsGenericType &&
                SimpleLifetypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }

    private void ValidateNotImplementedInterface(IEnumerable<Type> lifetypeManagementInterfaces)
    {
        var customInterface = Type.GetInterfaces()
            .Where(i => !InjectableInterfaces.Contains(i) &&
                        !SimpleLifetypeInterfaces.Contains(i))
            .Concat(new Type[] { Type });

        var customInterfaceFromLifeType = lifetypeManagementInterfaces.Select(i => i.GetGenericArguments().First());
        var notInterfacesByClass = customInterfaceFromLifeType.Except(customInterface);
        if (notInterfacesByClass.Any())
        {
            var interfacesNames = notInterfacesByClass.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($"The interfaces {interfacesListNames} are not implemented by {Type} but are registered as injectable");
        }
    }

    private void ValidateUnknownInjectableType(IEnumerable<Type> injectableInterface)
    {
        if (!injectableInterface.Any())
        {
            var lifetypeInterfacesNames = SimpleLifetypeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {Type.Name} doesn't implement a LifeType interface
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }
    }
}
