using Autojector.Tests.SimpleInjectableClasses;
using InjectableClassesAssembly;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Autojector.Tests;

public class AutojectorExtensionsSimpleInjectorFromExternalAssembly : AutojectorBaseTest
{
    public AutojectorExtensionsSimpleInjectorFromExternalAssembly() : base()
    {
    }


    [Fact]
    public void ShouldAddTestSingletonClass()
    {
        // Arrange & Act
        AddAutojectorWithExternalAssembly();

        // Assert
        ServiceShouldNotBeNull<IExternalInjectableSingleton>();
    }

    [Fact]
    public void ShouldFailWithInternalAssemblies()
    {
        // Arrange & Act
        AddAutojectorWithExternalAssembly();

        // Act & Assert
        ShouldFailOnGetService<ITestSingleton>();
        ShouldFailOnGetService<TestSingleton>();
        ShouldFailOnGetService<ITestTransient>();
        ShouldFailOnGetService<TestTransient>();
        ShouldFailOnGetService<ITestTransientDependent>();
        ShouldFailOnGetService<TestTransientDependent>();
    }
    
    private void AddAutojectorWithExternalAssembly()
    {
        ServiceCollection.WithAutojector(a => a.UseSimpleInjection(GetExternalAssemby()).Build());
    }

}
