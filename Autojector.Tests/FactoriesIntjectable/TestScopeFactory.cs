using Autojector.Public;

namespace Autojector.Tests.FactoriesIntjectable;
interface ITestScopeService { }
class TestScopeService : ITestScopeService { }
internal class TestScopeFactory : IScopeFactoryInjectable<ITestScopeService>
{
    public ITestScopeService GetService() => new TestScopeService();
}
