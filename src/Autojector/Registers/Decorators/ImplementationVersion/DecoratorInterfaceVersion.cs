using Autojector.Base;
using Autojector.Registers.Base;
using Autojector.Registers.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Features.Decorators.ImplementationVersion;

internal class DecoratorInterfaceVersion : IFeatureImplementationVersion
{
    public DecoratorInterfaceVersion(IEnumerable<Type> nonAbstractClassesFromAssemblies, IDecoratorRegisterStrategy decoratorRegisterStrategy)
    {
        NonAbstractClassesFromAssemblies = nonAbstractClassesFromAssemblies;
        DecoratorRegisterStrategy = decoratorRegisterStrategy;
    }

    private IEnumerable<Type> NonAbstractClassesFromAssemblies { get; }
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }

    public IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decoratorsFromInterfaces = NonAbstractClassesFromAssemblies.Where(type => GetDecoratorInterfacesFromTypeOrAttribute(type).Any())
                                        .SelectMany(d => ConvertToPairType(d));

        var groupedDecorators = decoratorsFromInterfaces.GroupBy(d => d.Decorated)
                                .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));
        return groupedDecorators;
    }

    private static IEnumerable<DecoratorWithDecoratedType> ConvertToPairType(Type type)
    {
        var implementedDecorators = GetDecoratorInterfacesFromTypeOrAttribute(type);
        return implementedDecorators
            .Select(decorator => new DecoratorWithDecoratedType(type, decorator
                .GetGenericArguments()
                .First())
            );
    }

    private static IEnumerable<Type> GetDecoratorInterfacesFromTypeOrAttribute(Type type)
    {
        var decoratedTypesFromInterface = type.GetInterfacesFromTree(d => d.IsGenericType && d.GetGenericTypeDefinition() == DecoratorType);
        return decoratedTypesFromInterface;
    }
}
