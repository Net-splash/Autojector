using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.Chains;
internal interface IChainRegisterStrategy
{
    void Add(Type requestType, Type responseType, IEnumerable<Type> chainLinks);
}

internal class ChainRegisterStrategy : IChainRegisterStrategy
{
    public ChainRegisterStrategy(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }

    public void Add(Type requestType, Type responseType, IEnumerable<Type> chainLinks)
    {
        var chainLinkInterfaceType = typeof(IChainLink<,>).MakeGenericType(requestType, responseType);
        var chainType = typeof(Chain<,>).MakeGenericType(requestType, responseType);
        var enumerableChainType = typeof(IEnumerable<>).MakeGenericType(chainLinkInterfaceType);
        var chainConstructor = chainType.GetConstructor(new [] { enumerableChainType });
        foreach (var chainLink in chainLinks)
        {
            Services.AddTransient(chainLinkInterfaceType, chainLink);
        }

        var chainInterfaceType = typeof(IChain<,>).MakeGenericType(requestType, responseType);
        Services.AddTransient(chainInterfaceType, (serviceProvider) =>
        {
            var chainLinksImplementation = serviceProvider.GetService(enumerableChainType);
            var chain = chainConstructor.Invoke(new[] { chainLinksImplementation });
            return chain;
        });
    }
}

