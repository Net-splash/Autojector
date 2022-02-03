using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestSingleton : ISingleton<ITestSingleton> { }
internal class TestSingleton : ITestSingleton, ISingleton<TestSingleton> { }
