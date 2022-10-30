using Autojector.Abstractions;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection.Operators
{
    internal class SimpleAttributeTypeOperator : BaseSimpleInjectableOperator, ITypeConfigurator
    {
        public SimpleAttributeTypeOperator(
            Type type,
            IEnumerable<BaseInjectionAttribute> attributes,
            ISimpleRegisterStrategyFactory simpleRegisterStrategyFactory) : base(type)
        {
            this.Attributes = attributes;
            this.SimpleRegisterStrategyFactory = simpleRegisterStrategyFactory;
        }

        public IEnumerable<BaseInjectionAttribute> Attributes { get; }
        public ISimpleRegisterStrategyFactory SimpleRegisterStrategyFactory { get; }

        public void ConfigureServices()
        {
            ValidateTypeAndAttributes();
            foreach (var attribute in Attributes)
            {
                var registerStrategy = SimpleRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(attribute);
                registerStrategy.Add(attribute.AbstractionType, Type);
            }
        }

        private void ValidateTypeAndAttributes()
        {
            var interfacesFromAttributes = Attributes.Select(a => a.AbstractionType);
            ValidateUnknownLifetype(interfacesFromAttributes);

            var customInterfaceFromExtension = Type.GetInterfaces()
              .Where(i => !SimpleLifetypeInterfaces.Contains(i));

            var nonImplementedInterfaceFromLifetype = customInterfaceFromExtension.Except(interfacesFromAttributes);
            ValidateNotImplementedInterface(nonImplementedInterfaceFromLifetype);
        }
    }

}
