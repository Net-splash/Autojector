using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorSimpleInjectionFeature : BaseAutojectorFeature
{


    public AutojectorSimpleInjectionFeature(IEnumerable<Assembly> assemblies = null) : base(assemblies)
    {
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.SimpleInjection;

    public override void ConfigureServices(IServiceCollection services)
    {
        var injectableClasses = GetInjectables();
        foreach (var injectableClass in injectableClasses)
        {
            injectableClass.ConfigureServices(services);
        }
    }

    public IEnumerable<SimpleInjectableClassType> GetInjectables()
    {
        return Assemblies
            .SelectMany(type => type.GetTypes())
            .Where(type => SimpleInjectableTypes.ILifetypeInjectableType.IsAssignableFrom(type) &&
                           type.IsClass &&
                           !type.IsAbstract
                  )
            .Select(type => new SimpleInjectableClassType(type));
    }
}
