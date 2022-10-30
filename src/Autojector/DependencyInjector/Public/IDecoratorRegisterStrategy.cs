using Microsoft.Extensions.DependencyInjection;
using System;
namespace Autojector.DependencyInjector.Public
{
    internal interface IDecoratorRegisterStrategy
    {
        IServiceCollection Add(Type decorator, Type decorated);
    }
}
