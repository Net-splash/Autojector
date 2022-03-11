# ![Autojector](autojector-icon.png) Automatic Injector - Autojector

Very simple library that can be used for automatic injection without registering or adding the classes in the default microsoft inversion of control container.

I think that the lifetype of a class should be stated in the class definition.

The goal of this library is to make the injection more easy to use and at the same time to make design patterns more accessible.

In case you think that there is a design pattern that might be better to see more in real life please leave a comment as feedback or create a github issue and 
I will try to introduce it as much as possible into the library.

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

## 1. [Simple Injection](/simple-injection)
## 2. [Factories](/factories)
## 3. [Async Factories](/async-factories)
## 4. [Decorators](/decorators)
## 5. [Configs](/configs)
## 6. [Chains](/chains)
