using Autojector.Abstractions;
using Autojector.TestAssemblyGenerator;
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

    [Fact]
    public void ShouldCreateChianWithUnorderedChinLinks()
    {
        var code = @"
            namespace AA;
            internal class ChainLinkResponse{}
            internal class ChainLinkRequest {}
            internal class Chain1 : Autojector.Abstractions.IChainLink<ChainLinkRequest,ChainLinkResponse>{
                public bool CanHandle(ChainLinkRequest request) => false;

                public ChainLinkResponse Handle(ChainLinkRequest request)
                {
                    return null;
                }
            }

            internal class Chain2 : Autojector.Abstractions.IChainLink<ChainLinkRequest,ChainLinkResponse>{
                public bool CanHandle(ChainLinkRequest request) => false;

                public ChainLinkResponse Handle(ChainLinkRequest request)
                {
                    return null;
                }
            }

            internal class Chain3 : Autojector.Abstractions.IChainLink<ChainLinkRequest,ChainLinkResponse>{
                public bool CanHandle(ChainLinkRequest request) => true;

                public ChainLinkResponse Handle(ChainLinkRequest request)
                {
                    return new ChainLinkResponse();
                }
            }
        ";


        using (ITestAssemblyContext testAssemblyContext = AssembliesManager.GetAssemblyContextFromCode(code))
        {
            ServiceCollection.WithAutojector(ConfigureOptions, testAssemblyContext.Assembly);
            var requestType = testAssemblyContext.Assembly.GetTypeFromAssembly("ChainLinkRequest");
            var responseType = testAssemblyContext.Assembly.GetTypeFromAssembly("ChainLinkResponse");
            var chainType = typeof(IChain<,>).MakeGenericType(new System.Type[] { requestType ,responseType});
            var service = ShouldSucceedOnGetService(chainType);
        }
    }

}
