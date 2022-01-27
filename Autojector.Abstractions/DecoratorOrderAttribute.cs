namespace Autojector.Abstractions;

public class DecoratorOrderAttribute : Attribute {
    public DecoratorOrderAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}
