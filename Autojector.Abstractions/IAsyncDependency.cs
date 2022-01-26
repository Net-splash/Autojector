namespace Autojector.Abstractions;

public interface IAsyncDependency<T>
{
    public Task<T> Value { get; }
    public Task<T> ServiceAsync => Value;
}