namespace Autojector.Abstractions;

/// <summary>
/// Use this attribute on any class that implements IDecorator in order to set the order in which the decorators will be called
/// </summary>
public class DecoratorOrderAttribute : Attribute {
    public DecoratorOrderAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}
