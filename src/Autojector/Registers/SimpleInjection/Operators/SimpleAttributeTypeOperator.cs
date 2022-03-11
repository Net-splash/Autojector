using Autojector.Abstractions;
using Autojector.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.SimpleInjection.Operators;

internal record SimpleAttributeTypeOperator(
    Type Type,
    IEnumerable<BaseInjectionAttribute> Attributes,
    ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory) :
    BaseSimpleInjectableOperator(Type),
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        ValidateTypeAndAttributes();
        foreach (var attribute in Attributes)
        {
            var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(attribute);
            registerStrategy.Add(attribute.AbstractionType, Type);
        }
    }

    private void ValidateTypeAndAttributes()
    {
        var interfacesFromAttributes = Attributes.Select(a => a.AbstractionType);
        ValidateUnknownLifetype(interfacesFromAttributes);

        var customInterfaceFromExtension = Type.GetInterfaces()
          .Where(i => !SimpleLifetypeInterfaces.Contains(i));

        var nonImplementedInterfaceFromLifetype = customInterfaceFromExtension.Except(interfacesFromAttributes);
        ValidateNotImplementedInterface(nonImplementedInterfaceFromLifetype);
    }
}
