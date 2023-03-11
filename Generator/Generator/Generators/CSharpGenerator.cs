namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Microsoft.Extensions.Logging;
using Models;
using Processors.CSharp;

public class CSharpGenerator : AbstractGenerator
{
    private const string DtoTypeNamePostFix = "Dto";
    private const string DtoFileNamePostFix = "Dto";
    private const string FileType = ".cs";
    private readonly Logger<CSharpGenerator> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

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

    private ICSharpProcessor _cSharpProcessor;

    private List<TypeInfoRender> _dtoFileInfosRender = new List<TypeInfoRender>();
    public override string DtoTemplatePath { get; } = "Templates/dto.handlebars";

    public Dictionary<string, string> OpenApiCsharpTypeMap { get; } = new()
    {
        { "integer-int32", "int" },
        { "integer-int64", "long" },
        { "number-float", "float" },
        { "number-double", "double" },
        { "string", "string" },
        { "string-byte", "string" },
        { "string-binary", "string" },
        { "string-date", "string" },
        { "string-date-time", "string" },
        { "boolean", "bool" },
    };

    public override ICodeGenerator Generate()
    {
        if (!ShouldIRunDtoPreProcessing())
        {
            PreProcessDtos();
        }

        if (!ShouldIRunDtoGeneration())
        {
            GenerateDtos();
        }

        if (!ShouldIRunDtoTestPreProcessing())
        {
            PreProcessDtoTest();
        }

        if (!ShouldIRunDtoTestGeneration())
        {
            GenerateDtosTests();
        }

        return this;
    }

    private void PreProcessDtoTest()
    {
        // GetOriginalTypeNameTokenFromOpenApiSchema(DtoTestTypeInfos);
        // GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(DtoTypeInfos);
        // GetOriginalPropertyNamesByTypeFromOpenApiSchema(DtoTypeInfos);
        // GetOriginalPropertyTypesByTypeFromOpenApiSchema(DtoTypeInfos);
        //
        // // GetOriginalPropertyMetadataFromOpenApiSchema(DtoTestTypeInfos);
        // GetOriginalBaseNamespaceTokenFromConfiguration(DtoTestTypeInfos);
        // GetOriginalDtoNamespaceTokenFromConfiguration(DtoTestTypeInfos);
        //
        // GetOriginalTargetPathFromConfiguration(DtoTestTypeInfos);
        // GetOriginalDtoProjectBasePathFromConfiguration(DtoTestTypeInfos);
        // GetOriginalDtoProjectAdditionalPathFromConfiguration(DtoTestTypeInfos);
    }

    public override void PreProcessDtos()
    {
        GetOriginalTypeNameTokenFromOpenApiSchema(DtoTypeInfos);
        GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(DtoTypeInfos);
        GetOriginalPropertyNamesByTypeFromOpenApiSchema(DtoTypeInfos);
        GetOriginalPropertyTypesByTypeFromOpenApiSchema(DtoTypeInfos);
        MarkVariablesAsPropertiesFromOpenApiSchema(DtoTypeInfos);

        // GetOriginalPropertyMetadataFromOpenApiSchema(DtoTypeInfos);
        GetOriginalBaseNamespaceTokenFromConfiguration(DtoTypeInfos);
        GetOriginalDtoNamespaceTokenFromConfiguration(DtoTypeInfos);

        GetOriginalTargetPathFromConfiguration(DtoTypeInfos);
        GetOriginalDtoProjectBasePathFromConfiguration(DtoTypeInfos);
        GetOriginalDtoProjectAdditionalPathFromConfiguration(DtoTypeInfos);

        PreProcessDtoMetadata();
        CopyRenderDataToRenderObject();
    }

    private void GenerateDtos()
    {
        Render();
    }

    private void Render()
    {
        if (!_dtoFileInfosRender.Any() || !DtoTypeInfos.Any())
        {
            _logger.LogInformation("No render or preprocessed objects are available");
            return;
        }

        foreach (TypeInfo fileInfo in DtoTypeInfos)
        {
            string template = FileManager.ReadAllText(fileInfo.TemplateAbsolutePathWithFileName);
            TypeInfoRender singleRender = _dtoFileInfosRender
                .Where(p => p.Namespace == fileInfo.Namespace)
                .First(p => p.TypeName == fileInfo.Typename);
            string compiledContent = TemplateManager.CompileTemplate(template, singleRender);
            FileManager.DeleteFile(fileInfo.TargetPathWithFileName);
            FileManager.WriteContentIntoFile(compiledContent, fileInfo.TargetPathWithFileName);
        }
    }

    private void CopyRenderDataToRenderObject()
    {
        if (!DtoTypeInfos.Any())
        {
            _logger.LogInformation("No available Dto file info");
            return;
        }

        foreach (TypeInfo fileInfo in DtoTypeInfos)
        {
            List<PropertyInfoRender> propertyInfos = new List<PropertyInfoRender>();
            if (fileInfo.VariableInfos.Any())
            {
                foreach (VariableInfo propertyInfo in fileInfo.VariableInfos)
                {
                    propertyInfos.Add(new PropertyInfoRender
                    {
                        PropertyName = propertyInfo.VariableName,
                        PropertyTypeName = propertyInfo.VariableTypeName,
                        IsNullable = propertyInfo.IsNullable
                    });
                }
            }

            _dtoFileInfosRender.Add(
                new TypeInfoRender()
                {
                    PropertyInfos = propertyInfos,
                    Namespace = fileInfo.Namespace,
                    TypeName = fileInfo.Typename
                });
        }
    }

    private void PreProcessDtoMetadata()
    {
        _cSharpProcessor.ReservedWordCheckForOriginalTypeNames(DtoTypeInfos, _reservedWords);
        _cSharpProcessor.ProcessTypeName(DtoTypeInfos, DtoTypeNamePostFix);
        _cSharpProcessor.ProcessFileName(DtoTypeInfos, DtoFileNamePostFix, FileType);
        _cSharpProcessor.ProcessTargetPath(DtoTypeInfos);
        _cSharpProcessor.ProcessPathWithFileName(DtoTypeInfos);
        _cSharpProcessor.ProcessTemplatePath(DtoTypeInfos, DtoTemplatePath);

        _cSharpProcessor.ReservedWordCheckForOriginalBaseNamespaceToken(DtoTypeInfos, _reservedWords);
        _cSharpProcessor.ReservedWordCheckForOriginalDtoNamespaceToken(DtoTypeInfos, _reservedWords);
        _cSharpProcessor.ProcessNamespace(DtoTypeInfos);

        _cSharpProcessor.ReservedWordsCheckForOriginalVariableNamesOfAType(DtoTypeInfos, _reservedWords);
        _cSharpProcessor.ProcessPropertiesByType(DtoTypeInfos);
        _cSharpProcessor.ReservedWordCheckForVariableNames(DtoTypeInfos, _reservedWords);

        _cSharpProcessor.ProcessNullableVariableTypes(DtoTypeInfos);
        _cSharpProcessor.ProcessOpenApiTypesToCsharpTypes(DtoTypeInfos, OpenApiCsharpTypeMap);
    }

    private void GenerateDtosTests()
    {
        throw new NotImplementedException();
    }

    public override ICodeGenerator Build()
    {
        Init();
        _cSharpProcessor = new CSharpProcessor(FileManager, StringManager, PathManager);
        return this;
    }
}