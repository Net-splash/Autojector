
using Autojector.Abstractions;

namespace Autojector.Tests.Factories;
interface ITestScopeService { }
class TestScopeService : ITestScopeService { }
internal class TestScopeFactory : IScopeFactory<ITestScopeService>
{
    public ITestScopeService GetService() => new TestScopeService();
}
