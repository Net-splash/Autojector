using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.AsyncFactories;
internal record AsyncFactoryInjectableTypeOperator(Type Type) : BaseTypeOperator(Type), ITypeConfigurator
{

    public static Type AsyncTransientInjectableType = typeof(IAsyncTransientFactoryInjectable<>);
    public static Type AsyncScopeInjectableType = typeof(IAsyncScopeFactoryInjectable<>);
    public static Type AsyncSingletonInjectableType = typeof(IAsyncSingletonFactoryInjectable<>);
    public static IEnumerable<Type> AsyncFactoriesTypeInterfaces = new List<Type>()
        {
            AsyncTransientInjectableType,
            AsyncScopeInjectableType,
            AsyncSingletonInjectableType
        };

    public static Type AsyncFactoryType = typeof(IAsyncFactory<>);

    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var allFactoriesFromCurrentType = GetAllAsyncFactories();
        var lifetypeRegisterStrategyFactory = new AsyncFactoryRegisterStrategyFactory(services);
        foreach (var factoryInterface in allFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetAsyncFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }

        return services;
    }

    private IEnumerable<Type> GetAllAsyncFactories()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i => i.IsGenericType && AsyncFactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
