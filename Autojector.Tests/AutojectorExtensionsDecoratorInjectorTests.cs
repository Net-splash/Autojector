using Autojector.Tests.Decorators;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsDecoratorInjectorTests : AutojectorBaseTest
{
    public AutojectorExtensionsDecoratorInjectorTests() : base()
    {
    }


    [Fact]
    public void ShouldAddDectoratorToService()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a
            .UseSimpleInjection()
            .UseFactories()
            .UseDecorator()) ;

        //Assert
        ServiceShouldNotBeNull<IUndecoratedService>().ShouldBeAssignableTo<DecoratedService>();
    }


    [Fact]
    public void ShouldAddOrderedDecorators()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(
            a =>a
            .UseSimpleInjection()
            .UseFactories()
            .UseDecorator()
            );

        //Assert
        var simpleService = ServiceShouldNotBeNull<ISimpleService>();
        simpleService.ShouldBeAssignableTo<SecondDecorator>();

        var decorated = (SecondDecorator)simpleService;
        decorated.SimpleService.ShouldBeAssignableTo<FirstDecorator>();
    }


    [Fact]
    public void ShouldAddDectoratorToFactory()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a
            .UseFactories()
            .UseSimpleInjection()
            .UseDecorator());

        //Assert
        var abstractService = ServiceShouldNotBeNull<IUndecoratedFactoryProvidedService>();
        abstractService.ShouldBeAssignableTo<UndecoratedFactoryProvidedServiceDecorator>();

        var service = (UndecoratedFactoryProvidedServiceDecorator)abstractService;
        service.UndecoratedFactoryProvidedService.ShouldBeAssignableTo<UndecoratedFactoryProvidedService>();

    }
}
