using Autojector.Abstractions;
using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Features.Decorators
{
    internal class DecoratorTypesOperator : ITypeConfigurator
    {
        public Type DecoratedType { get; }
        public IEnumerable<Type> Decorators { get; }
        public IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }

        public DecoratorTypesOperator(
            Type decoratedType,
            IEnumerable<Type> decorators,
            IDecoratorRegisterStrategy decoratorRegisterStrategy)
        {
            DecoratedType = decoratedType;
            Decorators = decorators;
            DecoratorRegisterStrategy = decoratorRegisterStrategy;
        }

        public void ConfigureServices()
        {
            var orderedDecoratorsTypes = GetOrderDecorators();
            foreach (var decoratorType in orderedDecoratorsTypes)
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
            if (attributes.HasMany())
            {
                throw new InvalidOperationException($"Can not have multiple order operators for {decoratorType?.Name}");
            }
        }
        private void ValidateAgainstMultipleUnorderedDecorators(IEnumerable<Type> unordedDecorators)
        {
            if (unordedDecorators.HasMany())
            {
                throw new InvalidOperationException($"Can not have more than one unordered decorator for {DecoratedType.Name}");
            }
        }
    }

}