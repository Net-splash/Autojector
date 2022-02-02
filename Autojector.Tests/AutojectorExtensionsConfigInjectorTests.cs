using Autojector.Tests.Configs;
using Autojector.Tests.Decorators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsConfigInjectorTests : AutojectorBaseTest
{
    public AutojectorExtensionsConfigInjectorTests() : base()
    {
    }

    [Fact]
    public void ShouldAddConfig()
    {
        //Arrange
        var configs = (new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("TestConfig:Value","5")
        });

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configs)
            .Build();

        ServiceCollection.AddSingleton(configuration);
        
        //Act
        ServiceCollection.WithAutojector(a => a.UseConfigs().Build());

        //Assert
        var config = ServiceShouldNotBeNull<TestConfig>();

        config.Value.ShouldBe(5);
    }
}
