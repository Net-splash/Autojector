
## 2. Async Factories

In case your service should be resolved at by a more complex logic and ca not be simply added with only one implmentation you can use this feature.
This feature will provide your service in a wrapper called `IAsyncDependency` as the call stack will be async.

You should implement 
```c#
IAsyncTransientFactory<TService>
IAsyncScopFactorye<TService>
IAsyncSingletonFactory<TService>
```
This will make the class as an async factory and any `TService` will be injected using the method `GetServiceAsync` implemented by the async factory.
You can inject your service after that like `IAsyncDependency<TService>` in any constructor.

Implementation examples:
```c#
interface IMyService {}

class MyService1 : IMyService {}

class MyService2: IMyService {}

class MyServiceFactory: IAsyncTransientFactory<IMyService>{
  public async Task<IMyService> GetServiceAsync(){
    await Task.Deplay(5000); //  or any other long operation

    if(...) return new MyService1();
    return return new MyService2();
  }
}
```
Now anywhere `IAsyncDependency<IMyService>` is requested it will actually inject the `MyService1` or `MyService2`.

If `MyService1` or `MyService2` have some dependencies themself you can use the Simple Injection like: 
```c#
interface IMyService {}

class MyService1 : IMyService, ITransient<MyService1> {}

class MyService2: IMyService, ITransient<MyService2> {}

class MyServiceFactory: IAsyncTransientFactory<IMyService>{
  public MyServiceFactory(IServiceProvider serviceProvider)
  public async Task<IMyService> GetServiceAsync(){
    await Task.Deplay(5000); //  or any other long operation
    if(...) return serviceProvider.GetRequiredService<MyService1>();
    return return serviceProvider.GetRequiredService<MyService2>();
  }
}
```
Now you can use `IAsyncDependency<IMyService>` in other services and the Autojector will provide an instance of `IAsyncDependency<IMyService>`.

```c#
class DependentService{
  public DependentService(IAsyncDependency<IMyService> myServiceDependency){}
}
```

In order to get your service you should call `ServiceAsync` or `Value` like:
```c#
class DependentService{
    private IAsyncDependency<IMyService> _myServiceDependency;
    
    public DependentService(IAsyncDependency<IMyService> myServiceDependency){}
    }

    public async Task DoStuffAsync(){

        var service = await _myServiceDependency.ServiceAsync;
    }
}
```
In this case the method call `GetServiceAsync` from the factory will not be called until you await `ServiceAsync` or `Value`.
