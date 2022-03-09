using Autojector.Base;
using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Features.Factories;
internal class AutojectorFactoriesFeature : BaseAutojectorFeature
{
    private IFactoryRegisterStrategyFactory FactoryRegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Factories;
    public AutojectorFactoriesFeature(
        IEnumerable<Assembly> assemblies,
        IFactoryRegisterStrategyFactory factoryRegisterStrategyFactory
        ) : base(assemblies)
    {
        FactoryRegisterStrategyFactory = factoryRegisterStrategyFactory;
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var factories = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfacesFromTree(i => i.IsGenericType && FactoriesTypeInterfaces.Contains(i.GetGenericTypeDefinition())).Any());

        return factories.Select(type => new FactoryInjectableTypeOperator(type, FactoryRegisterStrategyFactory));
    }
}
