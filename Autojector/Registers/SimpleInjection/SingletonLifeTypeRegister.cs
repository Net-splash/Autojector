using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;
internal class SingletonLifeTypeRegister : ISimpleLifetypeRegisterStrategy
{
    public SingletonLifeTypeRegister(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }

    public IServiceCollection Add(Type classType, Type interfaceType)
        => Services.AddSingleton(interfaceType, classType);
}
