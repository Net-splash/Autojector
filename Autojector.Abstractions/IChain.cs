namespace Autojector.Abstractions;
/// <summary>
/// This interface should not be implemented outside Autojector
/// This will be provided when IChainLink is implemented and the Chains feature is activated in autojector.
/// Will agrgate all IChainLinks by TRequest and TResponse
/// </summary>
public interface IChain<TRequest,TResponse>
{
    public TResponse Handle(TRequest request);
}
