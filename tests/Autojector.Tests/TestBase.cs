using Autojector.TestAssemblyGenerator;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;

namespace Autojector.Tests;
public class TestBase
{
    protected IServiceCollection ServiceCollection;
    protected Func<IAutojectorBuilder, IAutojectorService> ConfigureOptions { get; }
    protected TestBase(Func<IAutojectorBuilder, IAutojectorService> configureOptions) : base()
    {
        ServiceCollection = new ServiceCollection();
        ConfigureOptions = configureOptions;
    }

    protected T ServiceShouldSucceedLocally<T>() where T : class
    {
        ServiceCollection.WithAutojector(ConfigureOptions, AssembliesManager.GetNonRuntimeAssemblies().ToArray());
        var serviceProvider = ServiceCollection.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<T>();
        service.ShouldNotBeNull();
        return service;
    }

    protected void ShouldFailOnGetServiceLocally<T>() where T: class
    {
        ServiceCollection.WithAutojector(ConfigureOptions, AssembliesManager.GetNonRuntimeAssemblies().ToArray());
        var serviceProvider = ServiceCollection.BuildServiceProvider();
        Should.Throw<Exception>(() => serviceProvider.GetRequiredService<T>());
    }


    protected void ShouldThrowOnBuildingServicesExternally(string code, string exceptionMessage = null)
    {
        using (ITestAssemblyContext testAssemblyContext = AssembliesManager.GetAssemblyContextFromCode(code))
        {
            var assembly = testAssemblyContext.Assembly;
            var exception = Should.Throw<Exception>(() => ServiceCollection.WithAutojector(ConfigureOptions, assembly));
            if (exceptionMessage != null)
            {
                exception.Message.ShouldBe(exceptionMessage);
            }
            ServiceCollection.Clear();
        }
    }

    protected void ShouldThrowExceptionOnGettingServiceExternally(string code, string serviceName, string exceptionMessage = null)
    {
        using(ITestAssemblyContext testAssemblyContext = AssembliesManager.GetAssemblyContextFromCode(code))
        {
            var assembly = testAssemblyContext.Assembly;
            var serviceTypeFromAssembly = assembly.GetTypeFromAssembly(serviceName);
            ServiceCollection.WithAutojector(ConfigureOptions, assembly);
            var serviceProvider = ServiceCollection.BuildServiceProvider();

            var exception = Should.Throw<Exception>(() => serviceProvider.GetRequiredService(serviceTypeFromAssembly));

            if (exceptionMessage != null)
            {
                exception.Message.ShouldBe(exceptionMessage);
            }
            ServiceCollection.Clear();
            serviceProvider.Dispose();
            serviceProvider = null;
        }
    }

    protected object ShouldSucceedOnGetService(Assembly assembly, string serviceName)
    {
        var serviceTypeFromAssembly = assembly.GetTypeFromAssembly(serviceName);
        return ShouldSucceedOnGetService(serviceTypeFromAssembly);
    }

    protected object ShouldSucceedOnGetService(Type serviceTypeFromAssembly)
    {
        var serviceProvider = ServiceCollection.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService(serviceTypeFromAssembly);
        service.ShouldNotBeNull();
        return service;
    }

    protected object ShouldSucceedOnGetService(string code,string abstractServiceName,string implementationServiceName = null)
    {
        using (ITestAssemblyContext testAssemblyContext = AssembliesManager.GetAssemblyContextFromCode(code))
        {
            ServiceCollection.WithAutojector(ConfigureOptions);
            var service = ShouldSucceedOnGetService(testAssemblyContext.Assembly, abstractServiceName);
            if(implementationServiceName != null)
            {
                service.GetType().Name.ShouldBe(implementationServiceName);
            }
            return service;
        }
    }
    protected void ShouldFailOnGetServiceLocallyWithExternalAssemblies<T>()
    {
        using (ITestAssemblyContext testAssemblyContext = AssembliesManager.GetAssemblyContextFromCode(""))
        {
            ServiceCollection.WithAutojector(ConfigureOptions, testAssemblyContext.Assembly);
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            Should.Throw<Exception>(() => serviceProvider.GetRequiredService<T>());
        }
    }
}
