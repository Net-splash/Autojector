using Autojector.Abstractions;
using Autojector.DependencyInjector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Autojector.Features.Factories
{
    internal class FactoryRegisterStrategy : IFactoryRegisterStrategy
{
        public FactoryRegisterStrategy(
        Func<Type, IServiceCollection> registerFactoryMethod,
        Func<Type, Func<IServiceProvider, object>, IServiceCollection> registerFactoryCallMethod
        )
        {
            RegisterFactoryMethod = registerFactoryMethod;
            RegisterFactoryCallMethod = registerFactoryCallMethod;
        }

        private static string MethodName => nameof(IFactory<object>.GetService);

        public Func<Type, IServiceCollection> RegisterFactoryMethod { get; }
        public Func<Type, Func<IServiceProvider, object>, IServiceCollection> RegisterFactoryCallMethod { get; }

        public IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType)
    {
        var factoryArgument = factoryInterfaceType.GetGenericArguments().First();
        var method = factoryImplementationType.GetMethod(MethodName);
        RegisterFactoryMethod(factoryImplementationType);
        return RegisterFactoryCallMethod(factoryArgument, (serviceProvider) =>
        {
            var serviceFactory = serviceProvider.GetService(factoryImplementationType);
            return method.Invoke(serviceFactory, null);
        });
    }
}

}