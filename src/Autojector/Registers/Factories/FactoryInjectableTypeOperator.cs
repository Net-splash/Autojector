using Autojector.Abstractions;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.Factories;
internal record FactoryInjectableTypeOperator(
    Type Type,
    IFactoryRegisterStrategyFactory FactoryRegisterStrategyFactory) 
    : 
    ITypeConfigurator
{
    public void ConfigureServices()
    {
        var ImplementedFactoryInterfaces = ExctractImplementedFactoryInterfaces();
        foreach (var factoryInterface in ImplementedFactoryInterfaces)
        {
            var lifetypeRegisterStrategy = FactoryRegisterStrategyFactory.GetFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }
    }

    private IEnumerable<Type> ExctractImplementedFactoryInterfaces()
    {
        var filteredInterfaces = Type.GetInterfacesFromTree(i => i.IsGenericType && FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
