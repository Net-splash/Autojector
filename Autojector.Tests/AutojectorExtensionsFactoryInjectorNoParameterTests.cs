using Autojector.Tests.FactoriesIntjectable;
using Autojector.Tests.SimpleInjectableClasses;
using InjectableClassesAssembly;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsFactoryInjectorNoParameterTests : AutojectorBaseTest
{
    public AutojectorExtensionsFactoryInjectorNoParameterTests() : base()
    {
    }

    [Fact]
    public void ShouldAddTransientServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorFactories());

        //Assert
        ServiceShouldNotBeNull<ITestTransientService>().ShouldBeAssignableTo<TestTransientService>();
    }

    [Fact]
    public void ShouldAddExtenalTransientServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorFactories(GetExternalAssemby()));

        //Assert
        ServiceShouldNotBeNull<IExternalTransientService>().ShouldBeAssignableTo<ExternalTransientService>();
    }


    [Fact]
    public void ShouldAddScopeServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorFactories());

        //Assert
        ServiceShouldNotBeNull<ITestScopeService>().ShouldBeAssignableTo<TestScopeService>();
    }

    [Fact]
    public void ShouldAddSingletonServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorFactories());

        //Assert
        ServiceShouldNotBeNull<ITestSingletonService>().ShouldBeAssignableTo<TestSingletonService>();
    }

    [Fact]
    public void ShouldNotAddSimpleInjectionsWithOnlyFactoriesEnabled()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorFactories());

        // Act & Assert
        ShouldFailOnGetService<ITestSingleton>();
        ShouldFailOnGetService<TestSingleton>();
        ShouldFailOnGetService<ITestTransient>();
        ShouldFailOnGetService<TestTransient>();
        ShouldFailOnGetService<ITestTransientDependent>();
        ShouldFailOnGetService<TestTransientDependent>();
    }


}
