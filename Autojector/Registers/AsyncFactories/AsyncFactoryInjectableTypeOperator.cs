using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.AsyncFactories;
internal record AsyncFactoryInjectableTypeOperator(Type Type, IAsyncFactoryRegisterStrategyFactory AsyncFactoryRegisterStrategyFactory) : 
    BaseTypeOperator(Type), 
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        var allFactoriesFromCurrentType = GetAllAsyncFactories();
        foreach (var factoryInterface in allFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = AsyncFactoryRegisterStrategyFactory.GetAsyncFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }

    }

    private IEnumerable<Type> GetAllAsyncFactories()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i => i.IsGenericType && AsyncFactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
