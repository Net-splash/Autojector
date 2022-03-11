using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.DependencyInjector.Public;

internal interface IAsyncFactoryRegisterStrategy
{
    IServiceCollection Add(Type type, Type factoryInterface);
}
