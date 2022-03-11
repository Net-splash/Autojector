using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Autojector.Registers.AsyncFactories;
internal interface IAsyncFactoryRegisterStrategy
{
    IServiceCollection Add(Type type, Type factoryInterface);
}

internal record AsyncFactoryRegisterStrategy(
Func<Type, IServiceCollection> registerFactoryMethod,
Func<Type, Func<IServiceProvider, object>, IServiceCollection> registerFactoryCallMethod
) : IAsyncFactoryRegisterStrategy
{
    private static string MethodName => nameof(IAsyncFactory<object>.GetServiceAsync);

    public IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType)
    {
        var serviceType = factoryInterfaceType.GetGenericArguments().First();
        var abstractAsyncDependencyType = typeof(IAsyncDependency<>).MakeGenericType(new Type[] { serviceType });
        var asyncDependencyType = typeof(AsyncDependency<>).MakeGenericType(new Type[] { serviceType });

        var taskWithDependencyTypeType = typeof(Task<>).MakeGenericType(new Type[] { serviceType });
        var constructorType = typeof(Func<>).MakeGenericType(new Type[] { taskWithDependencyTypeType });
        var asyncDependencyTypeConstructor = asyncDependencyType.GetConstructor(new Type[] { constructorType });
        var method = factoryImplementationType.GetMethod(MethodName);

        registerFactoryMethod(factoryImplementationType);
        return registerFactoryCallMethod(abstractAsyncDependencyType, (serviceProvider) =>
        {
            var serviceFactory = serviceProvider.GetService(factoryImplementationType);
            var methodCall = Expression.Call(Expression.Constant(serviceFactory), method);

            var lambda = Expression.Lambda(methodCall)
                                   .Compile() ;

            return asyncDependencyTypeConstructor.Invoke(new[] { lambda });
        });
    }
}
