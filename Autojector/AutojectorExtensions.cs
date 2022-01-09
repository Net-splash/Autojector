using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector;
public static class AutojectorExtensions
{
    public static IServiceCollection AddAutojector(this IServiceCollection services, Action<AutojectorOptions> configureOptions = null)
    {
        var autojectorOptions = new AutojectorOptions();
        if(configureOptions == null)
        {
            configureOptions = (o) => o.UseAutojectorSimpleInjection();  
        }
        configureOptions(autojectorOptions);
        foreach(var feature in autojectorOptions.GetAutojectorFeatures())
        {
            feature.ConfigureServices(services);
        }

        return services;
    }
}
