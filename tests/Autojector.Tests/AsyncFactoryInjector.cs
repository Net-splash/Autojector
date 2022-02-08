
using Autojector.Abstractions;
using Autojector.Tests.AsyncFactory;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AsyncFactoryInjector : TestBase
{
    public AsyncFactoryInjector() : base(a => a.UseAsyncFactories().Build())
    {
    }

    [Fact]
    public async void ShouldAddAsyncTransientServiceFromFactory()
    {
        //Assert
        var dependency = ServiceShouldSucceedLocally<IAsyncDependency<ITestTransientAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestTransientAsyncService>();
    }

    [Fact]
    public async void ShouldAddAsyncScopeServiceFromFactory()
    {
        //Assert
        var dependency = ServiceShouldSucceedLocally<IAsyncDependency<ITestScopeAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestScopeAsyncService>();
    }

    [Fact]
    public async void ShouldAddAsyncSingletonServiceFromFactory()
    {
        //Assert
        var dependency = ServiceShouldSucceedLocally<IAsyncDependency<ITestSingletonAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestSingletonAsyncService>();
    }
}
