﻿using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using static Autojector.Base.Types;
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
        ValidateAgainstLifetimeTypeArgument(lifetimeType);
        return SimpleLifetypeRegisterStrategies[lifetimeType](Services);
    }

    private void ValidateAgainstLifetimeTypeArgument(Type lifetimeType)
    {
        if (lifetimeType == null)
        {
            throw new ArgumentNullException($"lifetimeType argument is null. Please ensure that {nameof(GetSimpleLifetypeRegisterStrategy)} is called with a value");
        }

        if (!SimpleLifetypeRegisterStrategies.ContainsKey(lifetimeType))
        {
            throw new InvalidOperationException($"Unknown lifetime implementation for {lifetimeType.FullName}");
        }
    }
}
