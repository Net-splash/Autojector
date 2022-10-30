using System;

namespace Autojector.Features.Decorators
{
    internal class DecoratorWithDecoratedType
    {
        public DecoratorWithDecoratedType(Type decorator, Type decorated)
        {
            Decorator = decorator;
            Decorated = decorated;
        }

        public Type Decorator { get; }
        public Type Decorated { get; }
    }
}
