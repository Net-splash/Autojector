using Autojector.Public;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestTransient : ITransientInjectable<ITestTransient> { }
internal class TestTransient : ITestTransient, ITransientInjectable<TestTransient> { }


