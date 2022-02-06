using Autojector.Abstractions;
using System;
using System.Collections.Generic;

namespace Autojector.Base;
internal class Types
{
    public static Type InjectableType = typeof(IInjectable);

    public static Type TransientType = typeof(ITransient<>);
    public static Type ScopeType = typeof(IScope<>);
    public static Type SingletonType = typeof(ISingleton<>);
    public static IEnumerable<Type> SimpleLifetypeInterfaces = new List<Type>()
        {
            TransientType,
            ScopeType,
            SingletonType
        };

    public static IEnumerable<Type> InjectableInterfaces = new List<Type>()
        {
            InjectableType,
        };

    public static Type FactoryType = typeof(IFactory<>);
    public static Type FactoryTransientType = typeof(ITransientFactory<>);
    public static Type FactoryScopeType = typeof(IScopeFactory<>);
    public static Type FactorySingletonType = typeof(ISingletonFactory<>);
    public static IEnumerable<Type> FactoriesTypeInterfaces = new List<Type>()
        {
            FactoryTransientType,
            FactoryScopeType,
            FactorySingletonType
        };

    public static Type AsyncFactoryType = typeof(IAsyncFactory<>);
    public static Type AsyncTransientInjectableType = typeof(IAsyncTransientFactory<>);
    public static Type AsyncScopeInjectableType = typeof(IAsyncScopeFactory<>);
    public static Type AsyncSingletonInjectableType = typeof(IAsyncSingletonFactory<>);
    public static IEnumerable<Type> AsyncFactoriesTypeInterfaces = new List<Type>()
        {
            AsyncTransientInjectableType,
            AsyncScopeInjectableType,
            AsyncSingletonInjectableType
        };

    public static Type DecoratorType = typeof(IDecorator<>);
    public static Type DecoratorOrderAttributeType = typeof(DecoratorOrderAttribute);

    public static Type ConfigType = typeof(IConfig);

    public static Type ChainLinkType = typeof(IChainLink<,>);
}

