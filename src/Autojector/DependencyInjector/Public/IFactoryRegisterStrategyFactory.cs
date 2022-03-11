using System;

namespace Autojector.DependencyInjector.Public;

internal interface IFactoryRegisterStrategyFactory
{
    public IFactoryRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetimeType);
}
