﻿using Autojector.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers.SimpleInjection;
public record SimpleInjectableTypeOperator(Type Type) : BaseTypeOperator(Type), ITypeConfigurator
{
    public static Type TransientInjectableType = typeof(ITransientInjectable<>);
    public static Type ScopeInjectableType = typeof(IScopeInjectable<>);
    public static Type SingletonInjectableType = typeof(ISingletonInjectable<>);
    public static IEnumerable<Type> SimpleLifetimeInterfaces = new List<Type>()
        {
            TransientInjectableType,
            ScopeInjectableType,
            SingletonInjectableType
        };

    public static IEnumerable<Type> InjectableInterfaces = new List<Type>()
            {
                CommonInjectableTypes.InjectableType,
            };
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var injectableTypes = GetInjectableInterfaces();

        ValidateUnknownInjectableType(injectableTypes);
        ValidateNotImplementedInterface(injectableTypes);

        var registerStrategyFactory = new SimpleRegisterStrategyFactory(services);

        foreach (var injectableType in injectableTypes)
        {
            var injectableInterface = injectableType.GetGenericArguments().First();
            var registerStrategy = registerStrategyFactory.GetSimpleLifetypeRegisterStrategy(injectableType.GetGenericTypeDefinition());
            registerStrategy.Add(injectableInterface, Type);
        }

        return services;
    }

    private IEnumerable<Type> GetInjectableInterfaces()
    {
        var filteredInterfaces = this.GetInterfacesFromTree(i =>
                i.IsGenericType &&
                SimpleLifetimeInterfaces.Contains(i.GetGenericTypeDefinition()));
        return filteredInterfaces;
    }

    private void ValidateNotImplementedInterface(IEnumerable<Type> lifetypeManagementInterfaces)
    {
        var customInterface = Type.GetInterfaces()
            .Where(i => !InjectableInterfaces.Contains(i) &&
                        !SimpleLifetimeInterfaces.Contains(i))
            .Concat(new Type[] { Type });

        var customInterfaceFromLifeType = lifetypeManagementInterfaces.Select(i => i.GetGenericArguments().First());
        var notInterfacesByClass = customInterfaceFromLifeType.Except(customInterface);
        if (notInterfacesByClass.Any())
        {
            var interfacesNames = notInterfacesByClass.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($"The interfaces {interfacesListNames} are not implemented by {Type} but are registered as injectable");
        }
    }

    private void ValidateUnknownInjectableType(IEnumerable<Type> injectableInterface)
    {
        if (!injectableInterface.Any())
        {
            var lifetypeInterfacesNames = SimpleLifetimeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {Type.Name} doesn't implement a LifeType interface
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }
    }
}
