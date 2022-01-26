namespace Autojector.Abstractions;
public interface IFactory<T> : IInjectable
{
    T GetService();
}
