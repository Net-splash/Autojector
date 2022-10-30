using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using Autojector.Features.Configs.TypeOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;
namespace Autojector.Features.Configs
{
    internal class AutojectorConfigsUnimplementedByInterfaceFeature : BaseAutojectorFeature
    {
        private IConfigRegisterStrategy ConfigRegisterStrategy { get; }
        public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Configs;
        public AutojectorConfigsUnimplementedByInterfaceFeature(
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
            var interfaceFromExtension = InterfacesFromAssemblies
                .Where(ExtendsConfigType)
                .Select(configType => new InterfaceConfigTypeOperator(configType, ConfigRegisterStrategy));
            return interfaceFromExtension;
        }
    }

}
