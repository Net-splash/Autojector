namespace Autojector.Abstractions;

/// <summary>
/// This is the base interface of all the factories in autojector.
/// </summary>
public interface IFactory<T> : IInjectable
{
    /// <summary>
    /// This method should be implemented by any class that is registered as a factory and will resolve the dependency.
    /// </summary>
    /// <returns>
    /// Will return the resolved service (dependency).
    /// </returns>
    T GetService();
}
