namespace Autojector.Abstractions;


/// <summary>
/// This is the type registred for the dependencies that are resolved async
/// In order to use the dependency please call Value/ServiceAsync
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAsyncDependency<T>
{
    public Task<T> Value { get; }
    public Task<T> ServiceAsync { get; }
}