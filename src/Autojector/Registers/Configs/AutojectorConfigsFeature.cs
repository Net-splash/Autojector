using Autojector.Base;
using Autojector.Registers.Configs.ImplementationVersions;
using System.Collections.Generic;
using System.Reflection;

namespace Autojector.Registers.Configs;
internal class AutojectorConfigsFeature : BaseVersionedAutojectorFeature
{
    private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Configs;
    public AutojectorConfigsFeature(
        IEnumerable<Assembly> assemblies,
        IConfigRegisterStrategy configRegisterStrategy
        ) : base(assemblies)
    {
        ConfigRegisterStrategy = configRegisterStrategy;
    }

    protected override IEnumerable<IFeatureImplementationVersion> GetImplementationVersions()
    {
        return new List<IFeatureImplementationVersion>()
        {
            new ClassConfigVersion(NonAbstractClassesFromAssemblies,ConfigRegisterStrategy),
            new AttributeConfigVersion(NonAbstractClassesFromAssemblies,ConfigRegisterStrategy),
            new UnimplementedInterfaceConfigVersion(InterfacesFromAssemblies,ConfigRegisterStrategy)
        };
    }
}
