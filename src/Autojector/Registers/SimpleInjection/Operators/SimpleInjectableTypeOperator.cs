using Autojector.Base;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.SimpleInjection.Operators;

internal record SimpleInjectableTypeOperator(
    Type Type, 
    IEnumerable<Type> ImplementedGenericLifetypeInterfaces, 
    ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory) :
    BaseSimpleInjectableOperator(Type),
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        ValidateUnknownLifetype(ImplementedGenericLifetypeInterfaces);
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
        var customInterfaceFromExtension = Type.GetInterfaces()
            .Where(i => !SimpleLifetypeInterfaces.Contains(i))
            .Concat(new Type[] { Type });

        var customInterfaceFromLifeType = ImplementedGenericLifetypeInterfaces.Select(i => i.GetGenericArguments().First());
        var nonImplementedInterfaceFromLifetype = customInterfaceFromLifeType.Except(customInterfaceFromExtension);
        ValidateNotImplementedInterface(nonImplementedInterfaceFromLifetype);
    }


    private static void ValidateOnlyOneGenericArgument(Type injectableType)
    {
        if (injectableType.GetGenericArguments().HasMany())
        {
            throw new InvalidOperationException("Can not have more than one argument in a simple life type interface implementation");
        }
    }
}
