using Autojector.Abstractions;

namespace Sample.Services;
public record MyRequest();
public record MyResponse(string Data);
internal class ChainLink1 : IChainLink<MyRequest, MyResponse>
{
    public bool CanHandleRequest(MyRequest request)
    {
        return false;
    }

    public MyResponse Handle(MyRequest request)
    {
        return new MyResponse("ChainLink1Data");
    }
}

internal class ChainLink2 : IChainLink<MyRequest, MyResponse>
{
    public bool CanHandleRequest(MyRequest request)
    {
        return true;
    }

    public MyResponse Handle(MyRequest request)
    {
        return new MyResponse("ChainLink2Data");
    }
}
