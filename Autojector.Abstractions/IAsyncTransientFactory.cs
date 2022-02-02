namespace Autojector.Abstractions;
/// <summary>
/// This interface should be use to mark any class as a provider for the service
/// In order to be picked up by the Autojector please make sure that the AsynFactories was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// This will provide a IAsyncDependency<<typeparamref name="T"/>> which provide Value/ServiceAsync as a Task to retive the service
/// </summary>
/// <typeparam name="T">
/// This is the type of the service that you expect to be injected as. This can be an abstract class or an interface.
/// Make sure that your class will also implement that interface or extend that class.
/// </typeparam>
public interface IAsyncTransientFactory<T> : IAsyncFactory<T> { }
