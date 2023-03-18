namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Microsoft.Extensions.Logging;
using Models;
using Processors.CSharp;

public class CSharpGenerator : AbstractGenerator
{
    private const string DtoTypeNamePostFix = "Dto";
    private const string DtoFileNamePostFix = "Dto";
    private const string DtoTestTypeNamePostfix = "Dto_Should";
    private const string DtoTestFileNamePostfix = "Dto_Should";
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

    /// <summary>
    ///     A list where the types (c-sharp classes) are collected in order to prevent type collision
    ///     during code generation
    /// </summary>
    private readonly List<string> _typesInGenerationScope = new List<string>();

    private readonly List<string> _valueTypes = new()
    {
        "int", "long", "boolean", "float", "double", "string"
    };

    private ICSharpProcessor _cSharpProcessor;

    private List<DtoTypeInfoRender> _dtoFileInfosRender = new List<DtoTypeInfoRender>();

    public override string DtoTemplatePath { get; } = "Templates/dto.handlebars";
    public override string DtoTestTemplatePath { get; } = "Templates/dto_tests.handlebars";

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
            GenerateDtoTests();
        }

        return this;
    }

    private void PreProcessDtoTest()
    {
        GetOriginalTypeNameTokenFromOpenApiSchema(DtoTestTypeInfos);
        GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(DtoTestTypeInfos);
        GetOriginalPropertyNamesByTypeFromOpenApiSchema(DtoTestTypeInfos);
        GetOriginalPropertyTypesByTypeFromOpenApiSchema(DtoTestTypeInfos);
        MarkVariablesAsPropertiesFromOpenApiSchema(DtoTestTypeInfos);

        GetOriginalBaseNamespaceTokenFromConfiguration(DtoTestTypeInfos);
        GetOriginalDtoNamespaceTokenFromConfiguration(DtoTestTypeInfos);

        GetOriginalTargetPathFromConfiguration(DtoTestTypeInfos);
        GetOriginalDtoTestProjectBasePathFromConfiguration(DtoTestTypeInfos);
        GetOriginalDtoTestProjectAdditionalPathFromConfiguration(DtoTestTypeInfos);

        PreProcessDtoTestMetadata();
        // CopyDtoRenderDataToDtoRenderObject(DtoTypeInfos, _dtoFileInfosRender);
    }

    public override void PreProcessDtos()
    {
        GetOriginalTypeNameTokenFromOpenApiSchema(DtoTypeInfos);
        GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(DtoTypeInfos);
        GetOriginalPropertyNamesByTypeFromOpenApiSchema(DtoTypeInfos);
        GetOriginalPropertyTypesByTypeFromOpenApiSchema(DtoTypeInfos);
        MarkVariablesAsPropertiesFromOpenApiSchema(DtoTypeInfos);

        GetOriginalBaseNamespaceTokenFromConfiguration(DtoTypeInfos);
        GetOriginalDtoNamespaceTokenFromConfiguration(DtoTypeInfos);

        GetOriginalTargetPathFromConfiguration(DtoTypeInfos);
        GetOriginalDtoProjectBasePathFromConfiguration(DtoTypeInfos);
        GetOriginalDtoProjectAdditionalPathFromConfiguration(DtoTypeInfos);

        PreProcessDtoMetadata();
        CopyDtoRenderDataToDtoRenderObject(DtoTypeInfos, _dtoFileInfosRender);
    }

    private void GenerateDtos()
    {
        RenderDtos();
    }

    private void GenerateDtoTests()
    {
        RenderDtoTests();
    }

    private void RenderDtoTests()
    {
        throw new NotImplementedException();
    }

    private void RenderDtos()
    {
        if (!_dtoFileInfosRender.Any() || !DtoTypeInfos.Any())
        {
            _logger.LogInformation("No render or preprocessed objects are available");
            return;
        }

        foreach (TypeInfo fileInfo in DtoTypeInfos)
        {
            string template = FileManager.ReadAllText(fileInfo.TemplateAbsolutePathWithFileName);
            DtoTypeInfoRender singleRender = _dtoFileInfosRender
                .Where(p => p.Namespace == fileInfo.Namespace)
                .First(p => p.TypeName == fileInfo.TypeName);
            string compiledContent = TemplateManager.CompileTemplate(template, singleRender);
            FileManager.DeleteFile(fileInfo.TargetPathWithFileName);
            FileManager.WriteContentIntoFile(compiledContent, fileInfo.TargetPathWithFileName);
        }
    }

    /// <summary>
    ///     Copies all the data needed for Dto render to a separate render object
    /// </summary>
    /// <param name="dtoTypeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="dtoRenderTypeInfos">List of <see cref="DtoTypeInfoRender" /></param>
    private void CopyDtoRenderDataToDtoRenderObject(
        List<TypeInfo> dtoTypeInfos,
        List<DtoTypeInfoRender> dtoRenderTypeInfos)
    {
        if (!dtoTypeInfos.Any())
        {
            _logger.LogInformation("No available Dto file info");
            return;
        }

        foreach (TypeInfo typeInfo in dtoTypeInfos)
        {
            List<PropertyInfoRender> propertyInfos = new List<PropertyInfoRender>();
            if (typeInfo.VariableInfos.Any())
            {
                foreach (VariableInfo propertyInfo in typeInfo.VariableInfos)
                {
                    propertyInfos.Add(new PropertyInfoRender
                    {
                        PropertyName = propertyInfo.VariableName,
                        PropertyTypeName = propertyInfo.VariableTypeName,
                        IsNullable = propertyInfo.IsNullable
                    });
                }
            }

            dtoRenderTypeInfos.Add(
                new DtoTypeInfoRender()
                {
                    PropertyInfos = propertyInfos,
                    Namespace = typeInfo.Namespace,
                    TypeName = typeInfo.TypeName
                });
        }
    }

    private void PreProcessDtoMetadata()
    {
        _cSharpProcessor.ReservedWordCheckForOriginalTypeNames(DtoTypeInfos, _reservedWords);
        _cSharpProcessor.ProcessTypeName(DtoTypeInfos, DtoTypeNamePostFix);
        _cSharpProcessor.TypeCheckInGenerationScope(DtoTypeInfos, _typesInGenerationScope);
        _cSharpProcessor.AddTypeNamesToGenerationScope(DtoTypeInfos, _typesInGenerationScope);
        _cSharpProcessor.ProcessFileName(DtoTypeInfos, DtoFileNamePostFix, FileType);
        _cSharpProcessor.ProcessDtoTargetPath(DtoTypeInfos);
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

    private void PreProcessDtoTestMetadata()
    {
        _cSharpProcessor.ReservedWordCheckForOriginalTypeNames(DtoTestTypeInfos, _reservedWords);
        _cSharpProcessor.ProcessTestTypeName(DtoTestTypeInfos, DtoTestTypeNamePostfix);
        _cSharpProcessor.TypeCheckInGenerationScope(DtoTestTypeInfos, _typesInGenerationScope);
        _cSharpProcessor.AddTypeNamesToGenerationScope(DtoTestTypeInfos, _typesInGenerationScope);
        _cSharpProcessor.ProcessFileName(DtoTestTypeInfos, DtoTestFileNamePostfix, FileType);
        _cSharpProcessor.ProcessDtoTestsTargetPath(DtoTestTypeInfos);
        _cSharpProcessor.ProcessPathWithFileName(DtoTestTypeInfos);
        _cSharpProcessor.ProcessTemplatePath(DtoTestTypeInfos, DtoTestTemplatePath);
    }

    public override ICodeGenerator Build()
    {
        Init();
        _cSharpProcessor = new CSharpProcessor(FileManager, StringManager, PathManager);
        return this;
    }
}