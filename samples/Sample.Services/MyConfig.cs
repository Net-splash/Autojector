using Autojector.Abstractions;
namespace Sample.Services;
public class MyConfig : IConfig
{
    public string Data { get; set; }
}

public interface IUnimplmentedConfig : IConfig{
    public string Data { get; set; }

}