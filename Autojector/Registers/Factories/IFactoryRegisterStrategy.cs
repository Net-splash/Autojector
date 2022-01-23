using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.Factories;
internal interface IFactoryRegisterStrategy
{
    IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType);
}
