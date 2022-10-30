using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.DependencyInjector.Public
{
    internal interface IFactoryRegisterStrategy
    {
        IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType);
    }
}