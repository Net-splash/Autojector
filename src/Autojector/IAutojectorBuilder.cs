using System.Reflection;

namespace Autojector;

/// <summary>
/// This interface should be used when you want to build the instance of the Autojector
/// </summary>
public interface IAutojectorBuilder
{
    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement simple interface: ITransient\f[T\f], IScope\f[T\f], ISingleton\f[T\f]
    /// will be injected automatically without registing them anywhere.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseSimpleInjectionByInterface(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that have simple injection attribute:
    /// TransientAttribute, ScopeAttribute, SingletonAttribute
    /// will be injected automatically without registing them anywhere.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseSimpleInjectionByAttribute(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement factory interfaces: ITransientFactory\f[T\f], IScopFactorye\f[T\f], ISingletonFactory\f[T\f]
    /// will be registered as the factory for the service specifited as T.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseFactories(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement async factory interfaces: IAsyncTransientFactory\f[T\f], IAsyncScopFactorye\f[T\f], IAsyncSingletonFactory\f[T\f]
    /// will be registered as the factory for the service specifited as IAsyncDependency\f[T\f].
    /// The IAsyncDependency\f[T\f] contains a properties (Value,ServiceAsync) that return asynchronous the service.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseAsyncFactories(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that implement IDecorator\f[T\f]
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
    /// </returns>
    public IAutojectorBuilder UseDecoratorByInterface(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector where all the classes that have the attribute DecoratorAttribute
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
    /// </returns>
    public IAutojectorBuilder UseDecoratorByAttribute(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector which will register all classes that implement the IConfig interface
    /// as self services and will bind the data from Configuration to an instance of the class.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseConfigsByInteface(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector which will register all classes that are maked by ConfigAttribute
    /// as self services and will bind the data from Configuration to an instance of the class.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseConfigsByAttribute(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector which will register all interfaces that extend the IConfig interface
    /// as self services and will bind the data from Configuration to an instance of the class.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseUnimplementedConfigsByInteface(params Assembly[] assemblies);

    /// <summary>
    /// This method will add the feature of Autoinjector which will register all interfaces that are maked by ConfigAttribute
    /// as self services and will bind the data from Configuration to an instance of the class.
    /// </summary>
    /// <param name="assemblies">
    /// This should be a list with all the assembies where the autojector will search for services.
    /// If null the assemblies provided in AutojectorBuilder constructor (extension method) will be used
    /// If those are also null the AppDomain.CurrentDomain.GetAssemblies() will be used.
    /// </param>
    /// <returns>
    /// Any method from AutojectorBuilder will return the AutojectorBuilder so it can be further called.
    /// </returns>
    public IAutojectorBuilder UseUnimplementedConfigsByAttribute(params Assembly[] assemblies);
    
    /// <summary>
    /// This method will add the feature of Autoinjector will register all clases that implement IChainLink\f[TRequest,TResponse\f] 
    /// as a chain grouped by TRequest and TResponse. Will provide the class IChain\f[TRequest,TResponse\f] that expose the method Handle.
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
    /// </returns>
    public IAutojectorBuilder UseChains(params Assembly[] assemblies);

}
