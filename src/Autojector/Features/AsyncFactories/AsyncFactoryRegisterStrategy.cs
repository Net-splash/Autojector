using Autojector.Abstractions;
using Autojector.DependencyInjector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Autojector.Features.AsyncFactories
{

    internal class AsyncFactoryRegisterStrategy : IAsyncFactoryRegisterStrategy
    {
        public AsyncFactoryRegisterStrategy(
        Func<Type, IServiceCollection> registerFactoryMethod,
        Func<Type, Func<IServiceProvider, object>, IServiceCollection> registerFactoryCallMethod)
        {
            RegisterFactoryMethod = registerFactoryMethod;
            RegisterFactoryCallMethod = registerFactoryCallMethod;
        }
        private static string MethodName => nameof(IAsyncFactory<object>.GetServiceAsync);

        public Func<Type, IServiceCollection> RegisterFactoryMethod { get; }
        public Func<Type, Func<IServiceProvider, object>, IServiceCollection> RegisterFactoryCallMethod { get; }

        public IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType)
        {
            var serviceType = factoryInterfaceType.GetGenericArguments().First();
            var abstractAsyncDependencyType = typeof(IAsyncDependency<>).MakeGenericType(new Type[] { serviceType });
            var asyncDependencyType = typeof(AsyncDependency<>).MakeGenericType(new Type[] { serviceType });

            var taskWithDependencyTypeType = typeof(Task<>).MakeGenericType(new Type[] { serviceType });
            var constructorType = typeof(Func<>).MakeGenericType(new Type[] { taskWithDependencyTypeType });
            var asyncDependencyTypeConstructor = asyncDependencyType.GetConstructor(new Type[] { constructorType });
            var method = factoryImplementationType.GetMethod(MethodName);

            RegisterFactoryMethod(factoryImplementationType);
            return RegisterFactoryCallMethod(abstractAsyncDependencyType, (serviceProvider) =>
            {
                var serviceFactory = serviceProvider.GetService(factoryImplementationType);
                var methodCall = Expression.Call(Expression.Constant(serviceFactory), method);

                var lambda = Expression.Lambda(methodCall)
                                       .Compile();

                return asyncDependencyTypeConstructor.Invoke(new[] { lambda });
            });
        }
    }

}
