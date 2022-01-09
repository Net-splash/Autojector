using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;

namespace Autojector.Tests;
public class AutojectorBaseTest
{
    protected ServiceCollection ServiceCollection;
    public AutojectorBaseTest()
    {
        ServiceCollection = new ServiceCollection();
    }

    protected T ServiceShouldNotBeNull<T>() where T : class
    {
        var serviceProvider = ServiceCollection.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<T>();
        service.ShouldNotBeNull();
        return service;
    }

    protected void ShouldFailOnGetService<T>() where T: class
    {
        var serviceProvider = ServiceCollection.BuildServiceProvider();
        Should.Throw<Exception>(() => serviceProvider.GetRequiredService<T>());
    }

    protected Assembly GetExternalAssemby() => GetAssemblyByName("InjectableClassesAssembly");

    protected Assembly GetAssemblyByName(string name)
    {
        return AppDomain.CurrentDomain.GetAssemblies().
               SingleOrDefault(assembly => assembly.GetName().Name == name);
    }
}
