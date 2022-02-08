using Autojector.TestAssemblyGenerator;
using Autojector.Tests.SimpleInjectableClasses;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Autojector.Tests;

public class SimpleInjectorExternalAssembly : TestBase
{
    public SimpleInjectorExternalAssembly() : base(
        a => a.UseSimpleInjection().Build()
    )
    {
    }

    [Fact]
    public void ShouldFailWhenInterfaceIsNotImplemented()
    {
        var code = @"

        public interface IMyService{}

        public class MyService: Autojector.Abstractions.ITransient<IMyService>{}
        ";

        var exceptionMessage = "The interfaces IMyService are not implemented by MyService but are registered as injectable";
        ShouldThrowOnBuildingServicesExternally(code, exceptionMessage);
    }

    [Fact]
    public void ShoulSucceedForExternalTransientWithLifetimeOnClass()
    {
        var code = @"
        public interface IMyService{}
        public class MyService: IMyService,Autojector.Abstractions.ITransient<IMyService>{}
        ";

        ShouldSucceedOnGetService(code,"IMyService", "MyService");
    }

    [Fact]
    public void ShoulSucceedForExternalTransientWithLifetimeOnInterface()
    {
        var code = @"
        public interface IMyService:Autojector.Abstractions.ITransient<IMyService>{}
        public class MyService: IMyService{}
        ";

        ShouldSucceedOnGetService(code, "IMyService", "MyService");
    }

    [Fact]
    public void ShouldFailWithInternalAssemblies()
    {
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<ITestSingleton>();
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<TestSingleton>();
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<ITestTransient>();
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<TestTransient>();
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<ITestTransientDependent>();
        ShouldFailOnGetServiceLocallyWithExternalAssemblies<TestTransientDependent>();
    }

    
}
