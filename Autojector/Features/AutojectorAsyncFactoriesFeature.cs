using Autojector.Registers;
using Autojector.Registers.AsyncFactories;
using Autojector.Registers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;

internal class AutojectorAsyncFactoriesFeature : BaseAutojectorFeature
{
    public AutojectorAsyncFactoriesFeature(IEnumerable<Assembly> assemblies) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.AsyncFactories;

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators(IEnumerable<Type> types)
    {
        var nonAbstractClasses = GetNonAbstractClasses(types);
        var factories = nonAbstractClasses
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == AsyncFactoryInjectableTypeOperator.AsyncFactoryType));

        return factories.Select(type => new AsyncFactoryInjectableTypeOperator(type));
    }
}
