using System;

namespace Autojector.DependencyInjector.Public
{
    internal interface IFactoryRegisterStrategyFactory
    {
        IFactoryRegisterStrategy GetFactoryLifetypeRegisterStrategy(Type lifetimeType);
    }
}
