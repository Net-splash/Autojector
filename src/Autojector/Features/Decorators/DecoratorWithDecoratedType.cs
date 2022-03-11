using System;

namespace Autojector.Features.Decorators;

internal record DecoratorWithDecoratedType(Type Decorator, Type Decorated);
