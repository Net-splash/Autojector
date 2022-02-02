namespace Autojector.Abstractions;
public interface IChainLink<TRequest,TResponse>
{
    public bool CanHandleRequest(TRequest request);
    public TResponse Handle(TRequest request);
}
