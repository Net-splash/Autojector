using Autojector.Abstractions;

namespace Sample.Services;
public interface IFactoryInjectedService1
{
    string GetData();
}
internal class FactoryInjectedService1 : IFactoryInjectedService1
{
    public string GetData()
    {
        return "Factory1 ";
    }
}

internal class Factory1 : ITransientFactory<IFactoryInjectedService1>
{
    public IFactoryInjectedService1 GetService()
    {
        return new FactoryInjectedService1();
    }
}
