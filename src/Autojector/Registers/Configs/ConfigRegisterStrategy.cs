
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Autojector.Registers.Configs;

internal interface IConfigRegisterStrategy
{
    void Add(Type config);
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

    public void Add(Type configType)
    {
        AddConfigFactory();
        Services.AddTransient(configType, (serviceProvider) =>
        {
            var serviceFactory = (ConfigFactory)serviceProvider.GetService(typeof(ConfigFactory));
            return serviceFactory.GetConfig(configType);
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
