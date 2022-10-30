using System;
using Autojector.Abstractions;

namespace Autojector.DependencyInjector.Public {
    internal interface ISimpleRegisterStrategyFactory
    {
        ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetimeType);
        ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(BaseInjectionAttribute attribute);
    }
}