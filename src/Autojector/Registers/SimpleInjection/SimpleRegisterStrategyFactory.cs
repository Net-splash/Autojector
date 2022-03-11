using System;
using System.Collections.Generic;
using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using static Autojector.Base.Types;

namespace Autojector.Registers.SimpleInjection;
internal interface ISimpleRegisterStrategyFactory
{
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetimeType);
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(BaseInjectionAttribute attribute);
}
internal class SimpleRegisterStrategyFactory : ISimpleRegisterStrategyFactory
{
    private IServiceCollection Services { get; }
    public SimpleRegisterStrategyFactory(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
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

    private void ValidateAgainstLifetimeTypeArgument(BaseInjectionAttribute attribute)
    {
        if (attribute.GetType() == null)
        {
            throw new ArgumentNullException($"attribute argument is null. Please ensure that {nameof(GetSimpleLifetypeRegisterStrategy)} is called with a value");
        }

        if (!SimpleAttributeRegisterStrategies.ContainsKey(attribute.GetType()))
        {
            throw new InvalidOperationException($"Unknown lifetime implementation for {attribute.GetType().FullName}");
        }
    }

    private Dictionary<Type, Type> SimpleAttributeRegisterStrategies = new Dictionary<Type, Type>()
            {
                {TransientAttributeType,TransientType },
                {ScopeAttributeType,ScopeType },
                {SingletonAttributeType,SingletonType },
            };

    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(BaseInjectionAttribute attribute)
    {
        ValidateAgainstLifetimeTypeArgument(attribute);
        var simpleLifetypeInterface = SimpleAttributeRegisterStrategies[attribute.GetType()];
        return GetSimpleLifetypeRegisterStrategy(simpleLifetypeInterface);
    }

}
