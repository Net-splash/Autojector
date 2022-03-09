using Autojector.Base;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.Configs.ImplementationVersions;

internal class ClassConfigVersion : BaseConfigImplementationVersion
{
    public ClassConfigVersion(IEnumerable<Type> nonAbstractClassesFromAssemblies, IConfigRegisterStrategy configRegisterStrategy)
        : base(nonAbstractClassesFromAssemblies, configRegisterStrategy)
    {
    }

    public override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var configClasses = TypesFromAssemblies.Where(
           type => type.GetInterfacesFromTree(t => t == ConfigType).Any()
        );

        var configConfigurators = configClasses
            .Select(configType => new ConfigTypeOperator(configType, ConfigRegisterStrategy));

        return configConfigurators;
    }
}
