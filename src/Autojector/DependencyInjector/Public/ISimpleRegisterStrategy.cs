using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.DependencyInjector.Public
{
    internal interface ISimpleRegisterStrategy
    {
        IServiceCollection Add(Type interfaceType, Type implementationType);
    }
}
