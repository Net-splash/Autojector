# ![Autojector](https://github.com/Net-splash/Autojector/blob/main/autojector-icon.png) Automatic Injector - Autojector

Very simple library that can be used for automatic injection without registering or adding the classes in the default microsoft inversion of control container.

I think that the lifetype of a class should be stated in the class definition.

### Composition
Autojector is composed from 2 libraries:
1. Autojector
2. Autojector.Abstraction

#### When should you use Autojector ?
This is mostly for you startup project because this represends the setup.
You are going to call this either in your Startup class or in the place where you are adding your dependencies to IServiceCollection.

#### When should you use Autojector.Abstraction ?
Add Autojector.Abstraction to each project that contains classes that you expect to be picked up automaticly by Autojector

### Installing

1. Autojector like it is described in [here](https://www.nuget.org/packages/Autojector/):
```
Install-Package Autojector
```
```
dotnet add package Autojector
```
2. Autojector.Abstraction like it is described in [here](https://www.nuget.org/packages/Autojector.Abstraction/):
```
Install-Package Autojector.Abstraction
```
```
dotnet add package Autojector.Abstraction
```

### How should you use it ?
In order for Autojector to work you will need to first call the use of Autojector.
You can do that by simply calling:
```c#
services.AddAutojector()
```
This will add the Autojector with all it's features from the current version.
You can also pass the Assemblies in which Autojector should search for services to be injected like 
```c#
services.AddAutojector(assembly1,assembly2,...);
```
In case you want only some features from Autojector without adding all of them you can call
```c#
services.WithAutojector(autojectorBuilder => autojectorBuilder.UseSimpleInjection());
```
You can also pass assemblies to the `WithAutojector` method or `UseSimpleInjection`. 

### Where does it loads that services from ?
If some assemblies are passed to `AddAutojector` or `WithAutojector` the services will be searched here.

If some assemblies are passed to `Use<*Feature>` the services will be searched here ignoring what is in `AddAutojector` or `WithAutojector`

If no assembly is passed anywhere `AppDomain.CurrentDomain..GetAssemblies()` will be used.

### Examples

You can take a look of how the Autojector works in this simple solution with the minimum here

## [Autojector Examples](https://github.com/Net-splash/Autojector/tree/main/samples)

# Features

## 1. [Simple Injection](/Autojector/simple-injection)
In case you what a service to be injected as it interface you should implement one of the three interfaces that mark the class as a service.

You should implement any of the following: 
```c#
ITransient<T>
IScope<T>
ISingleton<T>
```
This will mark the class as an injectable service.

Please read more about Simple injection [here](/Autojector/simple-injection)

## 2. [Factories](/Autojector/factories)
In case your service should be resolved at by a more complex logic and ca not be simply added with only one implmentation you can use this feature.

You should implement any of the following
```c#
ITransientFactory<T>
IScopFactorye<T>
ISingletonFactory<T>
```
This will make the class as a factory and any `TService` will be injected using the method `GetService` implemented by the factory.

Please read more about Factories [here](/Autojector/factories)

## 3. [Async Factories](/Autojector/async-factories)
In case your service should be resolved at by a more complex logic and ca not be simply added with only one implmentation you can use this feature.
This feature will provide your service in a wrapper called `IAsyncDependency` as the call stack will be async.

You should implement any of the following
```c#
IAsyncTransientFactory<TService>
IAsyncScopFactorye<TService>
IAsyncSingletonFactory<TService>
```
This will make the class as an async factory and any `TService` will be injected using the method `GetServiceAsync` implemented by the async factory.
You can inject your service after that like `IAsyncDependency<TService>` in any constructor.

Please read more about Async factories [here](/Autojector/async-factories)

## 4. [Decorators](/Autojector/decorators)
In case you want to enhance the functionality of a service or you just want to debuggit without afecting the services that are dependent on this one you can use this feature.
This feature will receive in the constructor the instance of the service that you want to enhance.

You should implement only 
```c#
IDecorator<TService>
```
This will mark the class as a decorator. Please be sure to also implement `TService` as this should still have the same type.
After implementing this you will no longer receive the previous implementation of `TService` but an instance of the class that implements `IDecorator`.

Please read more about Decorators [here](/Autojector/decorators)

## 5. [Configs](/Autojector/configs)
In case you just want to bind the data from `IConfiguration` to an instance of a class you can declare your class as a config.
This feature will allow you to just create a class for the data that you have in config and use it.

You should implement only
```c#
IConfig
```
This will mark the class as a config and will populate that class when it's requested in a constructor.

Please read more about configs [here](/Autojector/configs)

## 6. [Chains](/Autojector/chains)
This is a more advance feature.
In case you need a set of chains of responsability that will handle your request you can use this feature
This feature will allow you to determine which implementation of the chain is the best to use.

You should implement
```c#
IChainLink<TRequest,TResponse>
```

Please read more about chains [here](/Autojector/chains)
