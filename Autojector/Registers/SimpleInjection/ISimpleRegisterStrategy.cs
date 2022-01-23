using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;
internal interface ISimpleRegisterStrategy
{
    public IServiceCollection Add(Type classType, Type interfaceType);
}
