using System;
using System.Collections.Generic;
using Autojector.Base;
using Autojector.DependencyInjector.Public;

namespace Autojector.Features.AsyncFactories;
internal record AsyncFactoryInjectableTypeOperator(
    Type Type, 
    IEnumerable<Type> FactoriesFromCurrentType,
    IAsyncFactoryRegisterStrategyFactory AsyncFactoryRegisterStrategyFactory) : 
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        foreach (var factoryInterface in FactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = AsyncFactoryRegisterStrategyFactory.GetAsyncFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }
    }
}
