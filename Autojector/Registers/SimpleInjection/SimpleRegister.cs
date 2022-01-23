using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;

internal record SimpleRegister(Func<Type, Type, IServiceCollection> Method) : ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type classType, Type interfaceType)
        => Method(classType, interfaceType);
}
