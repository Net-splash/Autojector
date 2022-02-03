
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.Configs;

internal interface IConfigRegisterStrategy
{
    void Add(Type config);
}

internal class ConfigRegisterStrategy : IConfigRegisterStrategy
{
    private IServiceCollection Services { get; }
    private bool IsConfigFactoryRegistred = false;
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
            IsConfigFactoryRegistred = true;
        }
    }
}
