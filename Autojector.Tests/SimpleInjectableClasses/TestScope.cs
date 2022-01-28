using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal class TestScope : ITestScope, IScope<TestScope> { }
internal interface ITestScope : IScope<ITestScope> { }
