using Autojector.Registers;
using Autojector.Registers.Base;
using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Abstractions.Types;

namespace Autojector.Features.Factories;
internal class AutojectorFactoriesFeature : BaseAutojectorFeature
{
    private IFactoryRegisterStrategyFactory FactoryRegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Factories;
    public AutojectorFactoriesFeature(IEnumerable<Assembly> assemblies,IServiceCollection services) : base(assemblies, services)
    {
        FactoryRegisterStrategyFactory = new FactoryRegisterStrategyFactory(Services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var factories = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType && 
                                    i.GetGenericTypeDefinition() == FactoryType));

        return factories.Select(type => new FactoryInjectableTypeOperator(type, FactoryRegisterStrategyFactory));
    }
}
