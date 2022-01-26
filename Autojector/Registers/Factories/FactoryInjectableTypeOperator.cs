using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Factories;
public record FactoryInjectableTypeOperator(Type Type) : BaseTypeOperator(Type), ITypeConfigurator
{
    public static Type TransientInjectableType = typeof(ITransientFactoryInjectable<>);
    public static Type ScopeInjectableType = typeof(IScopeFactoryInjectable<>);
    public static Type SingletonInjectableType = typeof(ISingletonFactoryInjectable<>);
    public static IEnumerable<Type> FactoriesTypeInterfaces = new List<Type>()
        {
            TransientInjectableType,
            ScopeInjectableType,
            SingletonInjectableType
        };
    public static Type FactoryType = typeof(IFactory<>);
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var allFactoriesFromCurrentType = GetAllFactories();
        var lifetypeRegisterStrategyFactory = new FactoryRegisterStrategyFactory(services);
        foreach (var factoryInterface in allFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }

        return services;
    }

    private IEnumerable<Type> GetAllFactories()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i => i.IsGenericType && FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
