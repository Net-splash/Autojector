using Autojector.Tests.SimpleInjectableClasses;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsSimpleInjectorNoParameterTests : AutojectorBaseTest
{
    public AutojectorExtensionsSimpleInjectorNoParameterTests() : base()
    {
    }

    [Fact]
    public void ShouldAddTestSingletonClass()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<TestSingleton>();
    }

    [Fact]
    public void ShouldAddTestSingletonInterface()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<ITestSingleton>();
    }
    [Fact]
    public void ShouldAddDependentSingletonClass()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<TestDependentSingleton>();
    }

    [Fact]
    public void ShouldAddTransientClass()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<TestTransient>();
    }


    [Fact]
    public void ShouldAddTransientInterface()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<ITestTransient>();
    }

    [Fact]
    public void ShouldAddScopedInterface()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<ITestScope>();
    }

    [Fact]
    public void ShouldAddScopedClass()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<TestScope>();
    }

    [Fact]
    public void ShouldAddTransientDependentOnSingleton()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        var service = ServiceShouldNotBeNull<ITestTransientDependent>();
        service.TestDependentSingleton.ShouldNotBeNull<ITestDependentSingleton>();
    }

    [Fact]
    public void ShouldAddTransientClassesWithNoInterface()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ServiceShouldNotBeNull<NoInterfaceTransient>();
    }

    [Fact]
    public void ShouldFailWhenNoDoesntImplementInjectable()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

        //Assert
        ShouldFailOnGetService<NoLifetimeType>();
    }
}