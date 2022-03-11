using Autojector.Abstractions;

namespace Sample.Services;
public interface IFactoryInjectedService2
{
    string GetData();
}

internal class FactoryInjectedService2 : IFactoryInjectedService2, ITransient<FactoryInjectedService2>
{
    public string GetData()
    {
        return "Factory2 ";
    }
}
internal class Factory2 : ITransientFactory<IFactoryInjectedService2>
{
    private IServiceProvider ServiceProvider { get; }
    public Factory2(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IFactoryInjectedService2 GetService()
    {
        return (FactoryInjectedService2)ServiceProvider.GetService(typeof(FactoryInjectedService2));
    }
}