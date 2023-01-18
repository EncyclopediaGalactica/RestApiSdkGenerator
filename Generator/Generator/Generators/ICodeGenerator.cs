namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Configuration;
using Managers;
using Microsoft.OpenApi.Models;

public interface ICodeGenerator
{
    ICodeGenerator SetGeneratorConfiguration(CodeGeneratorConfiguration configuration);
    ICodeGenerator SetOpenApiYamlSchema(OpenApiDocument openApiDocument);
    ICodeGenerator SetFileManager(IFileManager fileManager);
    ICodeGenerator SetPathManager(IPathManager pathManager);
    ICodeGenerator SetTemplateManager(ITemplateManager templateManager);
    void Generate();
    void GenerateDtos();
    void GenerateDtosTests();
    ICodeGenerator SetStringManager(IStringManager stringManager);
}