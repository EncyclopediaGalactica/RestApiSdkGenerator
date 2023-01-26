namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using Generator.Managers;
using Generator.Processors.CSharp;

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