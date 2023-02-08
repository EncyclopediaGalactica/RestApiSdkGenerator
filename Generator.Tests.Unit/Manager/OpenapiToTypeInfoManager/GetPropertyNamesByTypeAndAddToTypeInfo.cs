namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalPropertyNamesByTypeFromOpenApiSchemaData = new List<object[]>
    {
        new[] { new List<TypeInfo>(), new List<TypeInfo>() },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty"
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    RequiredProperties = new List<string>(),
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" },
                        new PropertyInfo { OriginalPropertyNameToken = "name" },
                    }
                },
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" },
                        new PropertyInfo { OriginalPropertyNameToken = "name" },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo { OriginalPropertyNameToken = "id" },
                        new PropertyInfo { OriginalPropertyNameToken = "name" },
                    }
                },
            }
        }
    };

    [Theory]
    [MemberData(nameof(GetOriginalPropertyNamesByTypeFromOpenApiSchemaData))]
    public void GetOriginalPropertyNamesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos, List<TypeInfo> expected)
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> schemaNames) testData =
            SchemasTestData.GetPropertyNamesBySchemaTestData();

        // Act
        _sut.GetPropertyNamesByTypeAndAddToTypeInfo(typeInfos, testData.openApi);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }
}