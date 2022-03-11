using Autojector.Abstractions;
using System.Threading.Tasks;

namespace Autojector.Tests.AsyncFactory;

interface ITestScopeAsyncService { }
class TestScopeAsyncService : ITestScopeAsyncService { }

internal class TestScopeAsyncFactory : IAsyncScopeFactory<ITestScopeAsyncService>
{
    public async Task<ITestScopeAsyncService> GetServiceAsync()
    {
        await Task.Delay(1);
        return new TestScopeAsyncService();
    }
}
