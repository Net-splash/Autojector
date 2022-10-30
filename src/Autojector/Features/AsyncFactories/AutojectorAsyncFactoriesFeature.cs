using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;
namespace Autojector.Features.AsyncFactories
{
    internal class AutojectorAsyncFactoriesFeature : BaseAutojectorFeature
    {
        private class FactoryWithImplementedType
        {
            public FactoryWithImplementedType(Type Factory)
            {
                this.Factory = Factory;
            }

            public IEnumerable<Type> ImplementedFactories
            {
                get
                {
                    return Factory.GetConcrateImplementationThatMatchGenericsDefinition(AsyncFactoriesTypeInterfaces);
                }
            }

            public Type Factory { get; }
        }
        private IAsyncFactoryRegisterStrategyFactory AsyncFactoryRegisterStrategyFactory { get; }
        public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.AsyncFactories;
        public AutojectorAsyncFactoriesFeature(
            IEnumerable<Assembly> assemblies,
            IAsyncFactoryRegisterStrategyFactory asyncFactoryRegisterStrategyFactory
            ) : base(assemblies)
        {
            AsyncFactoryRegisterStrategyFactory = asyncFactoryRegisterStrategyFactory;
        }


        protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
        {
            var factories = NonAbstractClassesFromAssemblies
                .Select(type => new FactoryWithImplementedType(type))
                .Where(factoryWithImplementedType => factoryWithImplementedType.ImplementedFactories.Any());

            return factories.Select(factoryWithImplementedType => new AsyncFactoryInjectableTypeOperator(
                factoryWithImplementedType.Factory,
                factoryWithImplementedType.ImplementedFactories,
                AsyncFactoryRegisterStrategyFactory));
        }
    }
}
