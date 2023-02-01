namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator;
using Generator.Generators;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("integer", "int32", "int")]
    [InlineData("integer", "int64", "long")]
    [InlineData("number", "float", "float")]
    [InlineData("number", "double", "double")]
    [InlineData("string", null, "string")]
    [InlineData("string", "", "string")]
    [InlineData("string", " ", "string")]
    [InlineData("string", "byte", "string")]
    [InlineData("string", "binary", "string")]
    [InlineData("string", "date", "string")]
    [InlineData("string", "date-time", "string")]
    [InlineData("boolean", null, "bool")]
    [InlineData("boolean", "", "bool")]
    [InlineData("boolean", " ", "bool")]
    [InlineData(null, " ", "")]
    [InlineData("", " ", "")]
    [InlineData(" ", " ", "")]
    public void ProcessOpenApiTypesToCsharpTypes(
        string typeNameToken,
        string format,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo
                    {
                        OriginalPropertyTypeNameToken = typeNameToken,
                        OriginalPropertyTypeFormat = format
                    }
                }
            }
        };
        CSharpGenerator cSharpGenerator = new CSharpGenerator();

        // Act
        _sut.ProcessOpenApiTypesToCsharpTypes(fileInfos, cSharpGenerator.OpenApiCsharpTypeMap);

        // Assert
        fileInfos[0].PropertyInfos.ToList()[0].PropertyTypeName.Should().Be(expected);
    }

    [Theory]
    [InlineData("integer", null)]
    [InlineData("integer", "")]
    [InlineData("integer", " ")]
    [InlineData("number", null)]
    [InlineData("number", "")]
    [InlineData("number", " ")]
    public void Throw_ProcessOpenApiTypesToCsharpTypes(
        string typeNameToken,
        string format)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo
                    {
                        OriginalPropertyTypeNameToken = typeNameToken,
                        OriginalPropertyTypeFormat = format
                    }
                }
            }
        };
        CSharpGenerator cSharpGenerator = new CSharpGenerator();

        // Act
        Action action = () =>
        {
            _sut.ProcessOpenApiTypesToCsharpTypes(fileInfos, cSharpGenerator.OpenApiCsharpTypeMap);
        };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}