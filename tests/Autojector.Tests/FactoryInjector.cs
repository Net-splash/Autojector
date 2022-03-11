using Autojector.Tests.Factories;
using Autojector.Tests.SimpleInjectable;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class FactoryInjector : TestBase
{
    public FactoryInjector() : base(a => a.UseFactories())
    {
    }

    [Fact]
    public void ShouldAddTransientServiceFromFactory()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestTransientService>().ShouldBeAssignableTo<TestTransientService>();
    }

    [Fact]
    public void ShouldAddScopeServiceFromFactory()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestScopeService>().ShouldBeAssignableTo<TestScopeService>();
    }

    [Fact]
    public void ShouldAddSingletonServiceFromFactory()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestSingletonService>().ShouldBeAssignableTo<TestSingletonService>();
    }

    [Fact]
    public void ShouldNotAddSimpleInjectionsWithOnlyFactoriesEnabled()
    {
        // Act & Assert
        ShouldFailOnGetServiceLocally<ITestSingleton>();
        ShouldFailOnGetServiceLocally<TestSingleton>();
        ShouldFailOnGetServiceLocally<ITestTransient>();
        ShouldFailOnGetServiceLocally<TestTransient>();
        ShouldFailOnGetServiceLocally<ITestTransientDependent>();
        ShouldFailOnGetServiceLocally<TestTransientDependent>();
    }
}
