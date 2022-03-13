## 1. Simple Injection


### By implementing interface
In case you what a service to be injected as it's interface you should implement one of the three interfaces that mark the class as a service.
Make sure that `AddAutojector` or `UseSimpleInjectionByInterface` was called.
You should implement : 
```c#
ITransient<T>
IScope<T>
ISingleton<T>
```
This will mark the class as an injectable service.

Implementation examples:
```c#
internal interface IMyService : ITransient<IMyService> {}
internal class MyService : IMyService {}
```
or 

```c#
internal interface IMyService {}
internal class MyService : IMyService, ITransient<IMyService> {}
```
Now you can use `IMyService` in other services and the Autojector will provide an instance of `MyService`.

```c#
class DependentService{
  public DependentService(IMyService myService) {}
}
```

In case you need to add a service as self:
```c#
internal class MyService : ITransient<MyService> {}
```


### By class attribute
Make sure that `AddAutojector` or `UseSimpleInjectionByAttribute` was called.

You should add any of the following attributes: 
```c#
TransientAttribute
ScopeAttribute
SingletonAttribute
```
This will mark the class as an injectable service.

Implementation examples:
```c#
public interface ISimpleInjectedByAttribute
{
    public string GetData();
}

[TransientAttribute(typeof(ISimpleInjectedByAttribute))]
internal class SimpleInjectedByAttribute : ISimpleInjectedByAttribute
{
    public string GetData()
    {
        return "SimpleInjectedByAttribute";
    }
}
```

Now you can use `IMyService` in other services and the Autojector will provide an instance of `MyService`.

```c#
class DependentService{
  public DependentService(IMyService myService) {}
}
```


No need to add this class anywhere else. Also you can see the expected lifetype of this class right from it's definision, you don't need to search for it.


In case you want to see real examples please take a look at the [samples](https://github.com/Net-splash/Autojector/tree/main/samples)
