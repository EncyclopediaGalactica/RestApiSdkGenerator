namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers.FileManager;
using Generator.Managers.PathManager;
using Generator.Processors.CSharp;

[ExcludeFromCodeCoverage]
public partial class CSharpProcessor_Should
{
    private readonly ICSharpProcessor _sut;

    public CSharpProcessor_Should()
    {
        _sut = new CSharpProcessor(
            new FileManagerImpl(),
            new Generator.Managers.StringManager.StringManagerImpl(),
            new PathManagerImpl());
    }
}