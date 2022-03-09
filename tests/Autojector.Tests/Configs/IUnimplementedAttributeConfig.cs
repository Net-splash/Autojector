using Autojector.Abstractions;
namespace Autojector.Tests.Configs;

[Config]
public interface IUnimplementedAttributeConfig
{
    public int Data { get; set; }
}
