

namespace Autojector.Abstractions;
public interface IChainLink<TRequest,TResponse>
{
    public bool CanHandleRequest(TRequest request);
    public TResponse Handle(TRequest request);
}


public interface IChain<TRequest,TResponse>
{
    public TResponse Handle(TRequest request);
}
