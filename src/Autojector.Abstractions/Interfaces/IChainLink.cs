namespace Autojector.Abstractions;
/// <summary>
/// This interface should be use to mark any class as a link in the chain
/// In order to be picked up by the Autojector please make sure that the Chains was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// Any class that implements this can be injected as IChain\f[TRequest,TResponse\f]
/// </summary>
public interface IChainLink<TRequest,TResponse>
{
    /// <summary>
    /// You will be force to implement this method when you are going to create you first chain by implementing multiple chain links
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Should return true only if the current implementation is able to handle the request</returns>
    public bool CanHandle(TRequest request);
    /// <summary>
    /// You will be force to implement this method when you are going to create you first chain by implementing multiple chain links
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The response</returns>
    public TResponse Handle(TRequest request);
}
