using Autojector.Abstractions;
using Autojector.Tests.Chains;
using Autojector.Tests.Decorators;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsChainInjectorTests : AutojectorBaseTest
{
    public AutojectorExtensionsChainInjectorTests() : base()
    {
    }


    [Fact]
    public void ShouldCreateChainFromChainLinks()
    {
        //Arrange & Act
        ServiceCollection.WithAutojector(a => a.UseChains()) ;

        //Assert
       var chain = ServiceShouldNotBeNull<IChain<ChainLinkRequest, ChainLinkResponse>>();

        var response = chain.Handle(new ChainLinkRequest());

        response.ShouldBeNull();
    }

}
