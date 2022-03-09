using Autojector.Base;
using Autojector.Registers.Base;
using Autojector.Registers.SimpleInjection;
using Autojector.Registers.SimpleInjection.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection.ImplementationVersion;

internal class SimpleInjectedInterfaceVersion : IFeatureImplementationVersion
{
    private record TypeWithLifetype(Type Type)
    {
        public bool HasImplementedLifetypeInterfaces => LifetypeType.Any();
        public IEnumerable<Type> LifetypeType
        {
            get
            {
                var implementedGenericInterfaces = Type.GetInterfacesFromTree(x => x.IsGenericType);
                var implementedLifetypeInterfaces = implementedGenericInterfaces
                    .Where(type => SimpleLifetypeInterfaces.Contains(type.GetGenericTypeDefinition()));
                return implementedLifetypeInterfaces;
            }
        }
    }
    protected IEnumerable<Type> NonAbstractClassesFromAssemblies { get; }
    public SimpleInjectedInterfaceVersion(
        ISimpleRegisterStrategyFactory registerStrategyFactory,
        IEnumerable<Type> nonAbstractClassesFromAssemblies)
    {
        RegisterStrategyFactory = registerStrategyFactory;
        NonAbstractClassesFromAssemblies = nonAbstractClassesFromAssemblies;
    }

    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }

    public IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        return NonAbstractClassesFromAssemblies
            .Select(type => new TypeWithLifetype(type))
            .Where(type => type.HasImplementedLifetypeInterfaces)
            .Select(pair => new SimpleInjectableTypeOperator(
                pair.Type,
                pair.LifetypeType,
                RegisterStrategyFactory
            ));
    }
}
