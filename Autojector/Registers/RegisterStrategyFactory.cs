using Autojector.Public;
using Autojector.Registers.Factories;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Autojector.Registers;
internal class RegisterStrategyFactory
{
    public RegisterStrategyFactory(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }
    private Dictionary<Type, Func<IServiceCollection, ISimpleRegisterStrategy>> SimpleLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, ISimpleRegisterStrategy>>()
            {
                {typeof(ITransientInjectable<>),(services) => new SimpleRegister(services.AddTransient) },
                {typeof(IScopeInjectable<>),(services) => new SimpleRegister(services.AddScoped) },
                {typeof(ISingletonInjectable<>),(services) => new SimpleRegister(services.AddSingleton) },

            };

    internal ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetypeType)
    {
        if (!SimpleLifetypeRegisterStrategies.ContainsKey(lifetypeType))
        {
            throw new InvalidOperationException($"Unknown lifetype implementation");
        }

        return SimpleLifetypeRegisterStrategies[lifetypeType](Services);
    }


    private Dictionary<Type, Func<IServiceCollection, IFactoryRegisterStrategy>> FactoryLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, IFactoryRegisterStrategy>>()
        {
                {typeof(ITransientFactoryInjectable<>),(services) => new FactoryRegister(services.AddTransient,services.AddTransient) },
                {typeof(IScopeFactoryInjectable<>),(services) => new FactoryRegister(services.AddScoped,services.AddScoped) },
                {typeof(ISingletonFactoryInjectable<>),(services) => new FactoryRegister(services.AddSingleton,services.AddSingleton) },
        };
    internal IFactoryRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetypeType)
    {
        if (!FactoryLifetypeRegisterStrategies.ContainsKey(lifetypeType))
        {
            throw new InvalidOperationException($"Unknown lifetype implementation");
        }

        return FactoryLifetypeRegisterStrategies[lifetypeType](Services);
    }
}
