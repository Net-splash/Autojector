using Autojector.Abstractions;
using Autojector.Tests.Chains;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class ChainInjector : TestBase
{
    public ChainInjector() : base(a => a.UseChains().Build())
    {
    }


    [Fact]
    public void ShouldCreateChainFromChainLinks()
    {
        //Assert
        var chain = ServiceShouldSucceedLocally<IChain<ChainLinkRequest, ChainLinkResponse>>();

        var response = chain.Handle(new ChainLinkRequest());

        response.ShouldNotBeNull();
    }

}
