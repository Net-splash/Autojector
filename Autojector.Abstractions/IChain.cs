

namespace Autojector.Abstractions;

public interface IChain<TRequest,TResponse>
{
    public TResponse Handle(TRequest request);
}
