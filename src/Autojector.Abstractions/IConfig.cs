namespace Autojector.Abstractions;
/// <summary>
/// This interface should be use to mark any class as a config.
/// In order to be picked up by the Autojector please make sure that the Configs was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// Any class that implements this can be injected and will be populated from Configuration
/// </summary>
public interface IConfig : IInjectable
{
}

