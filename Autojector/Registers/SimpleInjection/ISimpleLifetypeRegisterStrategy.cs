using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;
internal interface ISimpleLifetypeRegisterStrategy
{
    public IServiceCollection Add(Type classType, Type interfaceType);
}
