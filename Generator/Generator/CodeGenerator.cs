namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

using System.Text;
using FluentValidation;
using HandlebarsDotNet;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json;

public class CodeGenerator
{
    private readonly CodeGeneratorConfiguration _generatorConfiguration;
    private readonly ILogger<CodeGenerator> _logger;
    private readonly OpenApiDocument _openApiDocument;

    private CodeGenerator(
        OpenApiDocument openApiDocument,
        CodeGeneratorConfiguration generatorConfiguration)
    {
        ArgumentNullException.ThrowIfNull(openApiDocument);
        ArgumentNullException.ThrowIfNull(generatorConfiguration);

        _openApiDocument = openApiDocument;
        _generatorConfiguration = generatorConfiguration;
        _logger = new Logger<CodeGenerator>(LoggerFactory.Create(c => c.AddConsole()));
        Generate();
    }

    public List<GeneratedFileInfo> DtoFileInfos { get; } = new List<GeneratedFileInfo>();

    public List<GeneratedFileInfo> DtoTestFileInfos { get; private set; }

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

    private string CollectDtoGeneratingRelatedPaths()
    {
        StringBuilder builder = new StringBuilder();
        if (_generatorConfiguration.TestMode)
        {
            builder.Append(_generatorConfiguration.TargetDirectory);
            return builder.ToString();
        }

        builder.Append(_generatorConfiguration.DtoProjectBasePath);
        if (!string.IsNullOrEmpty(_generatorConfiguration.DtoProjectAdditionalPath))
        {
            builder.Append(_generatorConfiguration.DtoProjectAdditionalPath);
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
        CollectDtoInfo();
        CopyDtoPropertiesToDtoTests();
        if (!_generatorConfiguration.SkipDtoGenerating)
        {
            string dtoTargetPath = CollectDtoGeneratingRelatedPaths();
            GenerateDtos(dtoTargetPath);
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
        PreProcessDteTestNamespace();
    }

    private void PreProcessDteTestNamespace()
    {
        if (!DtoTestFileInfos.Any()) return;

        foreach (GeneratedFileInfo generatedFileInfo in DtoTestFileInfos)
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

        foreach (GeneratedFileInfo generatedFileInfo in DtoTestFileInfos)
        {
            if (!string.IsNullOrEmpty(generatedFileInfo.FileName)
                && !string.IsNullOrWhiteSpace(generatedFileInfo.FileName))
            {
                generatedFileInfo.FileName = PreProcessDtoTestFileName(generatedFileInfo.FileName);
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
    }

    private void CollectDtoInfo()
    {
        foreach (KeyValuePair<string, OpenApiSchema> aSchema in _openApiDocument.Components.Schemas.ToList())
        {
            DtoFileInfos.Add(new GeneratedFileInfo
            {
                FileName = PrepareFilename(aSchema),
                TargetDirectory = PrepareTargetDirectory(),
                Namespace = PrepareNamespace(PrepareDtoNamespace()),
                PropertyInfos = PrepareProperties(aSchema.Value.Properties, aSchema.Value.Required)
            });
        }
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

    private static string PrepareFilename(KeyValuePair<string, OpenApiSchema> aSchema)
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

    private void GenerateDtos(string dtoPath)
    {
        try
        {
            if (DtoFileInfos.Any())
            {
                foreach (GeneratedFileInfo dtoFileInfo in DtoFileInfos)
                {
                    string dtoTemplateString = File.ReadAllText("Templates/dto.handlebars");
                    HandlebarsTemplate<object, object>? template = Handlebars.Compile(dtoTemplateString);
                    string? compiledTemplate = template(dtoFileInfo);
                    if (!Directory.Exists($"{dtoPath}"))
                    {
                        Directory.CreateDirectory($"{dtoPath}");
                    }

                    if (File.Exists($"{dtoPath}/{dtoFileInfo.FileName}.cs"))
                    {
                        File.Delete($"{dtoPath}/{dtoFileInfo.FileName}.cs");
                    }

                    using (FileStream file = File.Open(
                               $"{dtoPath}/{dtoFileInfo.FileName}.cs",
                               FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                    {
                        file.Write(Encoding.ASCII.GetBytes(compiledTemplate));
                    }

                    Console.WriteLine($"Generated file: {dtoPath}{dtoFileInfo.FileName}.cs");
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
        private string _path;

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
            _path = path;
            return this;
        }

        public CodeGenerator? Generate()
        {
            try
            {
                if (string.IsNullOrEmpty(_path))
                {
                    _logger.LogError("Path to configuration file is not defined");
                    return null;
                }

                if (!File.Exists(_path))
                {
                    _logger.LogError("Config file path is invalid: {Path}", _path);
                    return null;
                }

                _logger.LogInformation(
                    "=== Encyclopedia Galactica Rest Api Sdk Generator ==="
                );
                _logger.LogInformation("Generator config file path: {Path}", _path);

                string configFileContent = File.ReadAllText(_path);
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

                _logger.LogInformation("OpenApi yaml file location: {Path}",
                    generatorConfiguration.OpenApiSpecificationPath);

                using FileStream yamlString = new FileStream(
                    generatorConfiguration.OpenApiSpecificationPath,
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
                    $"Inner exception message: {e.Message}",
                    e);
            }
        }
    }
}