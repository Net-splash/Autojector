using System;

namespace Autojector.DependencyInjector.Public
{
    internal interface IAsyncFactoryRegisterStrategyFactory
    {
        IAsyncFactoryRegisterStrategy GetAsyncFactoryLifetypeRegisterStrategy(Type lifetimeType);
    }
}
