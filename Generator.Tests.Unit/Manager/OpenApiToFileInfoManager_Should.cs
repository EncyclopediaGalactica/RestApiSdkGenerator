namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager;

using FluentAssertions;
using Generator;
using Generator.Managers;
using Generator.Models;
using Microsoft.OpenApi.Models;
using OpenApiDocumentManager;
using Xunit;

public class OpenApiToFileInfoManager_Should : TestBase
{
    private readonly IOpenApiToFileInfoManager _sut;

    public OpenApiToFileInfoManager_Should()
    {
        _sut = new OpenApiToFileInfoManager();
    }

    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoFileInfo()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();

        // Act
        _sut.GetTypeNamesFromOpenApiAndAddToFileInfo(fileInfos, TestOpenApiDocument);

        // Assert
        foreach (string expectedTypeName in ExpectedTypeNames)
        {
            fileInfos.Where(p => p.OriginalTypeNameToken == expectedTypeName).ToList().Count.Should().Be(1);
        }
    }

    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoFileInfo_ThrowWhenNoTypeNamesInOpenApi()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
        TestOpenApiDocument.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        Action action = () => { _sut.GetTypeNamesFromOpenApiAndAddToFileInfo(fileInfos, TestOpenApiDocument); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoFileInfo_ThrowWhenFileInfoListIsNotEmpty()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo()
        };

        // Act
        Action action = () => { _sut.GetTypeNamesFromOpenApiAndAddToFileInfo(fileInfos, TestOpenApiDocument); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}