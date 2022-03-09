using Autojector.Base;
using Autojector.Features;
using Autojector.Features.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Registers.Base;
internal abstract class BaseAutojectorFeature : BaseTypeConfigurator,IAutojectorFeature
{
    protected IEnumerable<Assembly> Assemblies { get; }

    protected BaseAutojectorFeature(IEnumerable<Assembly> assemblies)
    {
        Assemblies = assemblies;
    }

    public override void ConfigureServices()
    {
        var typeConfigurators = GetTypeConfigurators();
        foreach (var configurator in typeConfigurators)
        {
            configurator.ConfigureServices();
        }
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

    protected IEnumerable<Type> InterfacesFromAssemblies
    {
        get
        {
            return AllTypesFromAssemblies.Where(type => type.IsInterface);
        }
    }

    protected abstract IEnumerable<ITypeConfigurator> GetTypeConfigurators();

    public abstract AutojectorFeaturesEnum Priority { get; }
}
