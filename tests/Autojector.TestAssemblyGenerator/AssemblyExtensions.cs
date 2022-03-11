using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autojector.TestAssemblyGenerator;
public static class AssemblyExtensions
{
    public static Type GetTypeFromAssemblyByName(this Assembly assembly, string typeName)
    {
        return assembly.GetTypes().Where(t => t.Name == typeName).First();
    }
}
