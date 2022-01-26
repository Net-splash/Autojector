using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Autojector;
public static class AutojectorExtensions
{
    public static IServiceCollection AddAutojector(this IServiceCollection services, Action<AutojectorOptions> configureOptions = null, params Assembly[] assemblies)
    {
        var autojectorOptions = new AutojectorOptions(assemblies);
        if(configureOptions == null)
        {
            configureOptions = (options) => options.UseAutojectorSimpleInjection();  
        }
        configureOptions(autojectorOptions);
        foreach(var feature in autojectorOptions.GetAutojectorFeatures())
        {
            feature.ConfigureServices(services);
        }

        return services;
    }
}
