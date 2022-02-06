using Autojector.Registers;
using Autojector.Registers.AsyncFactories;
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Base.Types;
namespace Autojector.Features.AsyncFactories;
internal class AutojectorAsyncFactoriesFeature : BaseAutojectorFeature
{
    private IAsyncFactoryRegisterStrategyFactory AsyncFactoryRegisterStrategyFactory { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.AsyncFactories;
    public AutojectorAsyncFactoriesFeature(IEnumerable<Assembly> assemblies,IServiceCollection service) : base(assemblies, service)
    {
        AsyncFactoryRegisterStrategyFactory = new AsyncFactoryRegisterStrategyFactory(service);
    }


    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var factories = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == AsyncFactoryType));

        return factories.Select(type => new AsyncFactoryInjectableTypeOperator(type, AsyncFactoryRegisterStrategyFactory));
    }
}
