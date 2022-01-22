using Autojector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Autojector.Registers.Factories;

internal class SingletonFactoryRegister : IFactoryLifetypeRegisterStrategy
{
    public SingletonFactoryRegister(IServiceCollection services)
    {
        Services = services;
    }
    private string MethodName => nameof(IFactory<object>.GetService);

    private IServiceCollection Services { get; }

    public IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType)
    {
        var factoryArgument = factoryInterfaceType.GetGenericArguments().First();
        var method = factoryImplementationType.GetMethod(MethodName);
        Services.AddSingleton(factoryImplementationType);
        Services.AddSingleton(factoryArgument, (serviceProvider) =>
        {
            var serviceFactory = serviceProvider.GetService(factoryImplementationType);
            return method.Invoke(serviceFactory, null);

        });

        return Services;
    }
}