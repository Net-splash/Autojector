namespace Autojector.Abstractions;
/// <summary>
/// Use this attribute on any class that implements IChainLInk\f[TRequest,TResponse\f] in order to set the order in which the ChainLinks will be called
/// </summary>
public class ChainLinkOrderAttribute : Attribute
{
    public ChainLinkOrderAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}
