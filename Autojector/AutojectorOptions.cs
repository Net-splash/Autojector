using Autojector.Features;
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
    internal AutojectorOptions(Assembly[] assemblies)
    {
        Features = new List<IAutojectorFeature>();
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }
        Assemblies = assemblies;
    }
    private List<IAutojectorFeature> Features { get; }
    private Assembly[] Assemblies { get; }

    public AutojectorOptions UseAutojectorSimpleInjection(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorSimpleInjectionFeature(assemblies));
        return this;
    }

    public AutojectorOptions UseAutojectorFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorFactoriesFeature(assemblies));
        return this;
    }

    public AutojectorOptions UseAutojectorAsyncFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorAsyncFactoriesFeature(assemblies));
        return this;
    }

    public AutojectorOptions UseAutojectorDecorator(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorDecoratorsFeature(assemblies));
        return this;
    }

    public AutojectorOptions UseAutojectorValidators(Action<AutojectorValidatorsOptions> validatorsConfigureOptions, params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        var autojectorValidatorsOptions = new AutojectorValidatorsOptions();
        validatorsConfigureOptions(autojectorValidatorsOptions);
        Features.Add(new AutojectorValidatorsFeatures(autojectorValidatorsOptions, assemblies));
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
