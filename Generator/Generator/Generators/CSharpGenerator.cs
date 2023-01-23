namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Microsoft.Extensions.Logging;
using Models;
using Processors.CSharp;

public class CSharpGenerator : AbstractGenerator
{
    private const string DtoTypeNamePostFix = "Dto";
    private const string DtoFileNamePostFix = "Dto";
    private const string FileType = ".cs";
    private const string DtoTemplate = "Templates/dto.handlebars";

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

    protected override string DtoTemplatePath { get; }

    public override ICodeGenerator Generate()
    {
        if (!ShouldIRunDtoGeneration())
        {
            GenerateDtos();
        }

        if (!ShouldIRunDtoTestGeneration())
        {
            GenerateDtosTests();
        }

        return this;
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
        GetOriginalDtoProjectAdditionalPathFromConfiguration();

        PreProcessDtoMetadata();
        CopyRenderDataToRenderObject();
        Render();
    }

    private void Render()
    {
        if (!_dtoFileInfosRender.Any())
        {
            _logger.LogInformation("No render objects available");
        }

        foreach (FileInfo fileInfo in _dtoFileInfosRender)
        {
            string compiledContent = TemplateManager.CompileTemplate(
                fileInfo.TemplateAbsolutePathWithFileName,
                fileInfo);
            FileManager.DeleteFile(fileInfo.TargetPathWithFileName);
            FileManager.WriteContentIntoFile(compiledContent, fileInfo.TargetPathWithFileName);
        }
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
                    TargetPathWithFileName = fileInfo.TargetPathWithFileName,
                    PropertyInfos = propertyInfos,
                    Namespace = fileInfo.Namespace,
                    TemplateAbsolutePathWithFileName = fileInfo.TemplateAbsolutePathWithFileName
                });
        }
    }

    private void PreProcessDtoMetadata()
    {
        _dtoProcessor.ProcessDtoTypeName(DtoFileInfos, DtoTypeNamePostFix);
        _dtoProcessor.ProcessDtoFileNames(DtoFileInfos, DtoFileNamePostFix, FileType);
        _dtoProcessor.ProcessTargetPath(DtoFileInfos);
        _dtoProcessor.CheckIfPropertyNameIsReservedWord(DtoFileInfos, _reservedWords);
        _dtoProcessor.ProcessPathWithFileName(DtoFileInfos);
        _dtoProcessor.ProcessDtoTemplatePath(DtoFileInfos, DtoTemplatePath);
        _dtoProcessor.ProcessDtoNamespace(DtoFileInfos);
        _dtoProcessor.ProcessPropertyNames(DtoFileInfos);
        _dtoProcessor.ProcessPropertyTypeNames(DtoFileInfos, _reservedWords, _valueTypes);
    }

    public override void GenerateDtosTests()
    {
        throw new NotImplementedException();
    }

    public override ICodeGenerator Initialize()
    {
        _dtoProcessor = new DtoProcessor(FileManager, StringManager, PathManager);
        return this;
    }
}