using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Abstractions.Types;

namespace Autojector.Features.SimpleInjection;
internal class AutojectorSimpleInjectionFeature : BaseAutojectorFeature
{
    private ISimpleRegisterStrategyFactory RegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.SimpleInjection;

    public AutojectorSimpleInjectionFeature(IEnumerable<Assembly> assemblies,IServiceCollection services) : 
        base(assemblies, services)
    {
        RegisterStrategyFactory = new SimpleRegisterStrategyFactory(services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var injectables = NonAbstractClassesFromAssemblies.Where(HasSimpleInjectalbeGenericInterface);
        return injectables.Select(type => new SimpleInjectableTypeOperator(type, RegisterStrategyFactory));
    }

    private bool HasSimpleInjectalbeGenericInterface(Type type)
    {
        var injectablePredefinedInterfaces = SimpleLifetypeInterfaces;
        var typeGenericInterfaces = type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Select(i => i.GetGenericTypeDefinition());
        return injectablePredefinedInterfaces.Intersect(typeGenericInterfaces).Any();
    }
}
