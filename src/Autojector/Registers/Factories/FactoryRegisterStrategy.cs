using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Autojector.Registers.Factories;
internal interface IFactoryRegisterStrategy
{
    IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType);
}

internal record FactoryRegisterStrategy(
    Func<Type, IServiceCollection> registerFactoryMethod,
    Func<Type, Func<IServiceProvider, object>, IServiceCollection> registerFactoryCallMethod
    ) : IFactoryRegisterStrategy
{
    private static string MethodName => nameof(IFactory<object>.GetService);

    public IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType)
    {
        var factoryArgument = factoryInterfaceType.GetGenericArguments().First();
        var method = factoryImplementationType.GetMethod(MethodName);
        registerFactoryMethod(factoryImplementationType);
        return registerFactoryCallMethod(factoryArgument, (serviceProvider) =>
        {
            var serviceFactory = serviceProvider.GetService(factoryImplementationType);
            return method.Invoke(serviceFactory, null);
        });
    }
}
