using Autojector.Base;
using Autojector.Features.SimpleInjection.ImplementationVersion;
using Autojector.Registers;
using Autojector.Registers.SimpleInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Autojector.Features.SimpleInjection;

internal class AutojectorSimpleInjectionFeature : BaseVersionedAutojectorFeature
{
    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.SimpleInjection;

    public AutojectorSimpleInjectionFeature(
        IEnumerable<Assembly> assemblies, 
        ISimpleRegisterStrategyFactory registerStrategyFactory
        ) : base(assemblies)
    {
        RegisterStrategyFactory = registerStrategyFactory;
    }

    protected override IEnumerable<IFeatureImplementationVersion> GetImplementationVersions()
    {
        return new List<IFeatureImplementationVersion>
        {
            new SimpleInjectedInterfaceVersion(RegisterStrategyFactory,NonAbstractClassesFromAssemblies),
            new SimpleInjectedAttributeVersion(RegisterStrategyFactory,NonAbstractClassesFromAssemblies)
        };
    }
}
