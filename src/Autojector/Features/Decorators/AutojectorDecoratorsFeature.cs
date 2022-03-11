using Autojector.Abstractions;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Features;
using Autojector.Features.Decorators;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features.Decorators;
internal class AutojectorDecoratorsByAttributeFeature : BaseAutojectorFeature
{
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Decorators;
    public AutojectorDecoratorsByAttributeFeature(
        IEnumerable<Assembly> assemblies,
        IDecoratorRegisterStrategy decoratorRegisterStrategy
        ) : base(assemblies)
    {
        DecoratorRegisterStrategy = decoratorRegisterStrategy;
    }
    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decoratorsFromAttributes = NonAbstractClassesFromAssemblies.Select(type => new TypeWithAttributes<DecoratorAttribute>(type))
                                      .Where(typeWithAttribute => typeWithAttribute.HasAttributes)
                                      .Select(d => new DecoratorWithDecoratedType(d.Type, d.Attributes.First().DecoratedType));


        return decoratorsFromAttributes.GroupBy(d => d.Decorated)
                                .Select(g => new DecoratorTypesOperator(g.Key, g.Select(x => x.Decorator), DecoratorRegisterStrategy));
    }
}
