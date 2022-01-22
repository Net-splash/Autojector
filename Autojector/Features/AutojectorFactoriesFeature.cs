using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
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

    public override void ConfigureServices(IServiceCollection services)
    {
        var factories = GetFactories();
        foreach (var factory in factories)
        {
            factory.ConfigureServices(services);
        }
    }

    public IEnumerable<FactoryInjectableClassType> GetFactories()
    {
        var allTypes = Assemblies
            .SelectMany(type => type.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        var factories = allTypes
            .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == FactoriesInjectableTypes.FactoryType));

        return factories.Select(type => new FactoryInjectableClassType(type));
    }
}
