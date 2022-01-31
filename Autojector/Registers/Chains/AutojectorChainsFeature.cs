﻿
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Autojector.Abstractions.Types;

namespace Autojector.Registers.Chains;
internal class AutojectorChainsFeature : BaseAutojectorFeature
{
    private record ChainLinkWithTypes(Type ChainLinkType,Type RequestType,Type ResponseType);
    public ChainRegisterStrategy ChainRegisterStrategy { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Chains;
    public AutojectorChainsFeature(IEnumerable<Assembly> assemblies, IServiceCollection services) : base(assemblies, services)
    {
        ChainRegisterStrategy = new ChainRegisterStrategy(Services);
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var chainLinks = NonAbstractClassesFromAssemblies
            .Where(type => type.GetInterfaces()
                               .Any(i => i.IsGenericType &&
                                    i.GetGenericTypeDefinition() == ChainLinkType));

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
        var implementedChainLinks = type.GetInterfaces().Where(d => d.IsGenericType &&
                                        d.GetGenericTypeDefinition() == ChainLinkType);

        return implementedChainLinks.Select(d => new ChainLinkWithTypes(type,
            d.GetGenericArguments().First(),
            d.GetGenericArguments().Skip(1).First())
        );
    }
}