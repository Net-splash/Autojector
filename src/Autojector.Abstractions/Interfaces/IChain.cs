namespace Autojector.Abstractions
{
    /// <summary>
    /// This interface should not be implemented outside Autojector
    /// This will be provided when IChainLink is implemented and the Chains feature is activated in autojector.
    /// Will agrgate all IChainLinks by TRequest and TResponse
    /// </summary>
    public interface IChain<TRequest, TResponse>
    {
        /// <summary>
        /// The implementation for this method will be provided by Autojector and will represent an iteration over the chain links
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The response from the chain</returns>
        TResponse Handle(TRequest request);
    }
}
