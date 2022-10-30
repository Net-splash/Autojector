using Autojector.Base;
using Autojector.DependencyInjector.Public;
using System;

namespace Autojector.Features.Configs
{
    internal class ConfigTypeOperator : ITypeConfigurator
    {
        public ConfigTypeOperator(Type config, IConfigRegisterStrategy configRegisterStrategy, string key = null)
        {
            Config = config;
            ConfigRegisterStrategy = configRegisterStrategy;
            Key = key;
        }

        public Type Config { get; }
        public IConfigRegisterStrategy ConfigRegisterStrategy { get; }
        public string Key { get; }

        public void ConfigureServices() => ConfigRegisterStrategy.Add(Config, Config, new string[]
        {
            Key
        });
    }
}