using Autojector.Abstractions;

namespace Autojector.Tests.Factories;
interface ITestTransientService { }
class TestTransientService : ITestTransientService { }
internal class TestTransientFactory : ITransientFactory<ITestTransientService>
{
    public ITestTransientService GetService() => new TestTransientService();
}
