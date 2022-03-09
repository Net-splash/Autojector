
using Autojector.Abstractions;

namespace Autojector.Tests.Chains;

internal class ChainLinkResponse{}
internal class ChainLinkRequest{}
internal class ChainLink1 : IChainLink<ChainLinkRequest, ChainLinkResponse>
{
    public bool CanHandle(ChainLinkRequest request) => false;
    
    public ChainLinkResponse Handle(ChainLinkRequest request)
    {
        throw new System.NotImplementedException();
    }
}

[ChainLinkOrder(2)]
internal class ChainLink2 : IChainLink<ChainLinkRequest, ChainLinkResponse>
{
    public bool CanHandle(ChainLinkRequest request) => true;

    public ChainLinkResponse Handle(ChainLinkRequest request)
    {
        return new ChainLinkResponse();
    }
}

[ChainLinkOrder(1)]
internal class ChainLink3 : IChainLink<ChainLinkRequest, ChainLinkResponse>
{
    public bool CanHandle(ChainLinkRequest request) => false;

    public ChainLinkResponse Handle(ChainLinkRequest request)
    {
        return null;
    }
}
