

using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.Decorators;
internal record DecoratorTypesOperator(Type DecoratedType,IEnumerable<Type> Decorators) : BaseOperator, ITypeConfigurator
{
    public static Type DecoratorType = typeof(IDecorator<>);
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var decoratorRegisterStrategy = new DecoratorRegisterStrategy(services);
        var orderedDecorators = GetOrderDecorators();
        foreach(var decorator in orderedDecorators)
        {
            decoratorRegisterStrategy.Add(decorator, DecoratedType);
        }

        return services;
    }

    private IEnumerable<Type> GetOrderDecorators()
    {
        var splittedDecorators = Decorators.ToLookup(d => d.GetCustomAttributes(typeof(DecoratorOrderAttribute), true).Any());

        var unordedDecorators = splittedDecorators[false];

        if (unordedDecorators.Skip(1).Any())
        {
            throw new InvalidOperationException("Can not have more than on unordered decorator");
        }

        var orderedDecorators = splittedDecorators[true].OrderBy( d => GetDecoratorOrderNumber(d)).ToList();
        return unordedDecorators.Concat(orderedDecorators);
    }

    private int GetDecoratorOrderNumber(Type d)
    {
        var attributes = d.GetCustomAttributes(typeof(DecoratorOrderAttribute), true);
        if (attributes.Skip(1).Any())
        {
            throw new InvalidOperationException("Can not have multiple order operators");
        }

        var attribute = (DecoratorOrderAttribute)attributes.First();

        return attribute.Order;
    }
}
