using Autojector.Public;
using Autojector.Registers.Factories;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Autojector.Registers;
internal class LifetypeRegisterStrategyFactory
{
    public LifetypeRegisterStrategyFactory(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }
    private Dictionary<Type, Func<IServiceCollection, ISimpleLifetypeRegisterStrategy>> SimpleLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, ISimpleLifetypeRegisterStrategy>>()
            {
                {typeof(ITransientInjectable<>),(services) => new TransientLifeTypeRegister(services) },
                {typeof(IScopeInjectable<>),(services) => new ScopeLifeTypeRegister(services) },
                {typeof(ISingletonInjectable<>),(services) => new SingletonLifeTypeRegister(services) },

            };

    private Dictionary<Type, Func<IServiceCollection, IFactoryLifetypeRegisterStrategy>> FactoryLifetypeRegisterStrategies = new Dictionary<Type, Func<IServiceCollection, IFactoryLifetypeRegisterStrategy>>()
        {
                {typeof(ITransientFactoryInjectable<>),(services) => new TransientFactoryRegister(services) },
                {typeof(IScopeFactoryInjectable<>),(services) => new ScopeFactoryRegister(services) },
                {typeof(ISingletonFactoryInjectable<>),(services) => new SingletonFactoryRegister(services) },
        };

    internal ISimpleLifetypeRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetypeType)
    {
        if (!SimpleLifetypeRegisterStrategies.ContainsKey(lifetypeType))
        {
            throw new InvalidOperationException($"Unknown lifetype implementation");
        }

        return SimpleLifetypeRegisterStrategies[lifetypeType](Services);
    }

    internal IFactoryLifetypeRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetypeType)
    {
        if (!FactoryLifetypeRegisterStrategies.ContainsKey(lifetypeType))
        {
            throw new InvalidOperationException($"Unknown lifetype implementation");
        }

        return FactoryLifetypeRegisterStrategies[lifetypeType](Services);
    }
}
