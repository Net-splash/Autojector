using Autojector.DependencyInjector.Public;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
namespace Autojector.Registers.Decorators;

internal class DecoratorRegisterStrategy : IDecoratorRegisterStrategy
{
    private IServiceCollection Services { get; }

    public DecoratorRegisterStrategy(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Add(Type decorator, Type decorated)
    {
        var wrappedDescriptor = Services.FirstOrDefault(s => s.ServiceType == decorated);

        if (wrappedDescriptor == null)
        {
            throw new InvalidOperationException($"{decorated.Name} is not registered. Can not be decorated");
        }

        var objectFactory = ActivatorUtilities.CreateFactory(
          decorator,
          new[] { decorated });

        Services.Replace(ServiceDescriptor.Describe(
          decorated,
          s => objectFactory(s, new[] { CreateInstance(s, wrappedDescriptor) }),
          wrappedDescriptor.Lifetime)
        );

        return Services;
    }

    private static object CreateInstance(IServiceProvider services, ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance != null)
            return descriptor.ImplementationInstance;

        if (descriptor.ImplementationFactory != null)
            return descriptor.ImplementationFactory(services);

        return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
    }
}