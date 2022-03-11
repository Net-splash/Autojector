using Autojector.Base;
using Autojector.Extensions;
using Autojector.Registers;
using Autojector.Registers.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;

namespace Autojector.Features.Factories;
internal class AutojectorFactoriesFeature : BaseAutojectorFeature
{
    private IFactoryRegisterStrategyFactory FactoryRegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Factories;
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
            .Where(type => type.HasAnyConcrateImplementationThatMatchGenericsDefinition(FactoriesTypeInterfaces));

        return factories.Select(type => new FactoryInjectableTypeOperator(type, FactoryRegisterStrategyFactory));
    }
}
