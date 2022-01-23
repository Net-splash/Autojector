namespace Autojector.Public;
public interface IFactory<T> : IInjectable
{
    T GetService();
}