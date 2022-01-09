using Autojector.Public;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.SimpleInjection;
internal static class SimpleInjectableTypes
{
    public static Type ILifetypeInjectableType = typeof(ILifetypeInjectable);
    public static Type TransientInjectableType = typeof(ITransientInjectable<>);
    public static Type ScopeInjectableType = typeof(IScopeInjectable<>);
    public static Type SingletonInjectableType = typeof(ISingletonInjectable<>);
    public static Type InjectableType = typeof(IInjectable);
    public static IEnumerable<Type> SimpleLifeTypeInterfaces = new List<Type>()
        {
            TransientInjectableType,
            ScopeInjectableType,
            SingletonInjectableType
        };

    public static IEnumerable<Type> InjectableInterfaces = new List<Type>()
            {
                InjectableType,
                ILifetypeInjectableType,
            };
}
