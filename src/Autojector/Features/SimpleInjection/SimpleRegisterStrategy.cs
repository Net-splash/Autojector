﻿using Autojector.DependencyInjector.Public;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Autojector.Features.SimpleInjection
{
    internal class SimpleRegisterStrategy : ISimpleRegisterStrategy
    {
        public SimpleRegisterStrategy(Func<Type, Type, IServiceCollection> method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        private Func<Type, Type, IServiceCollection> Method { get; }

        public IServiceCollection Add(Type interfaceType, Type implementationType)
            => Method(interfaceType, implementationType);
    }
}

