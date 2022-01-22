using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.Factories;
internal interface IFactoryLifetypeRegisterStrategy
{
    IServiceCollection Add(Type factoryImplementationType, Type factoryInterfaceType);
}
