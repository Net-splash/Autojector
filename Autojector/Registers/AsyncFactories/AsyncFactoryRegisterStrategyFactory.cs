using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.AsyncFactories;
internal interface IAsyncFactoryRegisterStrategyFactory
{
    public IAsyncFactoryRegisterStrategy GetAsyncFactoryLifetypeRegisterStrategy(Type lifetimeType);
}
internal class AsyncFactoryRegisterStrategyFactory : IAsyncFactoryRegisterStrategyFactory
{
    private IServiceCollection Services { get; }

    public AsyncFactoryRegisterStrategyFactory(IServiceCollection services)
    {
        this.Services = services;
    }


    private Dictionary<Type, Func<IServiceCollection, IAsyncFactoryRegisterStrategy>> AsyncFactoryLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, IAsyncFactoryRegisterStrategy>>()
        {
                {AsyncFactoryInjectableTypeOperator.AsyncTransientInjectableType,(services) => new AsyncFactoryRegisterStrategy(services.AddTransient,services.AddTransient) },
                {AsyncFactoryInjectableTypeOperator.AsyncScopeInjectableType,(services) => new AsyncFactoryRegisterStrategy(services.AddScoped,services.AddScoped) },
                {AsyncFactoryInjectableTypeOperator.AsyncSingletonInjectableType,(services) => new AsyncFactoryRegisterStrategy(services.AddSingleton,services.AddSingleton) },
        };
    public IAsyncFactoryRegisterStrategy GetAsyncFactoryLifetypeRegisterStrategy(Type lifetimeType)
    {
        if (!AsyncFactoryLifetypeRegisterStrategies.ContainsKey(lifetimeType))
        {
            throw new InvalidOperationException($"Unknown lifetime implementation");
        }

        return AsyncFactoryLifetypeRegisterStrategies[lifetimeType](Services);
    }
}

