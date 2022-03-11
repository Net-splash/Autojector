
using Autojector.Abstractions;

namespace Sample.Services;
public interface IAsyncFactoryInjectedService
{
    string GetData();
}
internal class AsyncFactoryInjectedService : IAsyncFactoryInjectedService
{
    public string GetData()
    {
        return "AsyncFactoryInjectedServiceData";
    }
}

public class AsyncFactory : IAsyncTransientFactory<IAsyncFactoryInjectedService>
{
    public async Task<IAsyncFactoryInjectedService> GetServiceAsync()
    {
        await Task.Delay(1000);
        return new AsyncFactoryInjectedService();
    }
}
