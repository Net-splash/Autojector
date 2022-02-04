## 1. Simple Injection

In case you what a service to be injected as it interface you should implement one of the three interfaces that mark the class as a service.

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

No need to add this class anywhere else. Also you can see the expected lifetype of this class right from it's definision, you don't need to search for it.



In case you want to see real examples please take a look at the [samples]((https://github.com/Net-splash/Autojector/tree/main/samples))
