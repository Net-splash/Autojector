using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using Autojector.Registers;
using Autojector.Registers.SimpleInjection;
using Autojector.Registers.SimpleInjection.Operators;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection;
internal class AutojectorSimpleInjectionByInterfaceFeature : BaseAutojectorFeature
{
    private record TypeWithLifetype(Type Type)
    {
        public bool HasImplementedLifetypeInterfaces => LifetypeInterfaces.Any();
        public IEnumerable<Type> LifetypeInterfaces
        {
            get
            {
                var implementedLifetypeInterfaces = Type.GetConcrateImplementationThatMatchGenericsDefinition(SimpleLifetypeInterfaces);
                return implementedLifetypeInterfaces;
            }
        }
    }

    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.SimpleInjection;

    public AutojectorSimpleInjectionByInterfaceFeature(
        IEnumerable<Assembly> assemblies, 
        ISimpleRegisterStrategyFactory registerStrategyFactory
        ) : base(assemblies)
    {
        RegisterStrategyFactory = registerStrategyFactory ?? throw new ArgumentNullException(nameof(registerStrategyFactory));
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
        => NonAbstractClassesFromAssemblies
            .Select(type => new TypeWithLifetype(type))
            .Where(type => type.HasImplementedLifetypeInterfaces)
            .Select(pair => new SimpleInjectableTypeOperator(
                pair.Type,
                pair.LifetypeInterfaces,
                RegisterStrategyFactory
            ));
}
