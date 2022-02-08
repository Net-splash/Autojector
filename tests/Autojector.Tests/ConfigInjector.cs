using Autojector.Tests.Configs;
using Autojector.Tests.Decorators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Autojector.Tests;

public class ConfigInjector : TestBase
{
    public ConfigInjector() : base(a => a.UseConfigs().Build())
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
        
        //Assert
        var config = ServiceShouldSucceedLocally<TestConfig>();

        config.Value.ShouldBe(5);
    }
}
