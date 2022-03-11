using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectable;

internal class TestScope : ITestScope, IScope<TestScope> { }
internal interface ITestScope : IScope<ITestScope> { }
