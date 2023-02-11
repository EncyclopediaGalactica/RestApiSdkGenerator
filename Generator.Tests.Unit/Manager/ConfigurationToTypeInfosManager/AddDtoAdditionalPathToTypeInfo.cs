namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> AddDtoAdditionalPathToTypeInfoData = new List<object[]>
    {
        new[]
        {
            new CodeGeneratorConfiguration()
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoProjectAdditionalPath = ""
            }
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoProjectAdditionalPath = " "
            }
        }
    };

    [Theory]
    [MemberData(nameof(AddDtoAdditionalPathToTypeInfoData))]
    public void AddDtoAdditionalPathToTypeInfo(CodeGeneratorConfiguration configuration)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        Sut.AddDtoAdditionalPathToTypeInfo(typeInfos, configuration);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void AddDtoAdditionalPathToTypeInfo_WhenConfigIsNull()
    {
        // Arrange && Act
        Action action = () => { Sut.AddDtoAdditionalPathToTypeInfo(null!, new CodeGeneratorConfiguration()); };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void AddDtoAdditionalPathToTypeInfo_TypeInfoIsEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        Sut.AddDtoAdditionalPathToTypeInfo(typeInfos,
            new CodeGeneratorConfiguration { DtoProjectAdditionalPath = "something" });

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }
}