using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.DependencyInjector.Public;
internal interface ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type interfaceType, Type implementationType);
}
