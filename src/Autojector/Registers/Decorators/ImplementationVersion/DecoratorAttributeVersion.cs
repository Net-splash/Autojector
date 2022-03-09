using Autojector.Abstractions;
using Autojector.Base;
using Autojector.Registers.Base;
using Autojector.Registers.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Features.Decorators.ImplementationVersion;

internal class DecoratorAttributeVersion : IFeatureImplementationVersion
{
    public DecoratorAttributeVersion(IEnumerable<Type> nonAbstractClassesFromAssemblies, IDecoratorRegisterStrategy decoratorRegisterStrategy)
    {
        NonAbstractClassesFromAssemblies = nonAbstractClassesFromAssemblies;
        DecoratorRegisterStrategy = decoratorRegisterStrategy;
    }

    private IEnumerable<Type> NonAbstractClassesFromAssemblies { get; }
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }

    public IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decoratorsFromAttributes = NonAbstractClassesFromAssemblies.Select(type => new TypeWithAttributes<DecoratorAttribute>(type))
                                      .Where(typeWithAttribute => typeWithAttribute.HasImplementedLifetypeAttributes)
                                      .Select(d => new DecoratorWithDecoratedType(d.Type, d.Attributes.First().DecoratedType));


        return decoratorsFromAttributes.GroupBy(d => d.Decorated)
                                .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));
    }
}