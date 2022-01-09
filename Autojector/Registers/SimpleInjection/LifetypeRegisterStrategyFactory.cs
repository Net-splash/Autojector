using Autojector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.SimpleInjection;
internal class LifetypeRegisterStrategyFactory
{
    public LifetypeRegisterStrategyFactory(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }
    private Dictionary<Type, Func<IServiceCollection, ISimpleLifetypeRegisterStrategy>> LifetypeRegisters = new Dictionary<Type, Func<IServiceCollection, ISimpleLifetypeRegisterStrategy>>()
            {
                {typeof(ITransientInjectable<>),(services) => new TransientLifeTypeRegister(services) },
                {typeof(IScopeInjectable<>),(services) => new ScopetLifeTypeRegister(services) },
                {typeof(ISingletonInjectable<>),(services) => new SingletonLifeTypeRegister(services) },

            };

    internal ISimpleLifetypeRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetypeType)
    {
        if (!LifetypeRegisters.ContainsKey(lifetypeType))
        {
            throw new InvalidOperationException($"Unknown lifetype implementation");
        }

        return LifetypeRegisters[lifetypeType](Services);
    }
}
