using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.Factories;
internal interface IFactoryRegisterStrategyFactory
{
    public IFactoryRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetimeType);
}
internal class FactoryRegisterStrategyFactory : IFactoryRegisterStrategyFactory
{
    public FactoryRegisterStrategyFactory(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }


    private Dictionary<Type, Func<IServiceCollection, IFactoryRegisterStrategy>> FactoryLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, IFactoryRegisterStrategy>>()
        {
                {FactoryTransientType,(services) => new FactoryRegisterStrategy(services.AddTransient,services.AddTransient) },
                {FactoryScopeType,(services) => new FactoryRegisterStrategy(services.AddScoped,services.AddScoped) },
                {FactorySingletonType,(services) => new FactoryRegisterStrategy(services.AddSingleton,services.AddSingleton) },
        };
    public IFactoryRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetimeType)
    {
        if (!FactoryLifetypeRegisterStrategies.ContainsKey(lifetimeType))
        {
            throw new InvalidOperationException($"Unknown lifetime implementation");
        }

        return FactoryLifetypeRegisterStrategies[lifetimeType](Services);
    }
}
