using Autojector.Abstractions;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Registers.Decorators;
internal record DecoratorTypesOperator(
    Type DecoratedType,
    IEnumerable<Type> Decorators, 
    IDecoratorRegisterStrategy DecoratorRegisterStrategy) : ITypeConfigurator
{
    public void ConfigureServices()
    {
        var orderedDecoratorsTypes = GetOrderDecorators();
        foreach(var decoratorType in orderedDecoratorsTypes)
        {
            DecoratorRegisterStrategy.Add(decoratorType, DecoratedType);
        }
    }

    private IEnumerable<Type> GetOrderDecorators()
    {
        var splittedDecorators = Decorators.ToLookup(decoratorType => GetOrderAttributes(decoratorType).Any());
        var unordedDecorators = splittedDecorators[false];
        ValidateAgainstMultipleUnorderedDecorators(unordedDecorators);
        var orderedDecorators = splittedDecorators[true].OrderBy(GetDecoratorOrderNumber);
        return unordedDecorators.Concat(orderedDecorators);
    }

    private int GetDecoratorOrderNumber(Type decoratorType)
    {
        var orderAttributes = GetOrderAttributes(decoratorType);
        ValidateAgainstMultipleOrderDecoratorsOnSameClass(orderAttributes, decoratorType);
        var attribute = orderAttributes.First();
        return attribute.Order;
    }

    private static IEnumerable<DecoratorOrderAttribute> GetOrderAttributes(Type decoratorType)
    {
        return decoratorType.GetCustomAttributes(DecoratorOrderAttributeType, true)
            .Select(attribute => (DecoratorOrderAttribute)attribute);
    }

    private void ValidateAgainstMultipleOrderDecoratorsOnSameClass(IEnumerable<DecoratorOrderAttribute> attributes, Type decoratorType)
    {
        if (attributes.Skip(1).Any())
        {
            throw new InvalidOperationException($"Can not have multiple order operators for {decoratorType?.Name}");
        }
    }
    private void ValidateAgainstMultipleUnorderedDecorators(IEnumerable<Type> unordedDecorators)
    {
        if (unordedDecorators.Skip(1).Any())
        {
            throw new InvalidOperationException($"Can not have more than one unordered decorator for {DecoratedType.Name}");
        }
    }
}
