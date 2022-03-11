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
    public ConfigInjector() : base(a => a
                .UseConfigsByInteface()
                .UseConfigsByAttribute()
                .UseUnimplementedConfigsByInteface()
                .UseUnimplementedConfigsByAttribute()
                .Build())
    {
    }

    [Fact]
    public void ShouldAddConfig()
    {
        var config = ShouldCreateConfig<TestConfig>();

        //Assert
        config.Data.ShouldBe(5);
    }

    [Fact]
    public void ShouldAddTestAttributeConfig()
    {
        var config = ShouldCreateConfig<TestAttributeConfig>();

        //Assert
        config.Data.ShouldBe(5);
    }


    [Fact]
    public void ShouldAddUnimplementedConfig()
    {
        var config = ShouldCreateConfig<IUnimplementedConfig>("UnimplementedConfig");

        //Assert
        config.Data.ShouldBe(5);
    }

    [Fact]
    public void ShouldAddUnimplementedAttributeConfig()
    {
        var config = ShouldCreateConfig<IUnimplementedAttributeConfig>("UnimplementedAttributeConfig");

        //Assert
        config.Data.ShouldBe(5);
    }

    [Fact]
    public void ShouldFailToCreateWithNonEmptyConstructor()
    {
        var code = @"
        public class MyConfig: Autojector.Abstractions.IConfig{
            public MyConfig(int i){}
            }
        ";

        ShouldThrowExceptionOnGettingServiceExternally(code, $"The type MyConfig doens't have an empty constructor as each IConfig require");
    }

    private T ShouldCreateConfig<T>(string key = null)
        where T : class
    {
        key = key == null ? typeof(T).Name : key;
        //Arrange
        var configs = (new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>($"{key}:Data","5")
        });

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configs)
            .Build();

        ServiceCollection.AddSingleton(configuration);

        //Act
        var config = ServiceShouldSucceedLocally<T>();
        return config;
    }
}
