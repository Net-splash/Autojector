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
    private record TypeWithSimpleInjectableInterface(Type Type,IEnumerable<Type> implementedLifetypeInterfaces);
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
            .Select(GetTypeWithSimpleInjectableInterfaces)
            .Where(t => t.implementedLifetypeInterfaces.Any());
        
        return injectables.Select(pair => new SimpleInjectableTypeOperator(pair.Type,pair.implementedLifetypeInterfaces, RegisterStrategyFactory));
    }

    private TypeWithSimpleInjectableInterface GetTypeWithSimpleInjectableInterfaces(Type type)
    {
        var implementedLifetypeInterfaces = type.GetInterfacesFromTree(x => x.IsGenericType)
            .Where(type => SimpleLifetypeInterfaces.Contains(type.GetGenericTypeDefinition())); ;
        return new TypeWithSimpleInjectableInterface(type, implementedLifetypeInterfaces) ;
    }
}
