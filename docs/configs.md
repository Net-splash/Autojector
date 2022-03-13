In case you just want to bind the data from `IConfiguration` to an instance of a class you can declare your class as a config.
This feature will allow you to just create a class for the data that you have in config and use it.

You should implement only
```c#
IConfig
```
This will mark the class as a config and will populate that class when it's requested in a constructor.

Here is an implementation example for class config. For this you will need to add `UseConfigsByInteface` in the configuration option of simply `AddAutojector`

Implementation example

```c#

public class MyConfig : IConfig
{
    public string Data { get; set; }
}
```

This will bind for example the data that you have in your `appsettings.json` in case there is a section called MyConfig to an instance of this class.




Here is an implementation example for class config. For this you will need to add `UseConfigsByAttribute` in the configuration option of simply `AddAutojector`
```c#
[Config]
public class MyConfig : IConfig
{
    public string Data { get; set; }
}
```

Here is an implementation example for unimplemented interface. For this you will need to add `UseUnimplementedConfigsByInteface` in the configuration option of simply `AddAutojector`
```c#
public interface IUnimplmentedConfig : IConfig{
    public string Data { get; set; }

}
```

Here is an implementation example for unimplemented interface. For this you will need to add `UseUnimplementedConfigsByAttribute` in the configuration option of simply `AddAutojector`
```c#
[Config]
public interface IUnimplmentedConfig{
    public string Data { get; set; }

}
```

