using Autojector.Tests.SimpleInjectableClasses;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class SimpleInjectorInternalAssembly : TestBase
{
    public SimpleInjectorInternalAssembly() : base(
            a => a.UseSimpleInjection().Build()
        )
    {
    }

    [Fact]
    public void ShouldAddTestSingletonClass()
    {
        //Assert
        ServiceShouldSucceedLocally<TestSingleton>();
    }

    [Fact]
    public void ShouldAddTestSingletonInterface()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestSingleton>();
    }
    [Fact]
    public void ShouldAddDependentSingletonClass()
    {
        //Assert
        ServiceShouldSucceedLocally<TestDependentSingleton>();
    }

    [Fact]
    public void ShouldAddTransientClass()
    {
        //Assert
        ServiceShouldSucceedLocally<TestTransient>();
    }


    [Fact]
    public void ShouldAddTransientInterface()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestTransient>();
    }

    [Fact]
    public void ShouldAddScopedInterface()
    {
        //Assert
        ServiceShouldSucceedLocally<ITestScope>();
    }

    [Fact]
    public void ShouldAddScopedClass()
    {
        //Assert
        ServiceShouldSucceedLocally<TestScope>();
    }

    [Fact]
    public void ShouldAddTransientDependentOnSingleton()
    {
        //Assert
        var service = ServiceShouldSucceedLocally<ITestTransientDependent>();
        service.TestDependentSingleton.ShouldNotBeNull<ITestDependentSingleton>();
    }

    [Fact]
    public void ShouldAddTransientClassesWithNoInterface()
    {
        //Assert
        ServiceShouldSucceedLocally<NoInterfaceTransient>();
    }

    [Fact]
    public void ShouldFailWhenNoDoesntImplementInjectable()
    {
        //Assert
        ShouldFailOnGetServiceLocally<NoLifetimeType>();
    }
}
