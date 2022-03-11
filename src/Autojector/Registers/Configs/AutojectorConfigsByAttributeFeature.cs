using Autojector.Base;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Autojector.Registers.Configs;

internal class AutojectorConfigsByAttributeFeature : BaseAutojectorFeature
{
    private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Configs;
    public AutojectorConfigsByAttributeFeature(
        IEnumerable<Assembly> assemblies,
        IConfigRegisterStrategy configRegisterStrategy
        ) : base(assemblies)
    {
        ConfigRegisterStrategy = configRegisterStrategy;
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var configClasses = NonAbstractClassesFromAssemblies.Select(
           type => new ConfigTypeWithAttribute(type)
        ).Where(config => config.HasAttributes);

        var configConfigurators = configClasses
            .Select(config => new ConfigTypeOperator(config.Type, ConfigRegisterStrategy, config.AttributeKey));

        return configConfigurators;
    }
}
