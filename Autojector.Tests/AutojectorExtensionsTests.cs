using Autojector.Public;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Autojector.Tests
{
    internal interface ITestSingleton : ISingletonInjectable{}
    internal class TestTransient : ITestSingleton {} 
    internal interface ITestDependentSingleton: ISingletonInjectable{}
    internal class TestDependentSingleton: ITestDependentSingleton
    {
        public TestDependentSingleton(ITestSingleton testSingleton)
        {
            TestSingleton = testSingleton;
        }

        public ITestSingleton TestSingleton { get; }
    }

    internal interface ITestTransientDependent : ITransientInjectable { }
    internal class TestTransientDependent : ITestTransientDependent
    {
        public  TestTransientDependent(ITestDependentSingleton testDependentSingleton)
        {
            TestDependentSingleton = testDependentSingleton;
        }

        public ITestDependentSingleton TestDependentSingleton { get; }
    }
    internal class RandomService
    {
    }
    internal class TestTransientFactory : ITransientFactoryInjectable<RandomService>
    {
        public RandomService GetService()
            => new RandomService();
    }

    public class AutojectorExtensionsTests
    {
        private ServiceCollection ServiceCollection;
        public AutojectorExtensionsTests()
        {
            ServiceCollection = new ServiceCollection();
        }

        [Fact]
        public void ShouldAddTestTransientClass()
        {
            ServiceCollection.AddAutojector();
            ServiceShouldNotBeNull<TestTransient>();
        }

        [Fact]
        public void ShouldAddTestTransientInterfaceWithClass()
        {
            ServiceCollection.AddAutojector();
            ServiceShouldNotBeNull<ITestSingleton>();
        }
        [Fact]
        public void ShouldAddDependentSingletonClass()
        {
            ServiceCollection.AddAutojector();
            ServiceShouldNotBeNull<ITestDependentSingleton>();
        }

        [Fact]
        public void ShouldAddTransientDependent()
        {
            ServiceCollection.AddAutojector();
            ServiceShouldNotBeNull<ITestTransientDependent>();
        }

        [Fact]
        public void ShouldAddRandomServiceFromFactoryDependent()
        {
            ServiceCollection.AddAutojector(opt => 
            opt.UseAutojectorSimpleInjection()
               .UseAutojectorFactories()
            );
            ServiceShouldNotBeNull<RandomService>();
        }

        private void ServiceShouldNotBeNull<T>() where T:class
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<T>();
            service.ShouldNotBeNull();
        }
    }
}
