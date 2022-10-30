using System;
using System.Collections.Generic;
using Autojector.Base;
using Autojector.DependencyInjector.Public;

namespace Autojector.Features.AsyncFactories
{
    internal class AsyncFactoryInjectableTypeOperator :
    ITypeConfigurator
    {
        public AsyncFactoryInjectableTypeOperator(
            Type type,
            IEnumerable<Type> factoriesFromCurrentType,
            IAsyncFactoryRegisterStrategyFactory asyncFactoryRegisterStrategyFactory)
        {
            Type = type;
            FactoriesFromCurrentType = factoriesFromCurrentType;
            AsyncFactoryRegisterStrategyFactory = asyncFactoryRegisterStrategyFactory;
        }

        public Type Type { get; }
        public IEnumerable<Type> FactoriesFromCurrentType { get; }
        public IAsyncFactoryRegisterStrategyFactory AsyncFactoryRegisterStrategyFactory { get; }

        public void ConfigureServices()
        {
            foreach (var factoryInterface in FactoriesFromCurrentType)
            {
                var lifetypeRegisterStrategy = AsyncFactoryRegisterStrategyFactory.GetAsyncFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
                lifetypeRegisterStrategy.Add(Type, factoryInterface);
            }
        }
    }

}
