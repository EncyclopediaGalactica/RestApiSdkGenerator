namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

public class Schemas_Should : TestBase
{
    [Fact]
    public void GetTypeNames()
    {
        // Arrange && Act
        List<string> result = Sut.Components.Schemas.GetSchemaNames(TestOpenApiDocument);

        // Assert
        result.Should().BeEquivalentTo(ExpectedTypeNames);
    }

    [Fact]
    public void GetTypeNames_Throw_WhenSchemaDoesNotHaveTypes()
    {
        // Arrange
        TestOpenApiDocument.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        Action action = () => { Sut.Components.Schemas.GetSchemaNames(TestOpenApiDocument); };

        // Assert
        action.Should().Throw<Exception>();
    }
}