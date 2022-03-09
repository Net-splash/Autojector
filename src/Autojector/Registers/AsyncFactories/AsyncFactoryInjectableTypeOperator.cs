using System;
using System.Collections.Generic;
using System.Linq;
using Autojector.Base;
using Autojector.Registers.Base;
using static Autojector.Base.Types;

namespace Autojector.Registers.AsyncFactories;
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
