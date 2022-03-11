using Autojector.Tests.SimpleInjectable;
using Shouldly;
using Xunit;

namespace Autojector.Tests;

public class SimpleInjectorInternalAssembly : TestBase
{
    public SimpleInjectorInternalAssembly() : base(
            a => a.UseSimpleInjectionByInterface().UseSimpleInjectionByAttribute().Build()
        )
    {
    }

    [Fact]
    public void ShouldAddTestSingletonClass()
        => ServiceShouldSucceedLocally<TestSingleton>();

    [Fact]
    public void ShouldAddTestSingletonInterface()
        => ServiceShouldSucceedLocally<ITestSingleton>();
    
    [Fact]
    public void ShouldAddDependentSingletonClass()
        => ServiceShouldSucceedLocally<TestDependentSingleton>();

    [Fact]
    public void ShouldAddTransientClass()
        => ServiceShouldSucceedLocally<TestTransient>();

    [Fact]
    public void ShouldAddTransientInterface()
        => ServiceShouldSucceedLocally<ITestTransient>();
    
    [Fact]
    public void ShouldAddScopedInterface()
        => ServiceShouldSucceedLocally<ITestScope>();

    [Fact]
    public void ShouldAddScopedClass()
        => ServiceShouldSucceedLocally<TestScope>();
    
    [Fact]
    public void ShouldAddTransientDependentOnSingleton()
    {
        //Assert
        var service = ServiceShouldSucceedLocally<ITestTransientDependent>();
        service.TestDependentSingleton.ShouldNotBeNull<ITestDependentSingleton>();
    }

    [Fact]
    public void ShouldAddTransientClassesWithNoInterface() 
        => ServiceShouldSucceedLocally<NoInterfaceTransient>();

    [Fact]
    public void ShouldFailWhenNoDoesntImplementInjectable() 
        => ShouldFailOnGetServiceLocally<NoLifetimeType>();


    [Fact]
    public void ShouldAddTransientClassesWithAttribute()
    {
        ServiceShouldSucceedLocally<IAttributeTransient>();
    }

}
