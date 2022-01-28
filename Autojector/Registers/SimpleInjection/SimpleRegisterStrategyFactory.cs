using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using static Autojector.Abstractions.Types;
namespace Autojector.Registers.SimpleInjection;
internal interface ISimpleRegisterStrategyFactory
{
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetimeType);
}
internal class SimpleRegisterStrategyFactory : ISimpleRegisterStrategyFactory
{
    private IServiceCollection Services { get; }
    public SimpleRegisterStrategyFactory(IServiceCollection services)
    {
        Services = services;
    }

    private Dictionary<Type, Func<IServiceCollection, ISimpleRegisterStrategy>> SimpleLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, ISimpleRegisterStrategy>>()
            {
                {TransientType,(services) => new SimpleRegisterStrategy(services.AddTransient) },
                {ScopeType,(services) => new SimpleRegisterStrategy(services.AddScoped) },
                {SingletonType,(services) => new SimpleRegisterStrategy(services.AddSingleton) },

            };
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetimeType)
    {
        if (!SimpleLifetypeRegisterStrategies.ContainsKey(lifetimeType))
        {
            throw new InvalidOperationException($"Unknown lifetime implementation");
        }

        return SimpleLifetypeRegisterStrategies[lifetimeType](Services);
    }
}
