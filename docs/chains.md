This is a more advance feature.
In case you need a set of chains of responsability that will handle your request you can use this feature
This feature will allow you to determine which implementation of the chain is the best to use.

You should implement
```c#
IChainLink<TRequest,TResponse>
```

Similar to the way MediatR works this will search for a all the classes that have the same TRequest and TResponse and it will chain them.

After marking at least one as `IChainLink<>` it will provide a class called `IChain<TRequest,TResponse>` where TRequest and TResponse is the same type from 
`IChainLink<>`

After that it will iterate over all instances of `IChainLink<>` and it will call the method `CanHandleRequest` and in case it will find a proper `IChainLink` that will return true.
It will call the method `Handle` which will return the response.
Both `IChainLink` and `Handle` the same as the `Handle` method from `IChain` receive as an input the TRequest.

After you inject the `IChain<TRequest,TResponse>` you can use the handle method to start the chaining.

Implementation example

```c#
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
```
After this you can just request the chain in your service constructor
```c#
internal class MyService{
    public MyService(IChian<MyRequest,MyResponse> chain)
    {

    }
}
```
Calling the method `Handle` will trigger one by one the `CanHandleRequest` method until one returns true.
If none returns true an exception will appear.


In case you want to enforce the order of the chains please add `ChainLinkOrder` as an attribute with an int value as a constructor and it will chain them in order.
