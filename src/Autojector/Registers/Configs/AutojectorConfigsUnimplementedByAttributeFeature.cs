using Autojector.Base;
using Autojector.Registers.Configs.TypeOperators;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Autojector.Registers.Configs;

internal class AutojectorConfigsUnimplementedByAttributeFeature : BaseAutojectorFeature
{
    private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Configs;
    public AutojectorConfigsUnimplementedByAttributeFeature(
        IEnumerable<Assembly> assemblies,
        IConfigRegisterStrategy configRegisterStrategy
        ) : base(assemblies)
    {
        ConfigRegisterStrategy = configRegisterStrategy;
    }
    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var interfacesFromAttribute = InterfacesFromAssemblies.Select(
          type => new ConfigTypeWithAttribute(type)
        ).Where(config => config.HasAttributes)
        .Select(config => new InterfaceConfigTypeOperator(config.Type, ConfigRegisterStrategy, config.AttributeKey));

        return interfacesFromAttribute;
    }
}