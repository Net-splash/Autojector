
using Autojector.Abstractions;

namespace Autojector.Tests.Chains;

internal class ChainLinkResponse
{

}

internal class ChainLinkRequest
{

}

internal class ChainLink1 : IChainLink<ChainLinkRequest, ChainLinkResponse>
{
    public bool CanHandleRequest(ChainLinkRequest request) => false;
    
    public ChainLinkResponse Handle(ChainLinkRequest request)
    {
        throw new System.NotImplementedException();
    }
}

internal class ChainLink2 : IChainLink<ChainLinkRequest, ChainLinkResponse>
{
    public bool CanHandleRequest(ChainLinkRequest request) => true;

    public ChainLinkResponse Handle(ChainLinkRequest request)
    {
        return new ChainLinkResponse();
    }
}
