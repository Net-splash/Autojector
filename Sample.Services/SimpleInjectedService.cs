
using Autojector.Abstractions;

namespace Sample.Services;
public interface ISimpleInjectedService
{
    string GetData();
}

internal class SimpleInjectedService : ISimpleInjectedService, ITransient<ISimpleInjectedService>
{
    public string GetData()
    {
        return "SomeData";
    }
}
