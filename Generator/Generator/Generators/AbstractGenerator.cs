namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Configuration;
using Managers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public abstract class AbstractGenerator : ICodeGenerator
{
    private Logger<AbstractGenerator> _logger = new Logger<AbstractGenerator>(LoggerFactory.Create(
        c => c.AddConsole()));

    protected IFileManager FileManager;

    protected CodeGeneratorConfiguration GeneratorConfiguration;

    protected OpenApiDocument OpenApiYamlSchema;

    protected IPathManager PathManager;

    protected IStringManager StringManager;

    protected ITemplateManager TemplateManager;
    protected abstract string DtoTemplatePath { get; }

    public List<FileInfo> DtoFileInfos { get; } = new List<FileInfo>();

    public List<FileInfo> DtoTestFileInfos { get; } = new List<FileInfo>();

    public ICodeGenerator SetTemplateManager(ITemplateManager templateManager)
    {
        ArgumentNullException.ThrowIfNull(templateManager);
        TemplateManager = templateManager;
        return this;
    }

    public ICodeGenerator SetGeneratorConfiguration(CodeGeneratorConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        GeneratorConfiguration = configuration;
        return this;
    }

    public ICodeGenerator SetOpenApiYamlSchema(OpenApiDocument openApiDocument)
    {
        ArgumentNullException.ThrowIfNull(openApiDocument);
        OpenApiYamlSchema = openApiDocument;
        return this;
    }

    public ICodeGenerator SetFileManager(IFileManager fileManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        FileManager = fileManager;
        return this;
    }

    public ICodeGenerator SetPathManager(IPathManager pathManager)
    {
        ArgumentNullException.ThrowIfNull(pathManager);
        PathManager = pathManager;
        return this;
    }

    public abstract ICodeGenerator Generate();
    public abstract void GenerateDtos();
    public abstract void GenerateDtosTests();

    public ICodeGenerator SetStringManager(IStringManager stringManager)
    {
        ArgumentNullException.ThrowIfNull(stringManager);
        StringManager = stringManager;
        return this;
    }

    public abstract ICodeGenerator Initialize();
    public abstract void PreProcessDtos();

    protected bool ShouldIRunDtoGeneration()
    {
        return GeneratorConfiguration.SkipDtoGenerating;
    }

    protected bool ShouldIRunDtoPreProcessing()
    {
        return GeneratorConfiguration.SkipDtoPreProcess;
    }

    protected bool ShouldIRunDtoTestGeneration()
    {
        return GeneratorConfiguration.SkipDtoTestsGenerating;
    }

    protected void GetOriginalBaseNamespaceTokenFromConfiguration()
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.SolutionBaseNamespace)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.SolutionBaseNamespace))
        {
            _logger.LogInformation("No Dto project namespace is provided");
        }

        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            fileInfo.OriginalBaseNamespaceToken = GeneratorConfiguration.SolutionBaseNamespace;
        }
    }

    protected void GetOriginalDtoNamespaceTokenFromConfiguration()
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectNameSpace)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectNameSpace))
        {
            _logger.LogInformation("No Dto project namespace is provided");
            return;
        }

        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            fileInfo.OriginalDtoNamespaceToken = GeneratorConfiguration.DtoProjectNameSpace;
        }
    }

    protected void GetOriginalTargetPathFromConfiguration()
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("No target directory is provided");
            return;
        }

        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            fileInfo.OriginalTargetDirectoryToken = GeneratorConfiguration.TargetDirectory;
        }
    }

    protected void GetOriginalDtoProjectBasePathFromConfiguration()
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectBasePath)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectBasePath))
        {
            _logger.LogInformation("No dto project base path is provided");
            return;
        }

        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            fileInfo.OriginalDtoPojectBasePathToken = GeneratorConfiguration.DtoProjectBasePath;
        }
    }

    protected void GetOriginalDtoProjectAdditionalPathFromConfiguration()
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectAdditionalPath)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectAdditionalPath))
        {
            _logger.LogInformation("No dto project additional path is provided");
            return;
        }

        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            fileInfo.OriginalDtoProjectAdditionalPathToken = GeneratorConfiguration.DtoProjectAdditionalPath;
        }
    }

    protected void GetOriginalTypeNameTokenFromOpenApiSchema()
    {
        if (!OpenApiYamlSchema.Components.Schemas.Any())
        {
            _logger.LogInformation("No schemas are available in the YAML file");
            return;
        }

        foreach (KeyValuePair<string, OpenApiSchema> schema in OpenApiYamlSchema.Components.Schemas)
        {
            DtoFileInfos.Add(
                new FileInfo
                {
                    OriginalTypeNameToken = schema.Key
                });
        }
    }

    protected void GetOriginalPropertyMetadataFromOpenApiSchema()
    {
        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No file info metadata available");
            return;
        }

        if (!OpenApiYamlSchema.Components.Schemas.Any())
        {
            _logger.LogInformation("No schemas are available in the YAML file");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            KeyValuePair<string, OpenApiSchema> openApiSchemaKeyValuePair = OpenApiYamlSchema.Components.Schemas
                .First(p => p.Key == fileInfo.OriginalTypeNameToken);

            if (!openApiSchemaKeyValuePair.Value.Properties.Any())
            {
                _logger.LogInformation("Schema name - {SchemaName} - does not have any property",
                    fileInfo.OriginalTypeNameToken);
                continue;
            }

            fileInfo.RequiredProperties = openApiSchemaKeyValuePair.Value.Required.ToList();
            fileInfo.RequiredProperties = fileInfo.RequiredProperties.ConvertAll(d => d.ToLower());

            foreach (KeyValuePair<string, OpenApiSchema> property in openApiSchemaKeyValuePair.Value.Properties)
            {
                fileInfo.PropertyInfos.Add(new PropertyInfo
                {
                    OriginalPropertyNameToken = property.Key,
                    OriginalPropertyTypeNameToken = property.Value.Type,
                    OriginalPropertyTypeFormat = property.Value.Format
                });
            }
        }
    }
}