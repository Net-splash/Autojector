using Autojector.Registers;
using Autojector.Registers.SimpleInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorSimpleInjectionFeature : BaseAutojectorFeature
{
    public AutojectorSimpleInjectionFeature(IEnumerable<Assembly> assemblies) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.SimpleInjection;

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators(IEnumerable<Type> types)
    {
        var nonAbstractClasses = GetNonAbstractClasses(types);
        var injectables = nonAbstractClasses.Where(HasSimpleInjectalbeGenericInterface);
        return injectables.Select(type => new SimpleInjectableTypeOperator(type));
    }

    private bool HasSimpleInjectalbeGenericInterface(Type type)
    {
        var injectablePredefinedInterfaces = SimpleInjectableTypeOperator.SimpleLifetimeInterfaces;
        var typeGenericInterfaces = type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Select(i => i.GetGenericTypeDefinition());
        return injectablePredefinedInterfaces.Intersect(typeGenericInterfaces).Any();
    }
}
