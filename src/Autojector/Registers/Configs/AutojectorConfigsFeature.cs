
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Registers.Configs;
internal class AutojectorConfigsFeature : BaseAutojectorFeature
{
    private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Configs;
    public AutojectorConfigsFeature(IEnumerable<Assembly> assemblies, IServiceCollection services) :
        base(assemblies, services)
    {
        ConfigRegisterStrategy = new ConfigRegisterStrategy(Services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var configs = NonAbstractClassesFromAssemblies.Where(c => c.GetInterfaces().Any(t => t == ConfigType));
        var configConfigurators = configs.Select(c => new ConfigTypeOperator(c, ConfigRegisterStrategy));
        return configConfigurators;
    }
}
