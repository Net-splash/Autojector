using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection
{
    internal class TransientLifeTypeRegister : ISimpleLifetypeRegisterStrategy
    {
        public TransientLifeTypeRegister(IServiceCollection services)
        {
            Services = services;
        }

        private IServiceCollection Services { get; }

        public IServiceCollection Add(Type classType)
        {
            Services.AddTransient(classType);
            return Services;
        }

        public IServiceCollection Add(Type classType, Type interfaceType)
        {
            Services.AddTransient(interfaceType, classType);
            return Services;
        }
    }
}