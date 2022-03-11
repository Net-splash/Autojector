
using Autojector.Abstractions;

namespace Autojector.Tests.Factories;
interface ITestSingletonService { }
class TestSingletonService : ITestSingletonService { }
internal class TestSingletonFactory : ISingletonFactory<ITestSingletonService>
{
    public ITestSingletonService GetService() => new TestSingletonService();
}
