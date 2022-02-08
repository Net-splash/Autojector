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

    [Fact]
    public void ShouldFailWithMultipleUndecoratedExternalServices()
    {
        var code = @"
        public interface IMyService{}
        public class MyService: IMyService,Autojector.Abstractions.ITransient<IMyService>{}
        public class FirstDecorator: IMyService,Autojector.Abstractions.IDecorator<IMyService>{}
        public class SecondDecorator: IMyService,Autojector.Abstractions.IDecorator<IMyService>{}
        ";

        ShouldThrowOnBuildingServicesExternally(code, "Can not have more than one unordered decorator for IMyService");
    }
}
