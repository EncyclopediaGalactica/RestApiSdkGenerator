namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

using System.Text;
using Configuration;
using FluentValidation;
using Generators;
using HandlebarsDotNet;
using Managers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Models;
using Newtonsoft.Json;

public class CodeGenerator
{
    private readonly string _dtoTestTemplatePath = "Templates/dto_tests.handlebars";
    private readonly IFileManager _fileManager;
    private readonly CodeGeneratorConfiguration _generatorConfiguration;
    private readonly ILogger<CodeGenerator> _logger;
    private readonly OpenApiDocument _openApiDocument;
    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;
    private readonly ITemplateManager _templateManager;
    private List<string> _availableCodeGenerators = new List<string> { "csharp" };
    private ICodeGenerator _codeGenerator;
    private string _dtoGenerationFinalPath;

    private CodeGenerator(
        OpenApiDocument openApiDocument,
        CodeGeneratorConfiguration generatorConfiguration)
    {
        ArgumentNullException.ThrowIfNull(openApiDocument);
        ArgumentNullException.ThrowIfNull(generatorConfiguration);

        _openApiDocument = openApiDocument;
        _generatorConfiguration = generatorConfiguration;

        _logger = new Logger<CodeGenerator>(LoggerFactory.Create(c => c.AddConsole()));
        _fileManager = new FileManagerImpl();
        _pathManager = new PathManagerImpl();
        _templateManager = new TemplateManagerImpl();
        _stringManager = new StringManagerImpl();
        CreateGenerator(generatorConfiguration);
        _codeGenerator!.Generate();
        // Generate();
    }

    public List<FileInfo> DtoFileInfos { get; } = new List<FileInfo>();

    public List<FileInfo> DtoTestFileInfos { get; private set; }

    private void CreateGenerator(CodeGeneratorConfiguration generatorConfiguration)
    {
        if (!_availableCodeGenerators.Contains(generatorConfiguration.Lang.ToLower()))
        {
            throw new GeneratorException($"Language, {generatorConfiguration.Lang}, is not available.");
        }

        switch (generatorConfiguration.Lang)
        {
            case "csharp":
                _codeGenerator = new CSharpGenerator()
                    .SetGeneratorConfiguration(generatorConfiguration)
                    .SetOpenApiYamlSchema(_openApiDocument)
                    .SetFileManager(_fileManager)
                    .SetPathManager(_pathManager)
                    .SetTemplateManager(_templateManager)
                    .SetStringManager(_stringManager);
                break;
        }
    }

    private void Generate()
    {
        DtoPhase();
        RequestModelsPhase();
    }

    private void RequestModelsPhase()
    {
        CollectRequestModelsInfo();
        if (!_generatorConfiguration.SkipRequestModelGenerating)
        {
            GenerateRequestModels();
        }

        if (!_generatorConfiguration.SkipRequestModelTestsGenerating)
        {
            GenerateRequestModelTests();
        }
    }

    private string DetermineDtoGenerationBasePath()
    {
        StringBuilder builder = new StringBuilder();
        if (_generatorConfiguration.TargetDirectory[0].ToString() == "/")
        {
            _logger.LogInformation("Target directory is absolute path. Path: {Path}",
                _generatorConfiguration.TargetDirectory);
            builder.Append(_generatorConfiguration.TargetDirectory);
        }
        else
        {
            _logger.LogInformation("Target directory is relative path. Transform it to absolute");
            builder.Append(_pathManager.GetCurrentDirectory())
                .Append("/")
                .Append(_generatorConfiguration.TargetDirectory);
        }

        if (!string.IsNullOrEmpty(_generatorConfiguration.DtoProjectBasePath)
            || !string.IsNullOrWhiteSpace(_generatorConfiguration.DtoProjectBasePath))
        {
            if (_generatorConfiguration.DtoProjectBasePath[0].ToString() == "/")
            {
                _logger.LogInformation("Dto generation base path is absolute path: {Path}",
                    _generatorConfiguration.DtoProjectBasePath);
                throw new GeneratorException(
                    "Dto project base path must be relative path: " +
                    $"{_generatorConfiguration.DtoProjectBasePath}"
                );
            }

            builder.Append("/").Append(_generatorConfiguration.DtoProjectBasePath);
        }

        if (!string.IsNullOrEmpty(_generatorConfiguration.DtoProjectAdditionalPath)
            || !string.IsNullOrWhiteSpace(_generatorConfiguration.DtoProjectAdditionalPath))
        {
            builder.Append("/")
                .Append(_generatorConfiguration.DtoProjectAdditionalPath);
        }

        return builder.ToString();
    }

