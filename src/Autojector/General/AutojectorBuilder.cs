using Autojector.Base;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Decorators;
using Autojector.Features.Factories;
using Autojector.Features.SimpleInjection;
using Autojector.Registers.AsyncFactories;
using Autojector.Registers.Chains;
using Autojector.Registers.Configs;
using Autojector.Registers.Decorators;
using Autojector.Registers.Factories;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.General;

internal class AutojectorBuilder : IAutojectorBuilder
{
    internal AutojectorBuilder(Assembly[] assemblies, IServiceCollection services)
    {
        Features = new List<IAutojectorFeature>();
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        Assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }
    private List<IAutojectorFeature> Features { get; }
    private Assembly[] Assemblies { get; }
    private IServiceCollection Services { get; }

    public IAutojectorBuilder UseSimpleInjectionByInterface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var simpleRegisterStrategyFactory = new SimpleRegisterStrategyFactory(Services);
        Features.Add(new AutojectorSimpleInjectionByInterfaceFeature(assemblies, simpleRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseSimpleInjectionByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var simpleRegisterStrategyFactory = new SimpleRegisterStrategyFactory(Services);
        Features.Add(new AutojectorSimpleInjectionByAttributeFeature(assemblies, simpleRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var factoryRegisterStrategyFactory = new FactoryRegisterStrategyFactory(Services);
        Features.Add(new AutojectorFactoriesFeature(assemblies, factoryRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseAsyncFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var asyncFactoryRegisterStrategyFactory = new AsyncFactoryRegisterStrategyFactory(Services);
        Features.Add(new AutojectorAsyncFactoriesFeature(assemblies, asyncFactoryRegisterStrategyFactory));
        return this;
    }

    public IAutojectorBuilder UseDecoratorByInterface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var decoratorRegisterStrategy = new DecoratorRegisterStrategy(Services);
        Features.Add(new AutojectorDecoratorsByInterfaceFeature(assemblies, decoratorRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseDecoratorByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var decoratorRegisterStrategy = new DecoratorRegisterStrategy(Services);
        Features.Add(new AutojectorDecoratorsByAttributeFeature(assemblies, decoratorRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseConfigsByInteface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = new ConfigRegisterStrategy(Services);
        Features.Add(new AutojectorConfigsByInterfaceFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseConfigsByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = new ConfigRegisterStrategy(Services);
        Features.Add(new AutojectorConfigsByAttributeFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseUnimplementedConfigsByInteface(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = new ConfigRegisterStrategy(Services);
        Features.Add(new AutojectorConfigsUnimplementedByInterfaceFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseUnimplementedConfigsByAttribute(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = new ConfigRegisterStrategy(Services);
        Features.Add(new AutojectorConfigsUnimplementedByAttributeFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseChains(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var chainRegisterStrategy = new ChainRegisterStrategy(Services);
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
