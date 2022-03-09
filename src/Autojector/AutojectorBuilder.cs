using Autojector.Base;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Base;
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

namespace Autojector;

internal class AutojectorBuilder : IAutojectorBuilder
{
    internal AutojectorBuilder(Assembly[] assemblies, IServiceCollection services)
    {
        Features = new List<IAutojectorFeature>();
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }
        Assemblies = assemblies;
        Services = services;
    }
    private List<IAutojectorFeature> Features { get; }
    private Assembly[] Assemblies { get; }
    private IServiceCollection Services { get; }

    public IAutojectorBuilder UseSimpleInjection(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var simpleRegisterStrategyFactory = new SimpleRegisterStrategyFactory(Services);
        Features.Add(new AutojectorSimpleInjectionFeature(assemblies, simpleRegisterStrategyFactory));
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

    public IAutojectorBuilder UseDecorator(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var decoratorRegisterStrategy = new DecoratorRegisterStrategy(Services);
        Features.Add(new AutojectorDecoratorsFeature(assemblies, decoratorRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseConfigs(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var configRegisterStrategy = new ConfigRegisterStrategy(Services);
        Features.Add(new AutojectorConfigsFeature(assemblies, configRegisterStrategy));
        return this;
    }

    public IAutojectorBuilder UseChains(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var chainRegisterStrategy = new ChainRegisterStrategy(Services);
        Features.Add(new AutojectorChainsFeature(assemblies, chainRegisterStrategy));
        return this;
    }

    public IAutojectorService Build()
    {
        ValidateAgainstOnlyDecoratorAdded();
        var orderedFeatures = GetAutojectorFeatures();
        return new AutojectorService(orderedFeatures);
    }

    private void ValidateAgainstOnlyDecoratorAdded()
    {
        var decoratorFeatures = Features.Where(f => f.GetType() == typeof(AutojectorDecoratorsFeature));
        if(decoratorFeatures.Count() == Features.Count())
        {
            throw new InvalidOperationException(@$"You can't have only decorators feature because there will be nothing to decorate. 
                            Please make sure to call other methods from builder beside UseDecorator");
        }
    }

    private IEnumerable<IAutojectorFeature> GetAutojectorFeatures()
    {
        var orderedFeatures = Features.OrderBy(feature => feature.Priority);
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
