## 1. Simple Injection

In case you what a service to be injected as it interface you should implement one of the three interfaces that mark the class as a service.

You should implement : 
```
ITransient<T>
IScope<T>
ISingleton<T>
```
This will mark the class as an injectable service.

Implementation examples:
```
internal interface IMyService : ITransient<IMyService> {}
internal class MyService : IMyService {}
```
or 

```
internal interface IMyService {}
internal class MyService : IMyService, ITransient<IMyService> {}
```
Now you can use `IMyService` in other services and the Autojector will provide an instance of `MyService`.

```
class DependentService{
public DependentService(IMyService myService){}
}
```

No need to add this class anywhere else. Also you can see the expected lifetype of this class right from it's definision, you don't need to search for it.