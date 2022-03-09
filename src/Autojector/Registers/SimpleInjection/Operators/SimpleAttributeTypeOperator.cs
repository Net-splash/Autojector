using Autojector.Abstractions;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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
        var lifetypeInterfaces = Attributes.Select(a => a.AbstractionType);
        ValidateUnknownLifetype(lifetypeInterfaces);
        foreach (var attribute in Attributes)
        {
            var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(attribute);
            registerStrategy.Add(attribute.AbstractionType, Type);
        }
    }
}
