using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;

internal interface ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type classType, Type interfaceType);
}

internal record SimpleRegisterStrategy(Func<Type, Type, IServiceCollection> Method) : ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type interfaceType, Type implementationType)
        => Method(interfaceType, implementationType);
}
