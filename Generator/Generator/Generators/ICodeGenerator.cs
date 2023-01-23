namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Configuration;
using Managers;
using Microsoft.OpenApi.Models;
using Models;

public interface ICodeGenerator
{
    public List<FileInfo> DtoFileInfos { get; }

    public List<FileInfo> DtoTestFileInfos { get; }
    ICodeGenerator SetGeneratorConfiguration(CodeGeneratorConfiguration configuration);
    ICodeGenerator SetOpenApiYamlSchema(OpenApiDocument openApiDocument);
    ICodeGenerator SetFileManager(IFileManager fileManager);
    ICodeGenerator SetPathManager(IPathManager pathManager);
    ICodeGenerator SetTemplateManager(ITemplateManager templateManager);
    ICodeGenerator Generate();
    void GenerateDtos();
    void GenerateDtosTests();
    ICodeGenerator SetStringManager(IStringManager stringManager);
    ICodeGenerator Initialize();
}