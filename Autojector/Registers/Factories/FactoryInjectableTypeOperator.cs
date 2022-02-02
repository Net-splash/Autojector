using Autojector.Abstractions;
using Autojector.Base;
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.Factories;
internal record FactoryInjectableTypeOperator(
    Type Type,
    IFactoryRegisterStrategyFactory FactoryRegisterStrategyFactory) 
    : 
    BaseTypeOperator(Type), 
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        var allFactoriesFromCurrentType = GetAllFactories();
        foreach (var factoryInterface in allFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = FactoryRegisterStrategyFactory.GetFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }
    }

    private IEnumerable<Type> GetAllFactories()
    {
        var filteredInterfaces = Type.GetInterfacesFromTree(i => i.IsGenericType && FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
