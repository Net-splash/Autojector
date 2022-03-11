using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Registers.Configs;
internal class ConfigFactory
{
    public ConfigFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    internal object GetConfig(ConstructorInfo constructor, Type configType, IEnumerable<string> keys = null)
    {
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

}
