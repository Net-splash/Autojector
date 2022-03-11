namespace Autojector.Abstractions;
/// <summary>
/// This attribute should be use to mark any class as a config.
/// In order to be picked up by the Autojector please make sure that the Configs was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// Any class that implements this can be injected and will be populated from Configuration
/// </summary>
public class ConfigAttribute : Attribute
{
    /// <summary>
    /// The constrcutor for the config
    /// </summary>
    /// <param name="key">
    /// This is the key from the configuration. That is where your data will be extracted from
    /// </param>
    public ConfigAttribute(string? key = null)
    {
        Key = key;
    }

    /// <summary>
    /// The key where the data will be store in configuration for this class
    /// </summary>
    public string? Key { get; }
}
