using System;
using System.Collections.Generic;
using System.Linq;
using Autojector.Base;

namespace Autojector.General;
internal class AutojectorService : IAutojectorService, ITypeConfigurator
{
    public AutojectorService(IEnumerable<IAutojectorFeature> orderedFeatures)
    {
        OrderedFeatures = orderedFeatures;
    }

    private IEnumerable<IAutojectorFeature> OrderedFeatures { get; }

    public void ConfigureServices()
    {
        ValidateAgainstOnlyDecoratorAdded();
        foreach (var feature in OrderedFeatures)
        {
            feature.ConfigureServices();
        }
    }
    private void ValidateAgainstOnlyDecoratorAdded()
    {
        var decoratorFeatures = OrderedFeatures.Where(f => f.FeatureType == AutojectorFeaturesEnum.Decorators);
        if (!OrderedFeatures.Except(decoratorFeatures).Any())
        {
            throw new InvalidOperationException(@$"You can't have only decorators feature because there will be nothing to decorate. 
                            Please make sure to call other methods from builder beside UseDecorator");
        }
    }
}
