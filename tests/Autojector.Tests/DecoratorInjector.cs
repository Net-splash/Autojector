using Autojector.Tests.Decorators;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class DecoratorInjector : TestBase
{
    public DecoratorInjector() : base(a => a
           .UseSimpleInjection()
           .UseFactories()
           .UseDecorator()
           .Build())
    {
    }


    [Fact]
    public void ShouldAddDectoratorToService()
    {
        //Assert
        ServiceShouldSucceedLocally<IUndecoratedService>().ShouldBeAssignableTo<DecoratedService>();
    }


    [Fact]
    public void ShouldAddOrderedDecorators()
    {
        //Assert
        var simpleService = ServiceShouldSucceedLocally<ISimpleService>();
        simpleService.ShouldBeAssignableTo<SecondDecorator>();

        var decorated = (SecondDecorator)simpleService;
        decorated.SimpleService.ShouldBeAssignableTo<FirstDecorator>();
    }


    [Fact]
    public void ShouldAddDectoratorToFactory()
    {
        //Assert
        var abstractService = ServiceShouldSucceedLocally<IUndecoratedFactoryProvidedService>();
        abstractService.ShouldBeAssignableTo<UndecoratedFactoryProvidedServiceDecorator>();

        var service = (UndecoratedFactoryProvidedServiceDecorator)abstractService;
        service.UndecoratedFactoryProvidedService.ShouldBeAssignableTo<UndecoratedFactoryProvidedService>();

    }
}
