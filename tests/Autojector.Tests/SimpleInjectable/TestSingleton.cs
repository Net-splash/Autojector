using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectable;

internal interface ITestSingleton : ISingleton<ITestSingleton> { }
internal class TestSingleton : ITestSingleton, ISingleton<TestSingleton> { }
