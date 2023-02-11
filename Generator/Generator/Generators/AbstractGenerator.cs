namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Configuration;
using Managers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public abstract class AbstractGenerator : ICodeGenerator
{
    private readonly Logger<AbstractGenerator> _logger = new Logger<AbstractGenerator>(LoggerFactory.Create(
        c => c.AddConsole()));

    protected IFileManager FileManager;

    protected CodeGeneratorConfiguration GeneratorConfiguration;
    protected IOpenApiToTypeInfoManager OpenApiToTypeInfoManager;

    protected OpenApiDocument OpenApiYamlSchema;

    protected IPathManager PathManager;

    protected IStringManager StringManager;

    protected ITemplateManager TemplateManager;
    public abstract string DtoTemplatePath { get; }

    public List<TypeInfo> DtoTypeInfos { get; } = new List<TypeInfo>();

    public List<TypeInfo> DtoTestTypeInfos { get; } = new List<TypeInfo>();

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

    public ICodeGenerator SetStringManager(IStringManager stringManager)
    {
        ArgumentNullException.ThrowIfNull(stringManager);
        StringManager = stringManager;
        return this;
    }

    public abstract ICodeGenerator Build();

    public ICodeGenerator SetOpenApiToFileInfoManager(IOpenApiToTypeInfoManager openApiToTypeInfoManager)
    {
        ArgumentNullException.ThrowIfNull(openApiToTypeInfoManager);
        OpenApiToTypeInfoManager = openApiToTypeInfoManager;
        return this;
    }

    public abstract void PreProcessDtos();

    protected bool ShouldIRunDtoGeneration()
    {
        return GeneratorConfiguration.SkipDtoGenerating;
    }

    protected bool ShouldIRunDtoPreProcessing()
    {
        return GeneratorConfiguration.SkipDtoPreProcess;
    }

    protected bool ShouldIRunDtoTestPreProcessing()
    {
        return GeneratorConfiguration.SkipDtoTestPreProcessing;
    }

    protected bool ShouldIRunDtoTestGeneration()
    {
        return GeneratorConfiguration.SkipDtoTestsGenerating;
    }

    /// <summary>
    ///     Checks if every library is initialised for starting code generation.
    /// </summary>
    /// <exception cref="GeneratorException">
    ///     If any of the libraries are null
    /// </exception>
    protected void Init()
    {
        if (FileManager is null
            || PathManager is null
            || StringManager is null
            || OpenApiToTypeInfoManager is null
            || GeneratorConfiguration is null
            || OpenApiYamlSchema is null)
        {
            throw new GeneratorException(
                "Generator initialization failed. One or some of the supporting libraries are not initialized.");
        }
    }

    protected void GetOriginalBaseNamespaceTokenFromConfiguration(List<TypeInfo> fileInfos)
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.SolutionBaseNamespace)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.SolutionBaseNamespace))
        {
            _logger.LogInformation("No Dto project namespace is provided");
        }

        if (!fileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
        }

        foreach (TypeInfo fileInfo in fileInfos)
        {
            fileInfo.OriginalBaseNamespaceToken = GeneratorConfiguration.SolutionBaseNamespace;
        }
    }

    protected void GetOriginalDtoNamespaceTokenFromConfiguration(List<TypeInfo> fileInfos)
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectNameSpace)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectNameSpace))
        {
            _logger.LogInformation("No Dto project namespace is provided");
            return;
        }

        if (!fileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (TypeInfo fileInfo in fileInfos)
        {
            fileInfo.OriginalDtoNamespaceToken = GeneratorConfiguration.DtoProjectNameSpace;
        }
    }

    protected void GetOriginalTargetPathFromConfiguration(List<TypeInfo> fileInfos)
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("No target directory is provided");
            return;
        }

        if (!fileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (TypeInfo fileInfo in fileInfos)
        {
            fileInfo.OriginalTargetDirectoryToken = GeneratorConfiguration.TargetDirectory;
        }
    }

    protected void GetOriginalDtoProjectBasePathFromConfiguration(List<TypeInfo> fileInfos)
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectBasePath)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectBasePath))
        {
            _logger.LogInformation("No dto project base path is provided");
            return;
        }

        if (!fileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (TypeInfo fileInfo in fileInfos)
        {
            fileInfo.OriginalDtoProjectBasePathToken = GeneratorConfiguration.DtoProjectBasePath;
        }
    }

    protected void GetOriginalDtoProjectAdditionalPathFromConfiguration(List<TypeInfo> fileInfos)
    {
        if (string.IsNullOrEmpty(GeneratorConfiguration.DtoProjectAdditionalPath)
            || string.IsNullOrWhiteSpace(GeneratorConfiguration.DtoProjectAdditionalPath))
        {
            _logger.LogInformation("No dto project additional path is provided");
            return;
        }

        if (!fileInfos.Any())
        {
            _logger.LogInformation("No available DtoFileInfo");
            return;
        }

        foreach (TypeInfo fileInfo in fileInfos)
        {
            fileInfo.OriginalDtoProjectAdditionalPathToken = GeneratorConfiguration.DtoProjectAdditionalPath;
        }
    }

    protected void GetOriginalTypeNameTokenFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetTypeNamesFromOpenApiAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetRequiredPropertiesByTypeAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalPropertyNamesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetPropertyNamesByTypeAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalPropertyTypesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetPropertyTypesByTypeAndAddTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    // protected void GetOriginalPropertyMetadataFromOpenApiSchema(List<TypeInfo> fileInfos)
    // {
    //     if (!fileInfos.Any())
    //     {
    //         _logger.LogInformation("No file info metadata available");
    //         return;
    //     }
    //
    //     if (!OpenApiYamlSchema.Components.Schemas.Any())
    //     {
    //         _logger.LogInformation("No schemas are available in the YAML file");
    //         return;
    //     }
    //
    //     foreach (TypeInfo fileInfo in fileInfos)
    //     {
    //         KeyValuePair<string, OpenApiSchema> openApiSchemaKeyValuePair = OpenApiYamlSchema.Components.Schemas
    //             .First(p => p.Key == fileInfo.OriginalTypeNameToken);
    //
    //         if (!openApiSchemaKeyValuePair.Value.Properties.Any())
    //         {
    //             _logger.LogInformation("Schema name - {SchemaName} - does not have any property",
    //                 fileInfo.OriginalTypeNameToken);
    //             continue;
    //         }
    //
    //         fileInfo.RequiredProperties = openApiSchemaKeyValuePair.Value.Required.ToList();
    //         fileInfo.RequiredProperties = fileInfo.RequiredProperties.ConvertAll(d => d.ToLower());
    //
    //         foreach (KeyValuePair<string, OpenApiSchema> property in openApiSchemaKeyValuePair.Value.Properties)
    //         {
    //             fileInfo.PropertyInfos.Add(new PropertyInfo
    //             {
    //                 OriginalPropertyNameToken = property.Key,
    //                 OriginalPropertyTypeNameToken = property.Value.Type,
    //                 OriginalPropertyTypeFormat = property.Value.Format
    //             });
    //         }
    //     }
    // }
}