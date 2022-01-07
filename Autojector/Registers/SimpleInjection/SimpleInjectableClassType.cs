using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.SimpleInjection
{
    public record SimpleInjectableClassType(Type ClassType)
    {
        public IEnumerable<Type> CustomInterfaces 
                => ClassType.GetInterfaces().Where(i => 
                !SimpleInjectableTypes.InjectableInterfaces.Contains(i) && 
                !SimpleInjectableTypes.SimpleLifeTypeInterfaces.Contains(i));

        private IEnumerable<Type> _lifetypeManagementInterfaces;
        public IEnumerable<Type> LifetypeManagementInterfaces =>
            _lifetypeManagementInterfaces ?? (_lifetypeManagementInterfaces = GetLifeTypeInterfaces(ClassType));

        public IEnumerable<Type> GetLifeTypeInterfaces(Type type)
        {
            var lifeTypeInterface = type.GetInterfaces().Where(i => SimpleInjectableTypes.SimpleLifeTypeInterfaces.Contains(i));
            var otherInterfaces = type.GetInterfaces().Where(i => !SimpleInjectableTypes.SimpleLifeTypeInterfaces.Contains(i));
            var lifeTypeInterfacesFromOtherInterfaces = otherInterfaces.SelectMany(x => GetLifeTypeInterfaces(x));

            return lifeTypeInterface.Concat(lifeTypeInterfacesFromOtherInterfaces).Distinct();
        }

        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var lifetypeRegisterStrategyFactory = new LifetypeRegisterStrategyFactory(services);
            foreach (var lifetypeInterface in LifetypeManagementInterfaces)
            {
                var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(lifetypeInterface);
                AddSimpleLifetype(lifetypeRegisterStrategy);
            }

            return services;
        }

        private void AddSimpleLifetype(ISimpleLifetypeRegisterStrategy lifetypeRegisterStrategy)
        {
            if (!CustomInterfaces.Any())
            {
                lifetypeRegisterStrategy.Add(ClassType);
                return;
            }

            foreach (var interfaceType in CustomInterfaces)
            {
                lifetypeRegisterStrategy.Add(ClassType, interfaceType);
            }
        }
    }
}
