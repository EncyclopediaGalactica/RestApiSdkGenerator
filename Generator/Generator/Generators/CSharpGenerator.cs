namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Microsoft.Extensions.Logging;
using Models;
using Processors.CSharp;

public class CSharpGenerator : AbstractGenerator
{
    protected const string DtoTypeNamePostFix = "Dto";
    protected const string DtoFileNamePostFix = "Dto";
    protected const string FileType = ".cs";

    private readonly List<string> _reservedWords = new List<string>
    {
        "abstract",
        "as",
        "base",
        "bool",
        "break",
        "byte",
        "case",
        "catch",
        "char",
        "checked",
        "class",
        "const",
        "continue",
        "decimal",
        "default",
        "delegate",
        "do",
        "double",
        "else",
        "enum",
        "event",
        "explicit",
        "extern",
        "false",
        "finally",
        "fixed",
        "float",
        "for",
        "foreach",
        "goto",
        "if",
        "implicit",
        "in",
        "int",
        "interface",
        "internal",
        "is",
        "lock",
        "long",
        "namespace",
        "new",
        "null",
        "object",
        "operator",
        "out",
        "override",
        "params",
        "private",
        "protected",
        "public",
        "readonly",
        "ref",
        "return",
        "sbyte",
        "sealed",
        "short",
        "sizeof",
        "stackalloc",
        "static",
        "string",
        "struct",
        "switch",
        "this",
        "throw",
        "true",
        "try",
        "typeof",
        "uint",
        "ulong",
        "unchecked",
        "unsafe",
        "ushort",
        "using",
        "virtual",
        "void",
        "volatile",
        "while"
    };

    private readonly List<string> _valueTypes = new()
    {
        "int", "long", "boolean", "float", "double", "string"
    };

    private List<FileInfo> _dtoFileInfosRender = new List<FileInfo>();

    private IDtoProcessor _dtoProcessor;
    private Logger<CSharpGenerator> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

    public CSharpGenerator()
    {
        _dtoProcessor = new DtoProcessor(
            FileManager,
            StringManager);
    }

    protected override string DtoTemplatePath { get; }

    public override void Generate()
    {
        if (ShouldIRunDtoGeneration()) GenerateDtos();
        if (ShouldIRunDtoTestGeneration()) GenerateDtosTests();
    }

    public override void GenerateDtos()
    {
        GetOriginalTargetLocationFromConfiguration();
        GetOriginalDtoProjectBasePathFromConfiguration();
        GetOriginalDtoProjectAdditionalPathFromConfiguration();

        GetOriginalTypeNameTokenFromOpenApiSchema();
        GetOriginalPropertyMetadataFromOpenApiSchema();
        GetOriginalBaseNamespaceTokenFromConfiguration();
        GetOriginalDtoNamespaceTokenFromConfiguration();
        PreProcessDtoMetadata();
        CopyRenderDataToRenderObject();
    }

    private void CopyRenderDataToRenderObject()
    {
        if (!DtoFileInfos.Any())
        {
            _logger.LogInformation("No available Dto file info");
            return;
        }

        foreach (FileInfo fileInfo in DtoFileInfos)
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            if (fileInfo.PropertyInfos.Any())
            {
                foreach (PropertyInfo propertyInfo in fileInfo.PropertyInfos)
                {
                    propertyInfos.Add(new PropertyInfo
                    {
                        PropertyName = propertyInfo.PropertyName,
                        PropertyTypeName = propertyInfo.PropertyTypeName,
                        IsNullable = propertyInfo.IsNullable
                    });
                }
            }

            _dtoFileInfosRender.Add(
                new FileInfo
                {
                    Filename = fileInfo.Filename,
                    PropertyInfos = propertyInfos
                });
        }
    }

    private void PreProcessDtoMetadata()
    {
        _dtoProcessor.ProcessDtoTypeName(DtoFileInfos, DtoTypeNamePostFix);
        _dtoProcessor.ProcessDtoFileNames(DtoFileInfos, DtoFileNamePostFix, FileType);
        _dtoProcessor.ProcessDtoNamespace(DtoFileInfos);
        _dtoProcessor.ProcessPropertyNames(DtoFileInfos, _reservedWords);
        _dtoProcessor.ProcessPropertyTypeNames(DtoFileInfos, _reservedWords, _valueTypes);
    }

    public override void GenerateDtosTests()
    {
        throw new NotImplementedException();
    }
}