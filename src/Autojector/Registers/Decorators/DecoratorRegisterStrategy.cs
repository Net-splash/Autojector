using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using static Autojector.Base.Types;
namespace Autojector.Registers.Decorators;
internal interface IDecoratorRegisterStrategy
{
    IServiceCollection Add(Type decorator, Type decorated);
}

internal class DecoratorRegisterStrategy : IDecoratorRegisterStrategy
{
    private IServiceCollection Services { get; }

    public DecoratorRegisterStrategy(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Add(Type decorator, Type decorated)
    {
        var serviceType = decorator.GetInterfaces()
                                   .SingleOrDefault(s => s.IsGenericType && s.GetGenericTypeDefinition() == DecoratorType)
                                   .GetGenericArguments()
                                   .FirstOrDefault();

        var wrappedDescriptor = Services.FirstOrDefault(s => s.ServiceType == serviceType);

        if (wrappedDescriptor == null)
        {
            throw new InvalidOperationException($"{serviceType.Name} is not registered");
        }

        var objectFactory = ActivatorUtilities.CreateFactory(
          decorator,
          new[] { serviceType });

        Services.Replace(ServiceDescriptor.Describe(
          serviceType,
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