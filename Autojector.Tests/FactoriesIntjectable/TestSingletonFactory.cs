using Autojector.Public;

namespace Autojector.Tests.FactoriesIntjectable;
interface ITestSingletonService { }
class TestSingletonService : ITestSingletonService { }
internal class TestSingletonFactory : ISingletonFactoryInjectable<ITestSingletonService>
{
    public ITestSingletonService GetService() => new TestSingletonService();
}
