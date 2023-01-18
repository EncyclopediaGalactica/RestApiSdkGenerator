namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.NullablePropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NullablePropertyType_Preprocessing_Should : TestBase
{
    [Fact]
    public void PreProcess_NullablePropertyTypes()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/NullablePropertyType";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Count.Should().Be(1);
        codeGenerator.DtoTestFileInfos.Where(
                p => p.Filename == "NullablePropertyTypeInDtoPreprocessingDto")
            .ToList().Count.Should().Be(1);

        FileInfo aSingleDto = codeGenerator.DtoTestFileInfos
            .First(p => p.Filename == "NullablePropertyTypeInDtoPreprocessingDto");

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "StringType").PropertyTypeName.Should().Be("string");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "StringType").IsNullable.Should().BeFalse();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "StringTypeNullable")
            .PropertyTypeName.Should().Be("string");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "StringTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerType").PropertyTypeName.Should().Be("int");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerType").IsNullable.Should().BeFalse();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerTypeNullable")
            .PropertyTypeName.Should().Be("int");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongType").PropertyTypeName.Should().Be("long");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongType").IsNullable.Should().BeFalse();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongTypeNullable")
            .PropertyTypeName.Should().Be("long");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatType")
            .PropertyTypeName.Should().Be("float");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatType").IsNullable.Should().BeFalse();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatTypeNullable")
            .PropertyTypeName.Should().Be("float");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleType")
            .PropertyTypeName.Should().Be("double");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleType").IsNullable.Should().BeFalse();

        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleTypeNullable")
            .PropertyTypeName.Should().Be("double");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleTypeNullable").IsNullable.Should().BeTrue();
    }
}