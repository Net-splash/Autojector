using Autojector.Base;
using Autojector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;
namespace Autojector.Registers.Configs;
internal class AutojectorConfigsByInterfaceFeature : BaseAutojectorFeature
{
    private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Configs;
    public AutojectorConfigsByInterfaceFeature(
        IEnumerable<Assembly> assemblies,
        IConfigRegisterStrategy configRegisterStrategy
        ) : base(assemblies)
    {
        ConfigRegisterStrategy = configRegisterStrategy;
    }

    private bool ExtendsConfigType(Type type)
    {
        return type.GetInterfacesFromTree(t => t == ConfigType).Any();
    }
    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var configClasses = NonAbstractClassesFromAssemblies.Where(ExtendsConfigType);

        var configConfigurators = configClasses
            .Select(configType => new ConfigTypeOperator(configType, ConfigRegisterStrategy));

        return configConfigurators;
    }
}
