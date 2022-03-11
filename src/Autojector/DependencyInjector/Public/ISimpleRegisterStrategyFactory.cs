using System;
using Autojector.Abstractions;

namespace Autojector.DependencyInjector.Public;
internal interface ISimpleRegisterStrategyFactory
{
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(Type lifetimeType);
    public ISimpleRegisterStrategy GetSimpleLifetypeRegisterStrategy(BaseInjectionAttribute attribute);
}
