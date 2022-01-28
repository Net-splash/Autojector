using Autojector.Abstractions;
using System.Threading.Tasks;

namespace Autojector.Tests.AsyncFactoryInjectable;

interface ITestTransientAsyncService { }
class TestTransientAsyncService : ITestTransientAsyncService { }

internal class TestTransientAsyncFactory : IAsyncTransientFactory<ITestTransientAsyncService>
{
    public async Task<ITestTransientAsyncService> GetServiceAsync()
    {
        await Task.Delay(1);
        return new TestTransientAsyncService();
    }
}
