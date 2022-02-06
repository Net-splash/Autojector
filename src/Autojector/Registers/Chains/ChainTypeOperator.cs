using Autojector.Abstractions;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Chains;
internal record ChainTypeOperator(Type RequestType, Type ResponseType, IEnumerable<Type> ChainLinks,IChainRegisterStrategy ChainRegisterStrategy) : ITypeConfigurator
{
    public void ConfigureServices()
    {
        var chainLinks = GetChainLinks();
        ChainRegisterStrategy.Add(RequestType, ResponseType, chainLinks);
    }

    private IEnumerable<Type> GetChainLinks()
    {
        var splittedDecorators = ChainLinks.ToLookup(d => d.GetCustomAttributes(typeof(ChainLinkOrderAttribute), true).Any());
        var unordedDecorators = splittedDecorators[false];
        var orderedDecorators = splittedDecorators[true].OrderBy(d => GetDecoratorOrderNumber(d)).ToList();
        return unordedDecorators.Concat(orderedDecorators);
    }

    private int GetDecoratorOrderNumber(Type d)
    {
        var attributes = d.GetCustomAttributes(typeof(ChainLinkOrderAttribute), true);
        ValidateAgainstMultipleOrderDecoratorsOnSameClass(attributes);
        var attribute = (ChainLinkOrderAttribute)attributes.First();
        return attribute.Order;
    }

    private static void ValidateAgainstMultipleOrderDecoratorsOnSameClass(object[] attributes)
    {
        if (attributes.Skip(1).Any())
        {
            throw new InvalidOperationException("Can not have multiple order operators");
        }
    }
}
