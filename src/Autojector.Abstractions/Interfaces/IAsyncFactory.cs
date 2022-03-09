namespace Autojector.Abstractions;
/// <summary>
/// This is the base interface of all the async factories in autojector.
/// </summary>
public interface IAsyncFactory<T> : IInjectable
{
    /// <summary>
    /// This method should be implemented by any class that is registered as a async factory and will resolve the dependency as IAsyncDependency.
    /// </summary>
    /// <returns>
    /// Will return the resolved service (dependency).
    /// </returns>
    Task<T> GetServiceAsync();
}
