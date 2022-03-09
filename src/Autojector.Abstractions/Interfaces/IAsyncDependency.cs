namespace Autojector.Abstractions;


/// <summary>
/// This is the type registred for the dependencies that are resolved async
/// In order to use the dependency please call Value/ServiceAsync
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAsyncDependency<T>
{
    /// <summary>
    /// The same as ServiceAsync
    /// </summary>
    public Task<T> Value { get; }
    /// <summary>
    /// This will return a task that can be awaited to obtain the service
    /// </summary>
    public Task<T> ServiceAsync { get; }
}