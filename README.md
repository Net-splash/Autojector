# Automatic Injector - Autojector

Very simple library that can be used for automatic injection without registering or adding the classes in the default microsoft inversion of control container.


### Installing Autojector

Autojector is composed by 2 libraries:
1. Autojector
2. Autojector.Abstraction

#### When should you use Autojector ?
This is mostly for you startup project because this represends the setup.
You are going to call this either in your Startup class or in the place where you are adding your dependencies to IServiceCollection.

#### When should you use Autojector.Abstraction ?
Add Autojector.Abstraction to each project that contains classes that you expect to be picked up automaticly by Autojector
