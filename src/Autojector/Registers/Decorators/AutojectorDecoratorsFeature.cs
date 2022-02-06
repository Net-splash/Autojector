using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Features.Decorators;
internal class AutojectorDecoratorsFeature : BaseAutojectorFeature
{
    private record DecoratorWithDecoratedType(Type Decorator, Type Decorated);
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Decorators;
    public AutojectorDecoratorsFeature(IEnumerable<Assembly> assemblies, IServiceCollection service) : base(assemblies, service)
    {
        DecoratorRegisterStrategy = new DecoratorRegisterStrategy(Services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decorators = NonAbstractClassesFromAssemblies.Where(type => GetDecoratorInterfacesFromType(type).Any());

        var resultDecoratorsGrouped = decorators
            .SelectMany(d => ConvertToPairType(d))
            .GroupBy(d => d.Decorated)
            .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));

        return resultDecoratorsGrouped;
    }

    private static IEnumerable<DecoratorWithDecoratedType> ConvertToPairType(Type type)
    {
        var implementedDecorators = GetDecoratorInterfacesFromType(type);
        return implementedDecorators
            .Select(decorator => new DecoratorWithDecoratedType(type, decorator
                .GetGenericArguments()
                .First())
            );
    }

    private static IEnumerable<Type> GetDecoratorInterfacesFromType(Type type)
    {
        return type.GetInterfaces().Where(d => d.IsGenericType && d.GetGenericTypeDefinition() == DecoratorType);
    }
}
