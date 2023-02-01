namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoTestsProcessor;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers;
using Generator.Processors.CSharp;

[ExcludeFromCodeCoverage]
public partial class DtoTestProcessor_Should
{
    private readonly IDtoTestsProcessor _sut;

    public DtoTestProcessor_Should()
    {
        _sut = new DtoTestProcessor(
            new FileManagerImpl(),
            new StringManagerImpl(),
            new PathManagerImpl());
    }
}