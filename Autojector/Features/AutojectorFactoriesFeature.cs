using Autojector.Registers;
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorFactoriesFeature : BaseAutojectorFeature
{
    public AutojectorFactoriesFeature(IEnumerable<Assembly> assemblies) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Factories;

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators(IEnumerable<Type> types)
    {
        var allTypes = types.Where(type => type.IsClass && !type.IsAbstract);
        var factories = allTypes
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType && 
                                    i.GetGenericTypeDefinition() == FactoriesInjectableTypes.FactoryType));

        return factories.Select(type => new FactoryInjectableTypeOperator(type));
    }
}
