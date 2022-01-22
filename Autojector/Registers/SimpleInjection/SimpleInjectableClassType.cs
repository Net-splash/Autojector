using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.SimpleInjection;
public record SimpleInjectableClassType(Type ClassType)
{
    public IEnumerable<Type> CustomInterfaces
            => ClassType.GetInterfaces().Where(i =>
            !SimpleInjectableTypes.InjectableInterfaces.Contains(i) &&
            !SimpleInjectableTypes.SimpleLifeTypeInterfaces.Contains(i));

    private IEnumerable<Type> _lifetypeManagementInterfaces;
    private IEnumerable<Type> LifetypeManagementInterfaces =>
        _lifetypeManagementInterfaces ?? (_lifetypeManagementInterfaces = GetLifeTypeInterfaces(ClassType));

    private IEnumerable<Type> GetCustomInterfaceFromLifetypeToRegister()
    {
        var customInterfaceFromLifeType = LifetypeManagementInterfaces.Select(i => i.GetGenericArguments().First());
        var interfacesThatAreNotInTheClass = customInterfaceFromLifeType.Except(CustomInterfaces.Concat(new Type[] { ClassType }));
        if (interfacesThatAreNotInTheClass.Any())
        {
            var interfacesNames = interfacesThatAreNotInTheClass.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($"The interfaces {interfacesListNames} are not implemented by {ClassType} but are registered as injectable");
        }

        return customInterfaceFromLifeType;
    }

    private IEnumerable<Type> GetLifeTypeInterfaces(Type type)
    {
        var allInterfaces = type.GetInterfaces();
        var groupedInterfaces = allInterfaces.ToLookup(i => i.IsGenericType && SimpleInjectableTypes.SimpleLifeTypeInterfaces.Contains(i.GetGenericTypeDefinition()));
        var lifeTypeInterface = groupedInterfaces[true];
        var otherInterfaces = groupedInterfaces[false];
        var lifeTypeInterfacesFromOtherInterfaces = otherInterfaces.SelectMany(x => GetLifeTypeInterfaces(x));

        return lifeTypeInterface.Concat(lifeTypeInterfacesFromOtherInterfaces).Distinct();
    }

    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        if (!LifetypeManagementInterfaces.Any())
        {
            var lifetypeInterfacesNames = SimpleInjectableTypes.SimpleLifeTypeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {ClassType.Name} doesn't implement a LifeType interface
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }

        var lifetypeRegisterStrategyFactory = new LifetypeRegisterStrategyFactory(services);
        foreach (var lifetypeInterface in LifetypeManagementInterfaces)
        {
            var lifetypeRegisterStrategy = lifetypeRegisterStrategyFactory.GetSimpleLifetypeRegisterStrategy(lifetypeInterface.GetGenericTypeDefinition());
            AddSimpleLifetype(lifetypeRegisterStrategy);
        }

        return services;
    }

    private void AddSimpleLifetype(ISimpleLifetypeRegisterStrategy lifetypeRegisterStrategy)
    {
        var customInterfacesLifetype = GetCustomInterfaceFromLifetypeToRegister();
        foreach (var interfaceType in customInterfacesLifetype)
        {
            lifetypeRegisterStrategy.Add(ClassType, interfaceType);
        }
    }
};
