using Autojector.Base;
using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection;
internal class AutojectorSimpleInjectionFeature : BaseAutojectorFeature
{
    private record TypeWithSimpleInjectableInterface(Type Type,IEnumerable<Type> SimpleInjectableInterface);
    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.SimpleInjection;

    public AutojectorSimpleInjectionFeature(IEnumerable<Assembly> assemblies,IServiceCollection services) : 
        base(assemblies, services)
    {
        RegisterStrategyFactory = new SimpleRegisterStrategyFactory(services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var injectables = NonAbstractClassesFromAssemblies
            .Select(t => new TypeWithSimpleInjectableInterface(t, GetInjectableInterfaces(t)))
            .Where(t => t.SimpleInjectableInterface.Any());
        
        return injectables.Select(pair => new SimpleInjectableTypeOperator(pair.Type,pair.SimpleInjectableInterface, RegisterStrategyFactory));
    }

    private IEnumerable<Type> GetInjectableInterfaces(Type type)
    {
        var typeGenericInterfaces = type.GetInterfacesFromTree(x => x.IsGenericType)
            .Select(type => type.GetGenericTypeDefinition());

        return SimpleLifetypeInterfaces.Intersect(typeGenericInterfaces);
    }
}
