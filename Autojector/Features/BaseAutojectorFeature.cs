using Autojector.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector;
internal abstract class BaseAutojectorFeature : IAutojectorFeature
{
    protected IEnumerable<Assembly> Assemblies { get; }


    protected BaseAutojectorFeature(IEnumerable<Assembly> assemblies = null)
    {
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }
        Assemblies = assemblies;
    }

    public abstract void ConfigureServices(IServiceCollection services);
    public abstract AutojectorFeaturesEnum Priority { get; }
}
