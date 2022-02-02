namespace Autojector.Abstractions;
/// <summary>
/// This interface should be use to mark any class as a link in the chain
/// In order to be picked up by the Autojector please make sure that the Chains was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// Any class that implements this can be injected as IChain<<typeparamref name="TRequest"/>,<typeparamref name="Tresponse"/>>
/// </summary>
public interface IChainLink<TRequest,TResponse>
{
    public bool CanHandleRequest(TRequest request);
    public TResponse Handle(TRequest request);
}
