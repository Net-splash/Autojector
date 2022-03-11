
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Configs;

internal interface IConfigRegisterStrategy
{
    void Add(Type interfaceType, Type classType, IEnumerable<string> keys);
}

internal class ConfigRegisterStrategy : IConfigRegisterStrategy
{
    private IServiceCollection Services { get; }
    private bool IsConfigFactoryRegistred {
        get{
            return Services.Any(s => s.ServiceType == typeof(ConfigFactory));
        }
    }
    public ConfigRegisterStrategy(IServiceCollection services)
    {
        Services = services;
    }

    public void Add(Type interfaceType, Type configType, IEnumerable<string> keys = null)
    {
        AddConfigFactory();
        Services.AddTransient(interfaceType, (serviceProvider) =>
        {
            var serviceFactory = (ConfigFactory)serviceProvider.GetService(typeof(ConfigFactory));
            return serviceFactory.GetConfig(configType, keys);
        });
    }

    private void AddConfigFactory()
    {
        if (!IsConfigFactoryRegistred)
        {
            Services.AddTransient<ConfigFactory>();
        }
    }

}