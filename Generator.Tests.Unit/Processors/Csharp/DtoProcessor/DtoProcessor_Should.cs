namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers;
using Generator.Processors.CSharp;

[ExcludeFromCodeCoverage]
public partial class DtoProcessor_Should
{
    private readonly IDtoProcessor _sut;

    public DtoProcessor_Should()
    {
        _sut = new DtoProcessor(
            new FileManagerImpl(),
            new StringManagerImpl(),
            new PathManagerImpl());
    }
}