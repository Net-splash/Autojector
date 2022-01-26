
using Autojector.Abstractions;
using Autojector.Tests.AsyncFactoryInjectable;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsAsyncFactoryInjectorNoParameterTests : AutojectorBaseTest
{
    public AutojectorExtensionsAsyncFactoryInjectorNoParameterTests() : base()
    {
    }


    [Fact]
    public async void ShouldAddAsyncTransientServiceFromFactory()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a.UseAutojectorAsyncFactories());

        //Assert
        var dependency = ServiceShouldNotBeNull<IAsyncDependency<ITestTransientAsyncService>>();
        var service = await dependency.ServiceAsync;
        service.ShouldBeAssignableTo<TestTransientAsyncService>();
    }
}
