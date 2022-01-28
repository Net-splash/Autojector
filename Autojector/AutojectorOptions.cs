using Autojector.Features;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Base;
using Autojector.Features.Decorators;
using Autojector.Features.Factories;
using Autojector.Features.SimpleInjection;
using Autojector.Features.Validators;
using Autojector.Registers.Configs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector;
public class AutojectorOptions
{
    public class AutojectorValidatorsOptions
    {
        internal AutojectorValidatorsOptions()
        {

        }

        public AutojectorValidatorsOptions UseCycleDependencyCheck()
        {
            return this;
        }

    }
    internal AutojectorOptions(Assembly[] assemblies, IServiceCollection services)
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

    public AutojectorOptions UseSimpleInjection(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorSimpleInjectionFeature(assemblies, Services));
        return this;
    }

    public AutojectorOptions UseFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorFactoriesFeature(assemblies, Services));
        return this;
    }

    public AutojectorOptions UseAsyncFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorAsyncFactoriesFeature(assemblies, Services));
        return this;
    }

    public AutojectorOptions UseDecorator(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorDecoratorsFeature(assemblies, Services));
        return this;
    }

    public AutojectorOptions UseConfigs(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorConfigsFeature(assemblies, Services));
        return this;
    }

    public AutojectorOptions UseValidators(Action<AutojectorValidatorsOptions> validatorsConfigureOptions, params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var autojectorValidatorsOptions = new AutojectorValidatorsOptions();
        validatorsConfigureOptions(autojectorValidatorsOptions);
        Features.Add(new AutojectorValidatorsFeatures(autojectorValidatorsOptions, assemblies, Services));
        return this;
    }

    internal IEnumerable<IAutojectorFeature> GetAutojectorFeatures()
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
