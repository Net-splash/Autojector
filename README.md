

# ![Autojector](https://github.com/Net-splash/Autojector/blob/main/autojector-icon.png) Automatic Injector - Autojector

Very simple library that can be used for automatic injection without registering or adding the classes in the default microsoft inversion of control container.


### When should you use them

Autojector is composed by 2 libraries:
1. Autojector
2. Autojector.Abstraction

#### When should you use Autojector ?
This is mostly for you startup project because this represends the setup.
You are going to call this either in your Startup class or in the place where you are adding your dependencies to IServiceCollection.

#### When should you use Autojector.Abstraction ?
Add Autojector.Abstraction to each project that contains classes that you expect to be picked up automaticly by Autojector

### Installing

The same as is discribed on the NuGet.com you should install :
1. Autojector like it is discribed in [here](https://www.nuget.org/packages/Autojector/):
```
Install-Package Autojector
```
```
dotnet add package Autojector
```
2. Autojector.Abstraction like it is discribed in [here](https://www.nuget.org/packages/Autojector.Abstraction/):
```
Install-Package Autojector.Abstraction
```
```
dotnet add package Autojector.Abstraction
```

### How should you use it ?

In order for Autojector to work you will need to first call the use of Autojector.
You can do that by simply calling:
```
services.AddAutojector()
```
This will add the Autojector with all it's features from the current version.
You can also pass the Assemblies in which Autojector should search for services to be injected like 
```
services.AddAutojector(assembly1,assembly2,...);
```
In case you want only some features from Autojector without adding all of them you can call
```
services.WithAutojector(autojectorBuilder => autojectorBuilder.UseSimpleInjection());
```
You can also pass assemblies to the `WithAutojector` method or `UseSimpleInjection`. 

### Where does it loads that services from ?

If some assemblies are passed to `AddAutojector` or `WithAutojector` the services will be searched here.
If some assemblies are passed to `Use<*Feature>` the services will be searched here ignoring what is in `AddAutojector` or `WithAutojector`
If no assembly is passed anywhere `AppDomain.CurrentDomain..GetAssemblies()` will be used.

##Features

### 1. Simple Injection

In case you what a service to be injected as it interface you should implement one of the three interfaces that mark the class as a service.

You should implement : 
```
ITransient<T>
IScope<T>
ISingleton<T>
```
This will mark the class as an injectable service:
```
internal interface IMyService : ITransient<IMyService>
{
}
internal class MyService : IMyService
{
}
```
or 

```
internal interface IMyService
{
}

internal class MyService : IMyService, ITransient<IMyService>
{
}
```
Now you can use `IMyService` in other services and the Autojector will provide an instance of `MyService`.

No need to add this class anywhere else. Also you can see the expected lifetype of this class right from it's definision, you don't need to search for it




