using Autojector.Abstractions;
using Autojector.Features.Chains.Exceptions;
using System.Collections.Generic;

namespace Autojector.Features.Chains;
internal class Chain<TRequest, TResponse> : IChain<TRequest, TResponse>
{
    private IEnumerable<IChainLink<TRequest, TResponse>> ChainLinks { get; }
    public Chain(IEnumerable<IChainLink<TRequest,TResponse>> chainLinks)
    {
        ChainLinks = chainLinks;
    }

    public TResponse Handle(TRequest request)
    {
        foreach(var chainLink in ChainLinks)
        {
            if (chainLink.CanHandle(request))
            {
                return chainLink.Handle(request);
            }
        }

        throw new UnableToHandleTheRequestException();
    }
}
