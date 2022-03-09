using Autojector.Abstractions;
using Autojector.Base;
using Autojector.Registers.Base;
using Autojector.Registers.SimpleInjection;
using Autojector.Registers.SimpleInjection.Operators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Features.SimpleInjection.ImplementationVersion;

internal class SimpleInjectedAttributeVersion : IFeatureImplementationVersion
{
    
    protected IEnumerable<Type> NonAbstractClassesFromAssemblies { get; }
    public SimpleInjectedAttributeVersion(
        ISimpleRegisterStrategyFactory registerStrategyFactory,
        IEnumerable<Type> nonAbstractClassesFromAssemblies)
    {
        RegisterStrategyFactory = registerStrategyFactory;
        NonAbstractClassesFromAssemblies = nonAbstractClassesFromAssemblies;
    }

    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }

    public IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        return NonAbstractClassesFromAssemblies
            .Select(type => new TypeWithAttributes<BaseInjectionAttribute>(type))
            .Where(type => type.HasImplementedLifetypeAttributes)
            .Select(pair => new SimpleAttributeTypeOperator(
                pair.Type,
                pair.Attributes,
                RegisterStrategyFactory
            ));
    }
}