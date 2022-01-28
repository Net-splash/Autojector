using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Abstractions.Types;

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
        var decorators = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == DecoratorType));

        var pairs = decorators.SelectMany(d => ExtractPairs(d));
        var resultDecoratorsGrouped = pairs.GroupBy(d => d.Decorated)
            .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));

        return resultDecoratorsGrouped;
    }

    private static IEnumerable<DecoratorWithDecoratedType> ExtractPairs(Type type)
    {
        var implementedDecorators = type.GetInterfaces()
                                    .Where(d => d.IsGenericType &&
                                        d.GetGenericTypeDefinition() == DecoratorType);

        return implementedDecorators.Select(d => new DecoratorWithDecoratedType(type,
                                    d.GetGenericArguments()
                                    .First()));
    }
}
