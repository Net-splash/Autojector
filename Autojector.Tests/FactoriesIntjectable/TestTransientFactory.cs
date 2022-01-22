using Autojector.Public;

namespace Autojector.Tests.FactoriesIntjectable;
interface ITestTransientService { }
class TestTransientService : ITestTransientService { }
internal class TestTransientFactory : ITransientFactoryInjectable<ITestTransientService>
{
    public ITestTransientService GetService() => new TestTransientService();
}
