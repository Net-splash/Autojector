using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsAddFullAutojectorTests : AutojectorBaseTest
{
    public AutojectorExtensionsAddFullAutojectorTests() : base()
    {
    }

    [Fact]
    public void ShouldAddEveryting()
    {
        //Arrange & Act
        ServiceCollection.AddAutojector();

    }

}