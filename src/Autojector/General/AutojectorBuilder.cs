using Autojector.Base;
using Autojector.DependencyInjector;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Chains;
using Autojector.Features.Configs;
using Autojector.Features.Decorators;
using Autojector.Features.Factories;
using Autojector.Features.SimpleInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.General;

internal class AutojectorBuilder : IAutojectorBuilder
{
    internal AutojectorBuilder(Assembly[] assemblies, IDependencyInjectorProvider dependencyInjectorProvider)
    {
        Features = new List<IAutojectorFeature>();
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        Assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        DependencyInjectorProvider = dependencyInjectorProvider;
    }

    private List<IAutojectorFeature> Features { get; }
    private Assembly[] Assemblies { get; }
    private IDependencyInjectorProvider DependencyInjectorProvider { get; }
    public IAutojectorBuilder UseSimpleInjectionByInterface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var simpleRegisterStrategyFactory = DependencyInjectorProvider.GetSimpleRegisterStrategyFactory();
        Features.Add(new AutojectorSimpleInjectionByInterfaceFeature(assemblies, simpleRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseSimpleInjectionByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var simpleRegisterStrategyFactory = DependencyInjectorProvider.GetSimpleRegisterStrategyFactory();
        Features.Add(new AutojectorSimpleInjectionByAttributeFeature(assemblies, simpleRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var factoryRegisterStrategyFactory = DependencyInjectorProvider.GetFactoryRegisterStrategyFactory();
        Features.Add(new AutojectorFactoriesFeature(assemblies, factoryRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseAsyncFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var asyncFactoryRegisterStrategyFactory = DependencyInjectorProvider.GetAsyncFactoryRegisterStrategyFactory();
        Features.Add(new AutojectorAsyncFactoriesFeature(assemblies, asyncFactoryRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseDecoratorByInterface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var decoratorRegisterStrategy = DependencyInjectorProvider.GetDecoratorRegisterStrategy();
        Features.Add(new AutojectorDecoratorsByInterfaceFeature(assemblies, decoratorRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseDecoratorByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var decoratorRegisterStrategy = DependencyInjectorProvider.GetDecoratorRegisterStrategy();
        Features.Add(new AutojectorDecoratorsByAttributeFeature(assemblies, decoratorRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseConfigsByInteface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = DependencyInjectorProvider.GetConfigRegisterStrategy();
        Features.Add(new AutojectorConfigsByInterfaceFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseConfigsByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = DependencyInjectorProvider.GetConfigRegisterStrategy();
        Features.Add(new AutojectorConfigsByAttributeFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseUnimplementedConfigsByInteface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = DependencyInjectorProvider.GetConfigRegisterStrategy();
        Features.Add(new AutojectorConfigsUnimplementedByInterfaceFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseUnimplementedConfigsByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = DependencyInjectorProvider.GetConfigRegisterStrategy();
        Features.Add(new AutojectorConfigsUnimplementedByAttributeFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseChains(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var chainRegisterStrategy = DependencyInjectorProvider.GetChainRegisterStrategy();
        Features.Add(new AutojectorChainsFeature(assemblies, chainRegisterStrategy));
        return this;
    }

    internal IAutojectorService Build()
    {
        var orderedFeatures = GetAutojectorFeatures();
        return new AutojectorService(orderedFeatures);
    }

    private IEnumerable<IAutojectorFeature> GetAutojectorFeatures()
    {
        var orderedFeatures = Features.OrderBy(feature => feature.FeatureType);
        return orderedFeatures;
    }

    private Assembly[] GetAssemblies(Assembly[] assemblies)
    {
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = Assemblies;
        }

        return assemblies;
    }
}
