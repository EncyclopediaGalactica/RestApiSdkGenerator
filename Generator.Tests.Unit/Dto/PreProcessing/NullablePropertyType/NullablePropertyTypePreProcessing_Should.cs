namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.NullablePropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NullablePropertyTypePreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_NullablePropertyTypes()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/NullablePropertyType";
        string configFilePath = $"{currentPath}/property_nullable_type_preprocessing_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);

        GeneratedFileInfo aSingleDto = codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto");

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