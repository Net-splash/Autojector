using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Configs.ImplementationVersions;
internal class AttributeConfigVersion : BaseConfigImplementationVersion
{
    public AttributeConfigVersion(IEnumerable<Type> nonAbstractClassesFromAssemblies, IConfigRegisterStrategy configRegisterStrategy)
        : base(nonAbstractClassesFromAssemblies, configRegisterStrategy)
    {
    }

    public override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var configClasses = TypesFromAssemblies.Select(
           type => new ConfigTypeWithAttribute(type)
        ).Where(config => config.HasImplementedLifetypeAttributes);

        var configConfigurators = configClasses
            .Select(config => new ConfigTypeOperator(config.Type, ConfigRegisterStrategy, config.AttributeKey));

        return configConfigurators;
    }
}
