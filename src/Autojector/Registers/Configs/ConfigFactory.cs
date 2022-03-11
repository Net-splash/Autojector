using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Configs;
internal class ConfigFactory
{
    public ConfigFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    internal object GetConfig(Type configType, IEnumerable<string> keys = null)
    {
        var constructor = configType.GetConstructor(Type.EmptyTypes);
        ValidateAgainstNonexistentEmptyConstructor(constructor, configType);
        var config = constructor.Invoke(null);
        keys = keys.Concat(new string[] { configType.Name });

        foreach (var key in keys.Where(k => k != null))
        {
            if (Configuration.GetSection(key) != null)
            {
                Configuration.Bind(key, config);
                return config;
            }
        }

        throw new InvalidProgramException("No config section found with keys :" + string.Join(",", keys));
    }

    private void ValidateAgainstNonexistentEmptyConstructor(System.Reflection.ConstructorInfo constructor, Type configType)
    {
        if (constructor == null)
        {
            throw new ArgumentNullException($"The type {configType?.Name} doens't have an empty constructor as each IConfig require");
        }
    }
}
