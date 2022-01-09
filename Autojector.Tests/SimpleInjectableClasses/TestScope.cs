using Autojector.Public;

namespace Autojector.Tests.SimpleInjectableClasses;

internal class TestScope : ITestScope, IScopeInjectable<TestScope> { }
internal interface ITestScope : IScopeInjectable<ITestScope> { }
