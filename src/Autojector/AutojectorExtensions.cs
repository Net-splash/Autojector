using Autojector.General;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Autojector;

/// <summary>
/// This delegate represends the method pass to configure the features Autojector features
/// </summary>
/// <param name="autojectorBuilder">An instance for IAutojectorBuilder where methods can be called to enable features</param>
public delegate void ConfigureAutojectorBuilderDelegate(IAutojectorBuilder autojectorBuilder);

/// <summary>
/// Should be used in order to add the autojector to the IServiceCollection
/// </summary>
public static class AutojectorExtensions
{
    /// <summary>
    /// This method will inject into services all the classes that are found by the configured feature with configureOption in all the assembies provided.
    /// </summary>
    /// <param name="services">
    /// This should be the IServiceCollection in which the autojector should inject all detected services 
    /// </param>
    /// <param name="configureAutojectorBuilder">
    /// This is the method that will configure all the features injected by autojector.
    /// E.g:
    /// autojectorBuilder => autojectorBuilder.UseSimpleInjection().UseFactories();
    /// </param>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Will return the same IServiceCollection received as input but modified so it will contain all autoinjected services.
    /// </returns>
    public static IServiceCollection WithAutojector(
        this IServiceCollection services,
        ConfigureAutojectorBuilderDelegate configureAutojectorBuilder,
        params Assembly[] assemblies)
    {
        configureAutojectorBuilder = configureAutojectorBuilder ?? throw new ArgumentNullException(nameof(configureAutojectorBuilder));
        var autojectorBuilder = new AutojectorBuilder(assemblies, services);
        configureAutojectorBuilder(autojectorBuilder);
        var autojectorService = autojectorBuilder.Build();
        autojectorService.ConfigureServices();

        return services;
    }
    
    /// <summary>
    /// This method will add the autojector with all its features. 
    /// </summary>
    /// <param name="services">
    ///     This should be the IServiceCollection in which the autojector should inject all detected services 
    /// </param>
    /// <param name="assemblies"> 
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the AppDomain.CurrentDomain..GetAssemblies() will be used
    /// </param>
    /// <returns>
    /// Will return the same IServiceCollection received as input but modified so it will contain all autoinjected services.
    /// </returns>
    public static IServiceCollection AddAutojector(
        this IServiceCollection services,
        params Assembly[] assemblies) => services.WithAutojector(new ConfigureAutojectorBuilderDelegate(UseAllAutojectorFeatures), assemblies);

    /// <summary>
    /// This method is used to create a ConfigureAutojectorBuilderDelegate with all the features Autojector supports
    /// </summary>
    /// <param name="autojectorBuilder">An instance for IAutojectorBuilder where methods can be called to enable features</param>
    public static void UseAllAutojectorFeatures(IAutojectorBuilder autojectorBuilder)
    {
        autojectorBuilder
            .UseSimpleInjectionByInterface()
            .UseSimpleInjectionByAttribute()
            .UseFactories()
            .UseAsyncFactories()
            .UseDecoratorByInterface()
            .UseDecoratorByAttribute()
            .UseConfigsByInteface()
            .UseConfigsByAttribute()
            .UseUnimplementedConfigsByInteface()
            .UseUnimplementedConfigsByAttribute()
            .UseChains();
    }
}
