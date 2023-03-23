namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers;
using Managers.FileManager;
using Microsoft.Extensions.Logging;

public partial class CSharpProcessor : ProcessorAbstract, ICSharpProcessor
{
    private readonly IFileManager _fileManager;

    private readonly ILogger<CSharpProcessor> _logger = new Logger<CSharpProcessor>(
        LoggerFactory.Create(o => o.AddConsole()));

    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;

    public CSharpProcessor(IFileManager fileManager, IStringManager stringManager, IPathManager pathManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        ArgumentNullException.ThrowIfNull(stringManager);
        ArgumentNullException.ThrowIfNull(pathManager);

        _fileManager = fileManager;
        _stringManager = stringManager;
        _pathManager = pathManager;
    }
}