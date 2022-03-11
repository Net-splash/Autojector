## 2. Factories

In case your service should be resolved at by a more complex logic and ca not be simply added with only one implmentation you can use this feature.

You should implement 
```
ITransientFactory<T>
IScopFactorye<T>
ISingletonFactory<T>
```
This will make the class as a factory and any `TService` will be injected using the method `GetService` implemented by the factory.

Implementation examples:
```c#
interface IMyService {}

class MyService1 : IMyService {}

class MyService2: IMyService {}

class MyServiceFactory: ITransientFactory<IMyService>{
  public IMyService GetService(){
    if(...) return new MyService1();
    return return new MyService2();
  }
}
```
Now anywhere `IMyService` is requested it will actually inject the `MyService1` or `MyService2`.

If `MyService1` or `MyService2` have some dependencies themself you can use the Simple Injection like: 
```c#
interface IMyService {}

class MyService1 : IMyService, ITransient<MyService1> {}

class MyService2: IMyService, ITransient<MyService2> {}

class MyServiceFactory: ITransientFactory<IMyService>{
  public MyServiceFactory(IServiceProvider serviceProvider)
  public IMyService GetService(){
    if(...) return serviceProvider.GetRequiredService<MyService1>();
    return return serviceProvider.GetRequiredService<MyService2>();
  }
}
```
Now you can use `IMyService` in other services and the Autojector will provide an instance of `MyService`.

```c#
class DependentService{
  public DependentService(IMyService myService){}
}
```
