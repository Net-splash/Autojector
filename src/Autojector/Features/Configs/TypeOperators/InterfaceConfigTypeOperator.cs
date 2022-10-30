using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using System;

namespace Autojector.Features.Configs.TypeOperators
{
    internal class InterfaceConfigTypeOperator : ITypeConfigurator
    {
        public InterfaceConfigTypeOperator(
            Type interfaceType,
            IConfigRegisterStrategy configRegisterStrategy,
            string key = null)
        {
            InterfaceType = interfaceType;
            ConfigRegisterStrategy = configRegisterStrategy;
            Key = key;
        }

        public Type InterfaceType { get; }
        public IConfigRegisterStrategy ConfigRegisterStrategy { get; }
        public string Key { get; }

        public void ConfigureServices()
        {
            var classBuilder = new ModelClassBuilder(InterfaceType);
            var classType = classBuilder.BuildType();
            ConfigRegisterStrategy.Add(InterfaceType, classType, new string[]
            {
                InterfaceType.Name.RemoveInterfacePrefix(),
                Key,
            });
        }
    }
}

