using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autojector.Base;
using Autojector.Registers;
using Autojector.Abstractions;
using Autojector.Registers.SimpleInjection.Operators;
using Autojector.DependencyInjector.Public;

namespace Autojector.Features.SimpleInjection;

internal class AutojectorSimpleInjectionByAttributeFeature : BaseAutojectorFeature
{
    public AutojectorSimpleInjectionByAttributeFeature(
        IEnumerable<Assembly> assemblies,
        ISimpleRegisterStrategyFactory registerStrategyFactory) : base(assemblies)
    {
        RegisterStrategyFactory = registerStrategyFactory ?? throw new ArgumentNullException(nameof(registerStrategyFactory));
    }

    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.SimpleInjection;

    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        return NonAbstractClassesFromAssemblies
            .Select(type => new TypeWithAttributes<BaseInjectionAttribute>(type))
            .Where(type => type.HasAttributes)
            .Select(pair => new SimpleAttributeTypeOperator(
                pair.Type,
                pair.Attributes,
                RegisterStrategyFactory
            ));
    }
}
