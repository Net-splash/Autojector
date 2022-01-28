using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestTransientDependent : ITransient<ITestTransientDependent>
{
    public TestDependentSingleton TestDependentSingleton { get; }
}
internal class TestTransientDependent : ITestTransientDependent
{
    public TestTransientDependent(TestDependentSingleton testDependentSingleton)
    {
        TestDependentSingleton = testDependentSingleton;
    }

    public TestDependentSingleton TestDependentSingleton { get; }
}
