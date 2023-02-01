namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager;

using Generator.Managers.OpenApiDocumentManager;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

public class TestBase
{
    protected readonly List<string> ExpectedTypeNames = new List<string> { "pet", "newPet", "errorModel" };
    protected readonly OpenApiDocumentManager Sut;
    protected readonly OpenApiDocument TestOpenApiDocument;

    public TestBase()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/OpenApiDocumentManager/petstore.yaml",
            FileMode.Open);
        TestOpenApiDocument = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (TestOpenApiDocument is null)
        {
            throw new Exception("OpenApi document is null");
        }

        Sut = new OpenApiDocumentManager();
    }
}