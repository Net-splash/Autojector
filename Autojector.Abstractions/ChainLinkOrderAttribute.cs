namespace Autojector.Abstractions;
public class ChainLinkOrderAttribute : Attribute
{
    public ChainLinkOrderAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}
