
using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectable;

internal interface ITestDependentSingleton : ISingleton<TestDependentSingleton> { }
internal class TestDependentSingleton : ITestDependentSingleton
{
    public TestDependentSingleton(ITestSingleton testSingleton)
    {
        TestSingleton = testSingleton;
    }

    public ITestSingleton TestSingleton { get; }
}