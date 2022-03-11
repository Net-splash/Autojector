using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;

internal interface ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type interfaceType, Type implementationType);
}

internal class SimpleRegisterStrategy : ISimpleRegisterStrategy
{
    public SimpleRegisterStrategy(Func<Type, Type, IServiceCollection> method)
    {
        Method = method ?? throw new ArgumentNullException(nameof(method));
    }

    private Func<Type, Type, IServiceCollection> Method { get; }

    public IServiceCollection Add(Type interfaceType, Type implementationType)
        => Method(interfaceType, implementationType);
}
