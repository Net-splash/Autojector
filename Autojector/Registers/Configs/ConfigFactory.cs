using Microsoft.Extensions.Configuration;
using System;

namespace Autojector.Registers.Configs;
internal class ConfigFactory
{
    public ConfigFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    internal object GetConfig(Type configType)
    {
        var constructor = configType.GetConstructor(Type.EmptyTypes);
        var config = constructor.Invoke(null);
        Configuration.Bind(configType.Name, config);
        return config;
    }
}
