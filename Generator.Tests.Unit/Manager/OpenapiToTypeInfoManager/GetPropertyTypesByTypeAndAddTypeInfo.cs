namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetPropertyTypesByTypeAndAddTypeInfoData = new List<object[]>
    {
        new[]
        {
            new List<TypeInfo>(),
            new List<TypeInfo>()
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
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
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                }
            },
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
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        }
                    }
                }
            },
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
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "threeProperties" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threeProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "desc",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "threeProperties" },
                new TypeInfo { OriginalTypeNameToken = "threePropertiesWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threeProperties",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "desc",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threePropertiesWithAllRequired",
                    PropertyInfos = new List<PropertyInfo>
                    {
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "id",
                            OriginalPropertyTypeNameToken = "integer",
                            OriginalPropertyTypeFormat = "int64"
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "name",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                        new PropertyInfo
                        {
                            OriginalPropertyNameToken = "desc",
                            OriginalPropertyTypeNameToken = "string",
                            OriginalPropertyTypeFormat = ""
                        },
                    }
                }
            },
        },
    };

    [Theory]
    [MemberData(nameof(GetPropertyTypesByTypeAndAddTypeInfoData))]
    public void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, List<TypeInfo> expected)
    {
        // Arrange
        (OpenApiDocument openApi,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
            testData = SchemasTestData.GetPropertyTypesBySchemaTestData();
        _sut.GetPropertyNamesByTypeAndAddToTypeInfo(typeInfos, testData.openApi);

        // Act
        _sut.GetPropertyTypesByTypeAndAddTypeInfo(typeInfos, testData.openApi);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }
}