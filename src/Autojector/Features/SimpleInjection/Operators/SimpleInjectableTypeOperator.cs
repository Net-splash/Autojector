﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection.Operators
{
    internal class SimpleInjectableTypeOperator : BaseSimpleInjectableOperator, ITypeConfigurator
    {
        public IEnumerable<Type> ImplementedGenericLifetypeInterfaces { get; }
        public ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory { get; }

        public SimpleInjectableTypeOperator(
            Type type,
            IEnumerable<Type> implementedGenericLifetypeInterfaces,
            ISimpleRegisterStrategyFactory simpleRegisterStrategyFactory) : base(type)
        {
            ImplementedGenericLifetypeInterfaces = implementedGenericLifetypeInterfaces;
            SimpleRegisterStrategyFactory = simpleRegisterStrategyFactory;
        }
        public void ConfigureServices()
        {
            ValidateUnknownLifetype(ImplementedGenericLifetypeInterfaces);
            ValidateNotImplementedInterface();
            foreach (var injectableType in ImplementedGenericLifetypeInterfaces)
            {
                ValidateOnlyOneGenericArgument(injectableType);

                var lifetimeType = injectableType.GetGenericTypeDefinition();
                var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(lifetimeType);

                var injectableInterface = injectableType.GetGenericArguments().First();
                registerStrategy.Add(injectableInterface, Type);
            }
        }

        private void ValidateNotImplementedInterface()
        {
            var customInterfaceFromExtension = Type.GetInterfaces()
                .Where(i => !SimpleLifetypeInterfaces.Contains(i))
                .Concat(new Type[] { Type });

            var customInterfaceFromLifeType = ImplementedGenericLifetypeInterfaces.Select(i => i.GetGenericArguments().First());
            var nonImplementedInterfaceFromLifetype = customInterfaceFromLifeType.Except(customInterfaceFromExtension);
            ValidateNotImplementedInterface(nonImplementedInterfaceFromLifetype);
        }


        private static void ValidateOnlyOneGenericArgument(Type injectableType)
        {
            if (injectableType.GetGenericArguments().HasMany())
            {
                throw new InvalidOperationException("Can not have more than one argument in a simple life type interface implementation");
            }
        }
    }

}
