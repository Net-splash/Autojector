using Autojector.Features.Base;
using Autojector.Registers.Base;
using System.Collections.Generic;
namespace Autojector.Base;
internal class AutojectorService : IAutojectorService, ITypeConfigurator
{
    public AutojectorService(IEnumerable<IAutojectorFeature> orderedFeatures)
    {
        OrderedFeatures = orderedFeatures;
    }

    private IEnumerable<IAutojectorFeature> OrderedFeatures { get; }

    public void ConfigureServices()
    {
        foreach (var feature in OrderedFeatures)
        {
            feature.ConfigureServices();
        }
    }
}
