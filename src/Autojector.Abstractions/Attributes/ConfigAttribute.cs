namespace Autojector.Abstractions;
public class ConfigAttribute : Attribute
{
    public ConfigAttribute(string? key = null)
    {
        Key = key;
    }

    public string Key { get; }
}
