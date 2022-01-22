using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection;
internal class ScopeLifeTypeRegister : ISimpleLifetypeRegisterStrategy
{
    public ScopeLifeTypeRegister(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }

    public IServiceCollection Add(Type classType, Type interfaceType)
        => Services.AddScoped(interfaceType, classType);
}
