namespace Autojector.Public;
public interface IFactory<T>
{
    T GetService();
}