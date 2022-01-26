
using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestDependentSingleton : ISingletonInjectable<TestDependentSingleton> { }
internal class TestDependentSingleton : ITestDependentSingleton
{
    public TestDependentSingleton(ITestSingleton testSingleton)
    {
        TestSingleton = testSingleton;
    }

    public ITestSingleton TestSingleton { get; }
}