using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Factories;
public record FactoryInjectableClassType(Type FactoryImplementationType)
{
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var lifetypeRegisterStrategyFactory = new LifetypeRegisterStrategyFactory(services);
        foreach (var factoryInterface in AllFactoriesFromCurrentType)
        {
            var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetFactoryLifetypeRegisterStrategy(factoryInterface.GetGenericTypeDefinition());
            lifetypeRegisterStrategy.Add(FactoryImplementationType, factoryInterface);
        }

        return services;
    }

    private IEnumerable<Type> _allFactoriesFromCurrentType;
    private IEnumerable<Type> AllFactoriesFromCurrentType =>
    _allFactoriesFromCurrentType ?? (_allFactoriesFromCurrentType = GetAllFactories(FactoryImplementationType));


    private IEnumerable<Type> GetAllFactories(Type type)
    {
        var allInterfaces = type.GetInterfaces();
        var groupedInterfaces = allInterfaces.ToLookup(i => i.IsGenericType && FactoryInjectableTypes.FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        var currentFactories = groupedInterfaces[true];
        var otherInterfaces = groupedInterfaces[false];
        var childrenInterfaceFactories = otherInterfaces.SelectMany(x => GetAllFactories(x));

        return currentFactories.Concat(childrenInterfaceFactories).Distinct();
    }


}
