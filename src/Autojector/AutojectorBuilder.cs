using Autojector.Base;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Base;
using Autojector.Features.Decorators;
using Autojector.Features.Factories;
using Autojector.Features.SimpleInjection;
using Autojector.Registers.Chains;
using Autojector.Registers.Configs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector;
public class AutojectorBuilder
{
    internal AutojectorBuilder(Assembly[] assemblies, IServiceCollection services)
    {
        Features = new List<IAutojectorFeature>();
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }
        Assemblies = assemblies;
        Services = services;
    }
    private List<IAutojectorFeature> Features { get; }
    private Assembly[] Assemblies { get; }
    private IServiceCollection Services { get; }

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement simple interface: ITransient<T>, IScope<T>, ISingleton<T>
    /// will be injected automatically without registing them anywhere.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseSimpleInjection(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorSimpleInjectionFeature(assemblies, Services));
        return this;
    }

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement factory interfaces: ITransientFactory<T>, IScopFactorye<T>, ISingletonFactory<T>
    /// will be registered as the factory for the service specifited as T.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorFactoriesFeature(assemblies, Services));
        return this;
    }

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement async factory interfaces: IAsyncTransientFactory<T>, IAsyncScopFactorye<T>, IAsyncSingletonFactory<T>
    /// will be registered as the factory for the service specifited as IAsyncDependency<T>.
    /// The IAsyncDependency<T> contains a properties (Value,ServiceAsync) that return asynchronous the service.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseAsyncFactories(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorAsyncFactoriesFeature(assemblies, Services));
        return this;
    }

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement IDecorator<T>
    /// will take the place of the currently existing service and will receive, if requested, an instance of that service.
    /// To decorate a decorator you should add DecoratorOrderAttribute on the second class or any other after that
    /// to make sure in which the decorator is used
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseDecorator(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorDecoratorsFeature(assemblies, Services));
        return this;
    }

    /// <summary>
    /// This method will add the feature of Autoinjector which will register all clases that implement the IConfig interface
    /// as self services and will bind the data from Configuration to an instance of the class.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseConfigs(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorConfigsFeature(assemblies, Services));
        return this;
    }

    /// <summary>
    /// This method will add the feature of Autoinjector will register all clases that implement IChainLink<TRequest,TResponse> 
    /// as a chain grouped by TRequest and TResponse. Will provide the class IChain<TRequest,TResponse> that expose the method Handle.
    /// That dependnecy will search for the first IChainLink that can be used to handle the request and will return the response from the first one that can.
    /// You can use ChainLinkOrderAttribute to ensure the order in which a the ChainLink will be called
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// Only exception is the Build method.
    /// </returns>
    public AutojectorBuilder UseChains(params Assembly[] assemblies)
    {
        assemblies = GetAssemblies(assemblies);
        Features.Add(new AutojectorChainsFeature(assemblies, Services));
        return this;
    }

    public IAutojectorService Build()
    {
        ValidateAgainstOnlyDecoratorAdded();
        var orderedFeatures = GetAutojectorFeatures();
        return new AutojectorService(orderedFeatures);
    }

    private void ValidateAgainstOnlyDecoratorAdded()
    {
        var decoratorFeatures = Features.Where(f => f.GetType() == typeof(AutojectorDecoratorsFeature));
        if(decoratorFeatures.Count() == Features.Count())
        {
            throw new InvalidOperationException(@$"You can't have only decorators feature because there will be nothing to decorate. 
                            Please make sure to call other methods from builder beside UseDecorator");
        }
    }

    private IEnumerable<IAutojectorFeature> GetAutojectorFeatures()
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
