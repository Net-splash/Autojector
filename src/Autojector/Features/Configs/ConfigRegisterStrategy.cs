using Autojector.DependencyInjector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Features.Configs;

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
        var constructor = configType.GetConstructor(Type.EmptyTypes);
        ValidateAgainstNonexistentEmptyConstructor(constructor, configType);
        Services.AddTransient(interfaceType, (serviceProvider) =>
        {
            var serviceFactory = (ConfigFactory)serviceProvider.GetService(typeof(ConfigFactory));
            return serviceFactory.GetConfig(constructor,configType, keys);
        });
    }

    private void ValidateAgainstNonexistentEmptyConstructor(System.Reflection.ConstructorInfo constructor, Type configType)
    {
        if (constructor == null)
        {
            throw new InvalidOperationException($"The type {configType?.Name} doens't have an empty constructor as each IConfig require");
        }
    }

    private void AddConfigFactory()
    {
        if (!IsConfigFactoryRegistred)
        {
            Services.AddTransient<ConfigFactory>();
        }
    }

}