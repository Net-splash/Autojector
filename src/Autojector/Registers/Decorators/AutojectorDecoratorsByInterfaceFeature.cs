using Autojector.Base;
using Autojector.Extensions;
using Autojector.Registers;
using Autojector.Registers.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Features.Decorators;

internal class AutojectorDecoratorsByInterfaceFeature : BaseAutojectorFeature
{
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Decorators;
    public AutojectorDecoratorsByInterfaceFeature(
        IEnumerable<Assembly> assemblies,
        IDecoratorRegisterStrategy decoratorRegisterStrategy
        ) : base(assemblies)
    {
        DecoratorRegisterStrategy = decoratorRegisterStrategy;
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
        var decoratedTypesFromInterface = type.GetConcrateImplementationThatMatchGenericsDefinition(new Type[] { DecoratorType });
        return decoratedTypesFromInterface;
    }
    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decoratorsFromInterfaces = NonAbstractClassesFromAssemblies.Where(type => GetDecoratorInterfacesFromType(type).Any())
                                        .SelectMany(d => ConvertToPairType(d));

        var groupedDecorators = decoratorsFromInterfaces.GroupBy(d => d.Decorated)
                                .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));
        return groupedDecorators;
    }
}
