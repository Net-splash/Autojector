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
        ValidateAgainstNonexistentEmptyConstructor(constructor, configType);
        var config = constructor.Invoke(null);
        Configuration.Bind(configType.Name, config);
        return config;
    }

    private void ValidateAgainstNonexistentEmptyConstructor(System.Reflection.ConstructorInfo constructor, Type configType)
    {
        if(constructor == null)
        {
            throw new ArgumentNullException($"The type {configType?.Name} doens't have an empty constructor as each IConfig require");
        }
    }
}
