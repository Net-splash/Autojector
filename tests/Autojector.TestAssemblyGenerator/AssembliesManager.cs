using Autojector.TestAssemblyGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Tests;
public static class AssembliesManager
{
    private static List<string> RuntimeLoadedAssemblyNames = new List<string>();

    public static IEnumerable<Assembly> GetNonRuntimeAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !RuntimeLoadedAssemblyNames.Contains(a.FullName));
    }

    public static ITestAssemblyContext GetAssemblyContextFromCode(string code)
    {
        var assemblyGenerator = new AssemblyGenerator(code);
        ITestAssemblyContext assemblyContext = assemblyGenerator.GetAssemblyContext();
        RuntimeLoadedAssemblyNames.Add(assemblyContext.Assembly.FullName);
        return assemblyContext;
    }
}
