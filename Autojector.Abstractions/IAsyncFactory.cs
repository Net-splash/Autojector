namespace Autojector.Abstractions;

public interface IAsyncFactory<T> : IInjectable
{
    Task<T> GetServiceAsync();
}
