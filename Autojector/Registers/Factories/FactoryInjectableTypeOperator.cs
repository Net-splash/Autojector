using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Factories;
public record FactoryInjectableTypeOperator(Type Type) : BaseTypeOperator(Type), ITypeConfigurator
{
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var allFactoriesFromCurrentType = GetAllFactories();
        var lifetypeRegisterStrategyFactory = new RegisterStrategyFactory(services);
        foreach (var factoryInterface in allFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(Type, factoryInterface);
        }

        return services;
    }

    private IEnumerable<Type> GetAllFactories()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i => i.IsGenericType && FactoryInjectableTypes.FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }
}
