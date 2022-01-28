
using Autojector.Abstractions;
using Autojector.Tests.AsyncFactoryInjectable;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsAsyncFactoryInjectorTests : AutojectorBaseTest
{
    public AutojectorExtensionsAsyncFactoryInjectorTests() : base()
    {
    }

    [Fact]
    public async void ShouldAddAsyncTransientServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a.UseAsyncFactories());

        //Assert
        var dependency = ServiceShouldNotBeNull<IAsyncDependency<ITestTransientAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestTransientAsyncService>();
    }

    [Fact]
    public async void ShouldAddAsyncScopeServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a.UseAsyncFactories());

        //Assert
        var dependency = ServiceShouldNotBeNull<IAsyncDependency<ITestScopeAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestScopeAsyncService>();
    }

    [Fact]
    public async void ShouldAddAsyncSingletonServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a.UseAsyncFactories());

        //Assert
        var dependency = ServiceShouldNotBeNull<IAsyncDependency<ITestSingletonAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestSingletonAsyncService>();
    }
}
