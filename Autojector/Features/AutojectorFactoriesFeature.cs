using Autojector.Registers.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorFactoriesFeature : BaseAutojectorFeature
{
    public AutojectorFactoriesFeature(IEnumerable<Assembly> assemblies = null) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Factories;

    public override void ConfigureServices(IServiceCollection services)
    {
        var factories = GetFactories();
        foreach (var factory in factories)
        {

        }
    }

    public IEnumerable<FactoryInjectableClassType> GetFactories()
    {
        return Assemblies
            .SelectMany(type => type.GetTypes())
            .Where(type => FactoriesInjectableTypes.FactoryType.IsAssignableFrom(type) &&
                           type.IsClass &&
                           !type.IsAbstract
                  )
            .Select(type => new FactoryInjectableClassType(type));
    }
}
