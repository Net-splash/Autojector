using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Autojector;
public static class AutojectorExtensions
{
    public static IServiceCollection WithAutojector(this IServiceCollection services, Action<AutojectorOptions> configureOptions = null, params Assembly[] assemblies)
    {
        var autojectorOptions = new AutojectorOptions(assemblies, services);
        if(configureOptions == null)
        {
            configureOptions = (options) => options.UseSimpleInjection();  
        }
        configureOptions(autojectorOptions);
        foreach(var feature in autojectorOptions.GetAutojectorFeatures())
        {
            feature.ConfigureServices();
        }

        return services;
    }

    public static IServiceCollection AddAutojector(this IServiceCollection services, params Assembly[] assemblies)
    {
        var autojectorOptions = new AutojectorOptions(assemblies, services);

        autojectorOptions.UseSimpleInjection();
        autojectorOptions.UseFactories();
        autojectorOptions.UseAsyncFactories();
        autojectorOptions.UseDecorator();
        autojectorOptions.UseConfigs();

        foreach (var feature in autojectorOptions.GetAutojectorFeatures())
        {
            feature.ConfigureServices();
        }

        return services;
    }
}
