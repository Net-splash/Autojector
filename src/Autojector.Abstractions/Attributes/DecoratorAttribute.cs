namespace Autojector.Abstractions;

public class DecoratorAttribute : Attribute
{
    public DecoratorAttribute(Type decoratedType) 
    {
        DecoratedType = decoratedType;
    }

    public Type DecoratedType { get; }
}