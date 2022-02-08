
using Autojector.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Runtime.Loader;

namespace Autojector.TestAssemblyGenerator;

public interface ITestAssemblyContext : IDisposable
{
    Assembly Assembly { get; }
}

internal class TestAssemblyContext : ITestAssemblyContext
{
    private AssemblyLoadContext AssemblyLoadContext { get; set; }

    public Assembly Assembly => AssemblyLoadContext.Assemblies.FirstOrDefault();

    internal TestAssemblyContext(AssemblyLoadContext assemblyLoadContext)
    {
        AssemblyLoadContext = assemblyLoadContext;
    }

    public void Dispose()
    {
        if (AssemblyLoadContext != null)
        {
            AssemblyLoadContext.Unload();
            AssemblyLoadContext = null;
        }
    }
}

internal class AssemblyGenerator
{
    private string Code { get; }

    public AssemblyGenerator(string code)
    {
        Code = code;
    }

    public ITestAssemblyContext GetAssemblyContext()
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(Code);
        string assemblyName = Path.GetRandomFileName();
        var coreDir = Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(coreDir + Path.DirectorySeparatorChar + "mscorlib.dll"),
            MetadataReference.CreateFromFile(coreDir + Path.DirectorySeparatorChar + "System.Runtime.dll"),
            MetadataReference.CreateFromFile(typeof(ITransient<>).Assembly.Location),
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using (var ms = new MemoryStream())
        {
            EmitResult result = compilation.Emit(ms);

            if (!result.Success)
            {
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                throw new InvalidOperationException("Fail to compile");
            }
            else
            {
                ms.Seek(0, SeekOrigin.Begin);
                var assemblyContext = new AssemblyLoadContext(Guid.NewGuid().ToString(), true);
                assemblyContext.LoadFromStream(ms);
                return new TestAssemblyContext(assemblyContext);
            }
        }
    }
}
