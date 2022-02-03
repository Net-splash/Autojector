namespace Autojector;

/// <summary>
/// This is the type returned by the autojector builder and is not intendend for anything else.
/// This interface is used internally by the autojector library.
/// We strongly recomand you to not use it.
/// </summary>
public interface IAutojectorService 
{
    /// <summary>
    /// This method is not intended to be called outside of autojector library. 
    /// Please do not use it.
    /// </summary>
    void ConfigureServices();
}
