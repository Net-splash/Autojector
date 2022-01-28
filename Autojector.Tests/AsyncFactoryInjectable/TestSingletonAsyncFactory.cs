using Autojector.Abstractions;
using System.Threading.Tasks;

namespace Autojector.Tests.AsyncFactoryInjectable;
interface ITestSingletonAsyncService { }
class TestSingletonAsyncService : ITestSingletonAsyncService { }

internal class TestSingletonAsyncFactory : IAsyncSingletonFactory<ITestSingletonAsyncService>
{
    public async Task<ITestSingletonAsyncService> GetServiceAsync()
    {
        await Task.Delay(1);
        return new TestSingletonAsyncService();
    }
}
