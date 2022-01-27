using Autojector.Tests.Decorators;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsDecoratorInjectorNoParameterTests : AutojectorBaseTest
{
    public AutojectorExtensionsDecoratorInjectorNoParameterTests() : base()
    {
    }


    [Fact]
    public void ShouldAddDectoratorToService()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(a => a
            .UseAutojectorSimpleInjection()
            .UseAutojectorDecorator()) ;

        //Assert
        ServiceShouldNotBeNull<IUndecoratedService>().ShouldBeAssignableTo<DecoratedService>();
    }

    [Fact]
    public void ShouldAddOrderedDecorators()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector(
            a =>a
            .UseAutojectorSimpleInjection()
            .UseAutojectorDecorator()
            );

        //Assert
        var simpleService = ServiceShouldNotBeNull<ISimpleService>();
        simpleService.ShouldBeAssignableTo<SecondDecorator>();

        var decorated = (SecondDecorator)simpleService;
        decorated.SimpleService.ShouldBeAssignableTo<FirstDecorator>();
    }
}
