using Autojector.Features;
using Autojector.Registers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector;
internal abstract class BaseAutojectorFeature : IAutojectorFeature
{
    protected IEnumerable<Assembly> Assemblies { get; }

    protected BaseAutojectorFeature(IEnumerable<Assembly> assemblies)
    {
        Assemblies = assemblies;
    }

    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var typeConfigurators = GetTypeConfigurators();
        foreach (var configurator in typeConfigurators)
        {
            configurator.ConfigureServices(services);
        }

        return services;
    }

    protected IEnumerable<Type> AllTypesFromAssemblies
    {
        get
        {
            return Assemblies.SelectMany(type => type.GetTypes());
        }
    }

    protected IEnumerable<Type> NonAbstractClassesFromAssemblies
    {
        get
        {
            return AllTypesFromAssemblies.Where(type => type.IsClass && !type.IsAbstract);
        }
    }

    protected abstract IEnumerable<ITypeConfigurator> GetTypeConfigurators();

    public abstract AutojectorFeaturesEnum Priority { get; }
}
