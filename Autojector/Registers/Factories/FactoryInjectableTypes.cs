using Autojector.Public;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.Factories;
internal static class FactoryInjectableTypes
{
    public static Type TransientInjectableType = typeof(ITransientFactoryInjectable<>);
    public static Type ScopeInjectableType = typeof(IScopeFactoryInjectable<>);
    public static Type SingletonInjectableType = typeof(ISingletonFactoryInjectable<>);
    public static IEnumerable<Type> FactoriesTypeInterfaces = new List<Type>()
        {
            TransientInjectableType,
            ScopeInjectableType,
            SingletonInjectableType
        };

    public static IEnumerable<Type> InjectableInterfaces = new List<Type>()
            {
    };
}
