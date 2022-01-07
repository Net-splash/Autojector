using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection
{
    internal class ScopetLifeTypeRegister : ISimpleLifetypeRegisterStrategy
    {
        public ScopetLifeTypeRegister(IServiceCollection services)
        {
            Services = services;
        }

        private IServiceCollection Services { get; }

        public IServiceCollection Add(Type classType)
        {
            Services.AddScoped(classType);
            return Services;
        }

        public IServiceCollection Add(Type classType, Type interfaceType)
        {
            Services.AddScoped(interfaceType, classType);
            return Services;
        }
    }
    

}