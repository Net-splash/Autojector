using Autojector.Base;
using Autojector.Registers.Base;
using Autojector.Registers.Configs.TypeOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.Configs.ImplementationVersions;

internal class UnimplementedInterfaceConfigVersion : BaseConfigImplementationVersion
{
    public UnimplementedInterfaceConfigVersion(IEnumerable<Type> interfacesFromAssemblies, IConfigRegisterStrategy configRegisterStrategy) : base(interfacesFromAssemblies, configRegisterStrategy)
    {
    }

    public override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var interfaceFromExtension = TypesFromAssemblies.Where(
          type => type.GetInterfacesFromTree(t => t == ConfigType).Any()
       ).Select(configType => new InterfaceConfigTypeOperator(configType, ConfigRegisterStrategy));

        var interfacesFromAttribute = TypesFromAssemblies.Select(
          type => new ConfigTypeWithAttribute(type)
        ).Where(config => config.HasImplementedLifetypeAttributes)
        .Select(config => new InterfaceConfigTypeOperator(config.Type, ConfigRegisterStrategy, config.AttributeKey));


        return Enumerable.Empty<ITypeConfigurator>()
            .Concat(interfaceFromExtension)
            .Concat(interfacesFromAttribute);
    }
}