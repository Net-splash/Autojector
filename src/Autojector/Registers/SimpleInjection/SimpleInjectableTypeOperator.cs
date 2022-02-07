using Autojector.Base;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.SimpleInjection;
internal record SimpleInjectableTypeOperator(Type Type, IEnumerable<Type> ImplementedGenericLifetypeInterfaces, ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory) : 
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        ValidateUnknownLifetype();
        ValidateNotImplementedInterface();
        foreach (var injectableType in ImplementedGenericLifetypeInterfaces)
        {
            ValidateOnlyOneGenericArgument(injectableType);
            
            var lifetimeType = injectableType.GetGenericTypeDefinition();
            var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(lifetimeType);
            
            var injectableInterface = injectableType.GetGenericArguments().First();
            registerStrategy.Add(injectableInterface, Type);
        }
    }

    private void ValidateNotImplementedInterface()
    {
        var customInterface = Type.GetInterfaces()
            .Where(i => !SimpleLifetypeInterfaces.Contains(i))
            .Concat(new Type[] { Type });

        var customInterfaceFromLifeType = ImplementedGenericLifetypeInterfaces.Select(i => i.GetGenericArguments().First());
        var nonImplementedInterfaceFromLifetype = customInterfaceFromLifeType.Except(customInterface);
        if (nonImplementedInterfaceFromLifetype.Any())
        {
            var interfacesNames = nonImplementedInterfaceFromLifetype.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($"The interfaces {interfacesListNames} are not implemented by {Type} but are registered as injectable");
        }
    }

    private void ValidateOnlyOneGenericArgument(Type injectableType)
    {
        if (injectableType.GetGenericArguments().HasMany())
        {
            throw new InvalidOperationException("Can not have more than one argument in a simple life type interface implementation");
        }
    }

    private void ValidateUnknownLifetype()
    {
        if (!ImplementedGenericLifetypeInterfaces.Any())
        {
            var lifetypeInterfacesNames = SimpleLifetypeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {Type.Name} doesn't implement a LifeType interface.
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }
    }
}
