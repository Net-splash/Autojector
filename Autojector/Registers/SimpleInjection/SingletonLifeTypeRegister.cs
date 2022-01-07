using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Registers.SimpleInjection
{
    internal class SingletonLifeTypeRegister : ISimpleLifetypeRegisterStrategy
    {
        public SingletonLifeTypeRegister(IServiceCollection services)
        {
            Services = services;
        }

        private IServiceCollection Services { get; }

        public IServiceCollection Add(Type classType)
        {
            Services.AddSingleton(classType);
            return Services;
        }

        public IServiceCollection Add(Type classType, Type interfaceType)
        {
            Services.AddSingleton(interfaceType, classType);
            return Services;
        }
    }
    

}