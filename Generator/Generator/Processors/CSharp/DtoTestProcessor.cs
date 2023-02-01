namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers;

public class DtoTestProcessor : IDtoTestsProcessor
{
    private readonly IFileManager _fileManager;
    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;

    public DtoTestProcessor(IFileManager fileManager, IStringManager stringManager, IPathManager pathManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        ArgumentNullException.ThrowIfNull(stringManager);
        ArgumentNullException.ThrowIfNull(pathManager);

        _fileManager = fileManager;
        _stringManager = stringManager;
        _pathManager = pathManager;
    }
}