    private void GenerateRequestModelTests()
    {
        throw new NotImplementedException();
    }

    private void CollectRequestModelsInfo()
    {
    }

    private void DtoPhase()
    {
        _logger.LogInformation("=== Dto phase started");
        PreProcessDtoDetails();
        CopyDtoPropertiesToDtoTests();
        if (!_generatorConfiguration.SkipDtoGenerating)
        {
            _dtoGenerationFinalPath = DetermineDtoGenerationBasePath();
            GenerateDtos();
        }

        if (!_generatorConfiguration.SkipDtoTestsGenerating)
        {
            PreprocessDtoTestProperties();
            GenerateDtoTests();
        }

        _logger.LogInformation("=== Dto phase finished");
    }

    private void PreprocessDtoTestProperties()
    {
        PreprocessDtoTestFileNames();
        PreProcessDtoTestNamespace();
        PreProcessDtoTestTypenames();
        PreProcessDtoTestTypenamesAsVariableNames();
    }

    private void PreProcessDtoTestTypenamesAsVariableNames()
    {
        if (!DtoTestFileInfos.Any()) return;

        foreach (FileInfo generatedFileInfo in DtoTestFileInfos)
        {
            if (!string.IsNullOrEmpty(generatedFileInfo.OriginalTypename)
                && !string.IsNullOrWhiteSpace(generatedFileInfo.OriginalTypename))
            {
                string variableName = $"{generatedFileInfo.OriginalTypename[0].ToString().ToLower()}" +
                                      $"{generatedFileInfo.OriginalTypename.Substring(
                                          1,
                                          generatedFileInfo.OriginalTypename.Length - 1)}";
                generatedFileInfo.TypenameAsVariableName = variableName;
            }
        }
    }

    private void PreProcessDtoTestTypenames()
    {
        if (!DtoTestFileInfos.Any()) return;

        foreach (FileInfo generatedFileInfo in DtoTestFileInfos)
        {
            if (!string.IsNullOrEmpty(generatedFileInfo.Typename)
                && !string.IsNullOrWhiteSpace(generatedFileInfo.Typename))
            {
                generatedFileInfo.OriginalTypename = generatedFileInfo.Typename;
                generatedFileInfo.Typename = PreProcessDtoTestTypename(generatedFileInfo.Typename);
            }
        }
    }

    private string PreProcessDtoTestTypename(string typename)
    {
        return $"{typename}_Should";
    }

    private void PreProcessDtoTestNamespace()
    {
        if (!DtoTestFileInfos.Any()) return;

        foreach (FileInfo generatedFileInfo in DtoTestFileInfos)
        {
            if (!string.IsNullOrEmpty(generatedFileInfo.Namespace)
                && !string.IsNullOrWhiteSpace(generatedFileInfo.Namespace))
            {
                if (!string.IsNullOrEmpty(_generatorConfiguration.DtoTestProjectNameSpace)
                    && !string.IsNullOrWhiteSpace(_generatorConfiguration.DtoTestProjectNameSpace))
                {
                    string checkedNamespace = PrepareNamespace(
                        $"{_generatorConfiguration.SolutionBaseNamespace}.{_generatorConfiguration.DtoTestProjectNameSpace}"
                    );
                    generatedFileInfo.Namespace = checkedNamespace;
                }
                else
                {
                    _logger.LogInformation(
                        "No Dto test project namespace is provided. Solution base namespace will be used"
                    );
                    string checkedNamespace = PrepareNamespace(_generatorConfiguration.SolutionBaseNamespace);
                    generatedFileInfo.Namespace = checkedNamespace;
                }
            }
        }
    }

    private void PreprocessDtoTestFileNames()
    {
        if (!DtoTestFileInfos.Any()) return;

        foreach (FileInfo generatedFileInfo in DtoTestFileInfos)
        {
            if (!string.IsNullOrEmpty(generatedFileInfo.Filename)
                && !string.IsNullOrWhiteSpace(generatedFileInfo.Filename))
            {
                generatedFileInfo.Filename = PreProcessDtoTestFileName(generatedFileInfo.Filename);
            }
        }
    }

    private string PreProcessDtoTestFileName(string fileName)
    {
        return $"{fileName}_Should";
    }

    /// <summary>
    ///     Copies the collected Dto information to another property.
    ///     This way the two operation are separated.
    /// </summary>
    private void CopyDtoPropertiesToDtoTests()
    {
        DtoTestFileInfos = DtoFileInfos;
    }

    private void GenerateDtoTests()
    {
        DeleteOldFiles();
        CheckAndCreatePath();
        GenerateDtoTestFiles();
    }

    private void GenerateDtoTestFiles()
    {
        _logger.LogInformation("=== Dto test file generation --> start");
        string basePath = _pathManager.GetCurrentDirectory();
        string dotTestTemplatePath = _pathManager.BuildPathString(
            basePath,
            _dtoTestTemplatePath);
        string templateContent = _fileManager.ReadAllText(dotTestTemplatePath);
        foreach (FileInfo generatedFileInfo in DtoFileInfos)
        {
            string compiledContent = _templateManager.CompileTemplate(
                templateContent,
                generatedFileInfo);
            string path = _pathManager.BuildPathString(
                _generatorConfiguration.TargetDirectory,
                _generatorConfiguration.DtoTestProjectBasePath!,
                _generatorConfiguration.DtoTestProjectAdditionalPath!);
            string pathWithFilename = _pathManager.BuildPathStringWithFilename(
                path,
                $"{generatedFileInfo.Filename}.cs");
            _fileManager.WriteContentIntoFile(compiledContent, pathWithFilename);
        }

        _logger.LogInformation("=== Dto test file generation --> end");
    }

    private void CheckAndCreatePath()
    {
        _logger.LogInformation("=== Checking Dto test path --> start");
        string targetDirectory = _pathManager.BuildPathString(
            _generatorConfiguration.TargetDirectory,
            _generatorConfiguration.DtoTestProjectBasePath!,
            _generatorConfiguration.DtoTestProjectAdditionalPath!);
        // _fileManager.CheckIfExistsOrCreate(targetDirectory);
        _logger.LogInformation("=== Checking Dto test path --> end");
    }

    private void DeleteOldFiles()
    {
        _logger.LogInformation("=== Delete old Dto test files --> start");
        foreach (FileInfo dtoTestFileInfo in DtoTestFileInfos)
        {
            string path = _pathManager.BuildPathString(
                _generatorConfiguration.TargetDirectory,
                _generatorConfiguration.DtoTestProjectBasePath!,
                _generatorConfiguration.DtoTestProjectAdditionalPath!);
            string pathWithFilename = _pathManager.BuildPathStringWithFilename(path, dtoTestFileInfo.Filename);
            _fileManager.DeleteFile(pathWithFilename);
        }

        _logger.LogInformation("=== Delete old Dto test files --> end");
    }

    private void PreProcessDtoDetails()
    {
        foreach (KeyValuePair<string, OpenApiSchema> aSchema in _openApiDocument.Components.Schemas.ToList())
        {
            DtoFileInfos.Add(new FileInfo
            {
                OriginalTypeNameToken = PrepareOriginalTypeNameToken(aSchema),
                OriginalTypename = PrepareOriginalTypeName(aSchema),
                Typename = PrepareTypeName(aSchema),
                Filename = PrepareFilename(aSchema),
                TargetDirectory = PrepareTargetDirectory(),
                Namespace = PrepareNamespace(PrepareDtoNamespace()),
                PropertyInfos = PrepareProperties(aSchema.Value.Properties, aSchema.Value.Required)
            });
        }
    }

    private string PrepareOriginalTypeNameToken(KeyValuePair<string, OpenApiSchema> aSchema)
    {
        return aSchema.Key;
    }

    private string PrepareOriginalTypeName(KeyValuePair<string, OpenApiSchema> aSchema)
    {
        return $"{aSchema.Key}Dto";
    }

    private string PrepareTypeName(KeyValuePair<string, OpenApiSchema> aSchema)
    {
        return aSchema.Key.First().ToString().ToUpper() + aSchema.Key.Substring(1) + "Dto";
    }

    private string? PrepareTargetDirectory()
    {
        if (string.IsNullOrEmpty(_generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(_generatorConfiguration.TargetDirectory))
        {
            return null;
        }

        if (_generatorConfiguration.TargetDirectory[^1].ToString() == "/")
        {
            return _generatorConfiguration.TargetDirectory;
        }

        return _generatorConfiguration.TargetDirectory + "/";
    }

    private string PrepareDtoNamespace()
    {
        StringBuilder builder = new(_generatorConfiguration.SolutionBaseNamespace);
        if (string.IsNullOrEmpty(_generatorConfiguration.DtoProjectNameSpace)
            && string.IsNullOrWhiteSpace(_generatorConfiguration.DtoProjectNameSpace))
        {
            _logger.LogInformation(
                "No namespace is provided for Dto project. Solution base namespace will be used"
            );
        }
        else
        {
            builder.Append(".").Append(_generatorConfiguration.DtoProjectNameSpace);
        }

        return builder.ToString();
    }

    private string PrepareNamespace(string? namespaceString)
    {
        if (string.IsNullOrEmpty(namespaceString))
        {
            return String.Empty;
        }

        namespaceString = CheckIfNamespaceLastCharIsDotAndRemoveIt(namespaceString);

        StringBuilder builder = new StringBuilder();
        bool isDot = false;
        for (int i = 0; i < namespaceString.Length; i++)
        {
            if (isDot)
            {
                builder.Append(namespaceString[i].ToString().ToUpper());
                isDot = false;
                continue;
            }

            if (namespaceString[i].ToString() == ".")
            {
                isDot = true;
                builder.Append(namespaceString[i].ToString());
                continue;
            }

            if (i == 0)
            {
                builder.Append(namespaceString[i].ToString().ToUpper());
                continue;
            }

            builder.Append(namespaceString[i]);
        }

        return builder.ToString();
    }

    private string CheckIfNamespaceLastCharIsDotAndRemoveIt(string namespaceString)
    {
        if (namespaceString.Last().ToString() == ".")
        {
            return namespaceString.Substring(0, namespaceString.Length - 1);
        }

        return namespaceString;
    }

    private string PrepareFilename(KeyValuePair<string, OpenApiSchema> aSchema)
    {
        return aSchema.Key.First().ToString().ToUpper() + aSchema.Key.Substring(1) + "Dto";
    }

    private ICollection<PropertyInfo> PrepareProperties(
        IDictionary<string, OpenApiSchema> valueProperties,
        ISet<string> requiredProperties)
    {
        ICollection<PropertyInfo> propertyInfos = new List<PropertyInfo>();

        foreach (KeyValuePair<string, OpenApiSchema> valueProperty in valueProperties)
        {
            propertyInfos.Add(new PropertyInfo
            {
                PropertyName = PreparePropertyName(valueProperty),
                PropertyTypeName = PreparePropertyTypeName(valueProperty.Value.Type, valueProperty.Value.Format),
                IsNullable = DeterminePropertyNullability(valueProperty.Key, requiredProperties)
            });
        }

        return propertyInfos;
    }

    /// <summary>
    ///     If a property is marked as required it is not nullable.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="requiredProperties">List of required properties</param>
    /// <returns>Return false if the property is in the list of required properties.</returns>
    private bool DeterminePropertyNullability(string propertyName, ISet<string> requiredProperties)
    {
        return !requiredProperties.Contains(propertyName);
    }

    private string PreparePropertyTypeName(string valueType, string valueFormat)
    {
        string typeName = String.Empty;
        switch (valueType)
        {
            case "integer":
                if (!string.IsNullOrEmpty(valueFormat))
                {
                    switch (valueFormat)
                    {
                        case "int64":
                            typeName = "long";
                            break;

                        case "int32":
                            typeName = "int";
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Value format is not defined for integer type! " +
                                      $"{nameof(valueType)}={valueType};" +
                                      $"{nameof(valueFormat)}={valueFormat};");

                    typeName = "int";
                }

                break;

            case "string":
                typeName = "string";
                break;

            case "number":

                if (!string.IsNullOrEmpty(valueFormat))
                {
                    switch (valueFormat)
                    {
                        case "float":
                            typeName = "float";
                            break;

                        case "double":
                            typeName = "double";
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Value format is not defined for number type! " +
                                      $"{nameof(valueType)}={valueType};" +
                                      $"{nameof(valueFormat)}={valueFormat};");

                    typeName = "double";
                }

                break;

            default:
                Console.WriteLine($"No such type! details: " +
                                  $"{nameof(valueType)}={valueType};" +
                                  $"{nameof(valueFormat)}={valueFormat}");
                break;
        }

        return typeName;
    }

    private string PreparePropertyName(KeyValuePair<string, OpenApiSchema> valueProperty)
    {
        string result = string.Empty;
        if (valueProperty.Key.Contains("_"))
        {
            result = KebabCaseToTitleCase(valueProperty.Key);
        }
        else
        {
            result = valueProperty.Key.First().ToString().ToUpper() + valueProperty.Key.Substring(1);
        }

        return result;
    }

    private string KebabCaseToTitleCase(string propertyNameFromOpenapiSpec)
    {
        StringBuilder builder = new StringBuilder();
        bool makeItUpper = false;
        for (int i = 0; i < propertyNameFromOpenapiSpec.Length; i++)
        {
            if (makeItUpper)
            {
                builder.Append(propertyNameFromOpenapiSpec[i].ToString().ToUpper());
                makeItUpper = false;
                continue;
            }

            if (propertyNameFromOpenapiSpec[i] == '_')
            {
                makeItUpper = true;
                continue;
            }

            if (i == 0)
            {
                builder.Append(propertyNameFromOpenapiSpec[i].ToString().ToUpper());
                continue;
            }

            builder.Append(propertyNameFromOpenapiSpec[i]);
        }

        return builder.ToString();
    }

    private void GenerateDtos()
    {
        try
        {
            if (DtoFileInfos.Any())
            {
                foreach (FileInfo dtoFileInfo in DtoFileInfos)
                {
                    string dtoTemplateString = _fileManager.ReadAllText("Templates/dto.handlebars");
                    string compiledTemplate = _templateManager.CompileTemplate(dtoTemplateString, dtoFileInfo);
                    string pathWithFilename = _pathManager.BuildPathStringWithFilename(
                        _dtoGenerationFinalPath, dtoFileInfo.Filename);
                    _fileManager.WriteContentIntoFile(compiledTemplate, $"{pathWithFilename}.cs");
                    _logger.LogInformation("Dto file is generated. Path: {Path}", $"{pathWithFilename}.cs");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Error happened while compiling template and writing generated file. Details: {Details}",
                e.Message
            );
            throw;
        }
    }

    private void GenerateRequestModels()
    {
        string tmplString = File.ReadAllText("Templates/test.handlebars");
        HandlebarsTemplate<object, object>? tmpl = Handlebars.Compile(tmplString);

        var data = new
        {
            Example1 = "example1 template data",
            Example2 = "example1 template data2",
        };

        var result = tmpl(data);
        Console.WriteLine(result);
        File.WriteAllText("Templates/test.cs", result);
    }

    public class Builder
    {
        private readonly ILogger<CodeGenerator.Builder> _logger;
        private string _actualPath = Directory.GetCurrentDirectory();
        private string _generatorConfigurationPath;


        public Builder()
        {
            _logger = new Logger<Builder>(LoggerFactory.Create(c => c.AddConsole()));
        }

        /// <summary>
        ///     Set path to configuration file.
        /// </summary>
        /// <param name="path">The path to configuration file</param>
        /// <returns>Returns a Builder</returns>
        public Builder SetPath(string path)
        {
            _generatorConfigurationPath = path;
            return this;
        }

        public CodeGenerator? Generate()
        {
            try
            {
                if (string.IsNullOrEmpty(_generatorConfigurationPath))
                {
                    _logger.LogError("Path to configuration file is not defined");
                    return null;
                }

                if (!File.Exists(_generatorConfigurationPath))
                {
                    _logger.LogError("Config file path is invalid: {Path}", _generatorConfigurationPath);
                    return null;
                }

                _logger.LogInformation(
                    "=== Encyclopedia Galactica Rest Api Sdk Generator ==="
                );
                _logger.LogInformation("Generator config file path: {Path}", _generatorConfigurationPath);

                string configFileContent = File.ReadAllText(_generatorConfigurationPath);
                CodeGeneratorConfiguration generatorConfiguration =
                    JsonConvert.DeserializeObject<CodeGeneratorConfiguration>(configFileContent);

                if (generatorConfiguration is null)
                {
                    string msg = "Configuration file deserialization is unsuccessful.";
                    _logger.LogError(msg);
                    throw new GeneratorException(msg);
                }

                _logger.LogInformation("Generator configuration has been parsed");

                CodeGeneratorConfigurationValidator configFileValidator = new CodeGeneratorConfigurationValidator();
                configFileValidator.Validate(generatorConfiguration, o => { o.ThrowOnFailures(); });

                string yamlFileFullPath = $"{_actualPath}/{generatorConfiguration.OpenApiSpecificationPath}";
                _logger.LogInformation("OpenApi yaml file location: {Path}", yamlFileFullPath);

                using FileStream yamlString = new FileStream(
                    yamlFileFullPath,
                    FileMode.Open);
                OpenApiDocument? openApiSpecification = new OpenApiStreamReader().Read(
                    yamlString,
                    out OpenApiDiagnostic? openApiDiagnostic);
                return new CodeGenerator(openApiSpecification, generatorConfiguration);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Unsuccessful code generation. For further information see inner exception. " +
                    "Inner exception message: {Message} ", e.Message
                );
                throw new GeneratorException(
                    "Unsuccessful code generation. For further information see inner exception. " +
                    $"Inner exception message: {e.Message} " +
                    $"Stack trace: {e.StackTrace}",
                    e);
            }
        }
    }
}