using Autojector.Registers;
using Autojector.Registers.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorDecoratorsFeature : BaseAutojectorFeature
{
    public AutojectorDecoratorsFeature(IEnumerable<Assembly> assemblies) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Decorators;

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var decorators = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == DecoratorTypesOperator.DecoratorType));

        var resultDecoratorsGrouped = decorators.GroupBy(d => d.GetInterfaces()
                            .Where(d => d.IsGenericType)
                            .Select(i =>  i.GetGenericTypeDefinition())
                            .SingleOrDefault(i => i == DecoratorTypesOperator.DecoratorType)
                            .GetGenericArguments()
                            .FirstOrDefault()
                            ).Select(g => new DecoratorTypesOperator(g.Key, g));

        return resultDecoratorsGrouped;
    }
}
