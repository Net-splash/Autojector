using Autojector.Abstractions;

namespace Autojector.Tests.Configs;

public interface IUnimplementedConfig: IConfig
{
    public int Data { get; set; }
}
