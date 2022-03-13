## 4. Decorators


In case you want to enhance the functionality of a service or you just want to debugg it without afecting the services that are dependent on this one you can use this feature.
This feature will receive in the constructor the instance of the service that you want to enhance.

### By Interface
Make sure that `AddAutojector` or `UseDecoratorByInterface` was called.

You should implement only 
```c#
IDecorator<TService>
```
This will mark the class as a decorator. Please be sure to also implement `TService` as this should still have the same type.
After implementing this you will no longer receive the previous implementation of `TService` but an instance of the class that implements `IDecorator`.

Implementation Example

```c#
public interface ISimpleInjectedDecoratedService
{
    string GetData();
}
internal class SimpleInjectedDecoratedService : ISimpleInjectedDecoratedService, ITransient<ISimpleInjectedDecoratedService>
{
    public string GetData()
    {
        return "SimpleInjectedDecoratedServiceData";
    }
}

internal class DecoratedService : IDecorator<ISimpleInjectedDecoratedService>, ISimpleInjectedDecoratedService
{
    private ISimpleInjectedDecoratedService SimpleInjectedDecoratedService { get; }
    public DecoratedService(ISimpleInjectedDecoratedService simpleInjectedDecoratedService)
    {
        SimpleInjectedDecoratedService = simpleInjectedDecoratedService;
    }

    public string GetData()
    {
        return SimpleInjectedDecoratedService.GetData() + " Decorated";
    }
}
```
In this case the value of your `simpleInjectedDecoratedService` that is injected in the `DecoratedService` will be actually an instance of `SimpleInjectedDecoratedService`.

The process of using composition for the decorator can continue if you really have to like this 
```c#

[DecoratorOrder(2)]
internal class DecoratedService2 : IDecorator<ISimpleInjectedDecoratedService>, ISimpleInjectedDecoratedService
{
    private ISimpleInjectedDecoratedService SimpleInjectedDecoratedService { get; }
    public DecoratedService2(ISimpleInjectedDecoratedService simpleInjectedDecoratedService)
    {
        SimpleInjectedDecoratedService = simpleInjectedDecoratedService;
    }

    public string GetData()
    {
        return SimpleInjectedDecoratedService.GetData() + " Decorated";
    }
}
```
Just keep in mind that for the same IDecorator<ISimpleInjectedDecoratedService> you can have only one service without the `DecoratorOrder` attribute.
In this second case you will receive an intance of `DecoratedService` as `simpleInjectedDecoratedService` in the constructor.

You can also decorate services that are resolved by factories like this 
```c#
internal interface IUndecoratedFactoryProvidedService{}

internal class UndecoratedFactoryProvidedService : IUndecoratedFactoryProvidedService{}

internal class UndecoratedFactoryProvidedServiceFactory : ITransientFactory<IUndecoratedFactoryProvidedService>
{
    public UndecoratedFactoryProvidedServiceFactory(){}
    public IUndecoratedFactoryProvidedService GetService()
    {
        return new UndecoratedFactoryProvidedService();
    }
}

internal class UndecoratedFactoryProvidedServiceDecorator : IDecorator<IUndecoratedFactoryProvidedService>, 
    IUndecoratedFactoryProvidedService
{
    public UndecoratedFactoryProvidedServiceDecorator(IUndecoratedFactoryProvidedService undecoratedFactoryProvidedService)
    {
        UndecoratedFactoryProvidedService = undecoratedFactoryProvidedService;
    }

    public IUndecoratedFactoryProvidedService UndecoratedFactoryProvidedService { get; }
}
```
In this case the value of `UndecoratedFactoryProvidedService` in `UndecoratedFactoryProvidedServiceDecorator` will actually be an instance of `UndecoratedFactoryProvidedService`


### By attribute

Mark the class as a decorator with the `DecoratorAttribute`.

Make sure that `AddAutojector` or `UseDecoratorByAttribute` was called.

Here is an implementation example by attribute

Implementation Example
```c#
public interface ISimpleInjectedDecoratedByAttributeService
{
    string GetData();
}

[Transient(typeof(ISimpleInjectedDecoratedByAttributeService))]
internal class SimpleInjectedDecoratedByAttributeService : ISimpleInjectedDecoratedByAttributeService
{
    public string GetData()
    {
        return "SimpleInjectedDecoratedByAttributeService";
    }
}

[Decorator(typeof(ISimpleInjectedDecoratedByAttributeService))]
internal class DecoratorByAttributeService : ISimpleInjectedDecoratedByAttributeService
{
    private ISimpleInjectedDecoratedByAttributeService SimpleInjectedDecoratedByAttributeService { get; }
    public DecoratorByAttributeService(ISimpleInjectedDecoratedByAttributeService simpleInjectedDecoratedByAttributeService)
    {
        SimpleInjectedDecoratedByAttributeService = simpleInjectedDecoratedByAttributeService;
    }

    public string GetData()
    {
        return SimpleInjectedDecoratedByAttributeService.GetData() + " Decorated";
    }
}
```