In case you just want to bind the data from `IConfiguration` to an instance of a class you can declare your class as a config.
This feature will allow you to just create a class for the data that you have in config and use it.

You should implement only
```c#
IConfig
```
This will mark the class as a config and will populate that class when it's requested in a constructor.

Implementation example

```c#

public class MyConfig : IConfig
{
    public string Data { get; set; }
}
```

This will bind for example the data that you have in your `appsettings.json` in case there is a section called MyConfig to an instance of this class.
