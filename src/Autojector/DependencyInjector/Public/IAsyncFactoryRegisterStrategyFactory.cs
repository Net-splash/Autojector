using System;

namespace Autojector.DependencyInjector.Public;

internal interface IAsyncFactoryRegisterStrategyFactory
{
    public IAsyncFactoryRegisterStrategy GetAsyncFactoryLifetypeRegisterStrategy(Type lifetimeType);
}

