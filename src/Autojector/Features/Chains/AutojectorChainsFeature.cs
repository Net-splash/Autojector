
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using static Autojector.Base.Types;

namespace Autojector.Features.Chains;
internal class AutojectorChainsFeature : BaseAutojectorFeature
{
    private record ChainLinkWithTypes(Type ChainLinkType,Type RequestType,Type ResponseType);
    public IChainRegisterStrategy ChainRegisterStrategy { get; }
    public override AutojectorFeaturesEnum FeatureType => AutojectorFeaturesEnum.Chains;
    public AutojectorChainsFeature(
        IEnumerable<Assembly> assemblies,
        IChainRegisterStrategy chainRegisterStrategy
        ) : base(assemblies)
    {
        ChainRegisterStrategy = chainRegisterStrategy;
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var chainLinks = NonAbstractClassesFromAssemblies
            .Where(type => type.HasAnyConcrateImplementationThatMatchGenericsDefinition(new Type[]
            {
                ChainLinkType
            }));

        var chainLinkWithTypes = chainLinks.SelectMany(d => ExtractChainLinkWithTypes(d));
        var grouped = chainLinkWithTypes.GroupBy(c => new { c.RequestType, c.ResponseType });

        return grouped.Select(c => new ChainTypeOperator(
            c.Key.RequestType, 
            c.Key.ResponseType, 
            c.Select(t => t.ChainLinkType),
            ChainRegisterStrategy
            )
        );
    }

    private IEnumerable<ChainLinkWithTypes> ExtractChainLinkWithTypes(Type type)
    {
        var implementedChainLinks = type.GetConcrateImplementationThatMatchGenericsDefinition(new Type[]
            {
                ChainLinkType
            });

        return implementedChainLinks.Select(d => new ChainLinkWithTypes(type,
            d.GetFirstArgument(),
            d.GetSecondArgument())
        );
    }
}
