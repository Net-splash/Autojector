using Autojector.Base;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.SimpleInjection;
internal record SimpleInjectableTypeOperator(Type Type, IEnumerable<Type> ImplementedGenericLifetypeInterface, ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory) : 
    BaseTypeOperator(Type), ITypeConfigurator
{
    private IEnumerable<Type> ImplementedLifetypeInterface => Type.GetInterfacesFromTree(x => x.IsGenericType);
    public void ConfigureServices()
    {
        ValidateUnknownInjectableType();
        ValidateNotImplementedInterface();
        foreach (var injectableType in ImplementedLifetypeInterface)
        {
            var injectableInterface = injectableType.GetGenericArguments().First();
            var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(injectableType.GetGenericTypeDefinition());
            registerStrategy.Add(injectableInterface, Type);
        }
    }

    private void ValidateNotImplementedInterface()
    {
        var customInterface = Type.GetInterfaces()
            .Where(i => !SimpleLifetypeInterfaces.Contains(i))
            .Concat(new Type[] { Type });

        var customInterfaceFromLifeType = ImplementedLifetypeInterface.Select(i => i.GetGenericArguments().First());
        var nonImplementedInterfaceFromLifetype = customInterfaceFromLifeType.Except(customInterface);
        if (nonImplementedInterfaceFromLifetype.Any())
        {
            var interfacesNames = nonImplementedInterfaceFromLifetype.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($"The interfaces {interfacesListNames} are not implemented by {Type} but are registered as injectable");
        }
    }

    private void ValidateUnknownInjectableType()
    {
        if (!ImplementedGenericLifetypeInterface.Any())
        {
            var lifetypeInterfacesNames = SimpleLifetypeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {Type.Name} doesn't implement a LifeType interface.
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }
    }
}
