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
        var types = Assemblies.SelectMany(type => type.GetTypes());
        var typeConfigurators = GetTypeConfigurators(types);
        foreach (var configurator in typeConfigurators)
        {
            configurator.ConfigureServices(services);
        }

        return services;
    }

    protected abstract IEnumerable<ITypeConfigurator> GetTypeConfigurators(IEnumerable<Type> types);

    public abstract AutojectorFeaturesEnum Priority { get; }
}
