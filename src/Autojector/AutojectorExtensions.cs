﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Autojector;
public static class AutojectorExtensions
{
    /// <summary>
    /// This method will inject into services all the classes that are found by the configured feature with configureOption in all the assembies provided.
    /// </summary>
    /// <param name="services">
    /// This should be the IServiceCollection in which the autojector should inject all detected services 
    /// </param>
    /// <param name="configureOptions">
    /// This is the method that will configure all the features injected by autojector.
    /// After adding all needed features you should call the Build method that will return from autojector builder.
    /// E.g:
    /// autojectorBuilder => autojectorBuilder.UseSimpleInjection().UseFactories().Build();
    /// </param>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the AppDomain.CurrentDomain..GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Will return the same IServiceCollection received as input but modified so it will contain all autoinjected services.
    /// </returns>
    public static IServiceCollection WithAutojector(this IServiceCollection services, Func<AutojectorBuilder,IAutojectorService> configureOptions = null, params Assembly[] assemblies)
    {
        var autojectorBuilder = new AutojectorBuilder(assemblies, services);
        if(configureOptions == null)
        {
            configureOptions = (options) => options.UseSimpleInjection().Build();  
        }

        var autojectorService = configureOptions(autojectorBuilder);
        autojectorService.ConfigureServices();

        return services;
    }

    /// <summary>
    /// This method will add the autojector with all its features. 
    /// For the current version the autojector include
    /// <list type="number">
    /// <item>Simple injection</item>
    /// <item>Factories</item>
    /// <item>Async Factories</item>
    /// <item>Decorators</item>
    /// <item>Configs</item>
    /// <item>Chains</item>
    /// </list>
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
    public static IServiceCollection AddAutojector(this IServiceCollection services, params Assembly[] assemblies)
    {
        var autojectorBuilder = new AutojectorBuilder(assemblies, services);

        autojectorBuilder.UseSimpleInjection();
        autojectorBuilder.UseFactories();
        autojectorBuilder.UseAsyncFactories();
        autojectorBuilder.UseDecorator();
        autojectorBuilder.UseConfigs();
        autojectorBuilder.UseChains();
        autojectorBuilder.Build().ConfigureServices();

        return services;
    }
}