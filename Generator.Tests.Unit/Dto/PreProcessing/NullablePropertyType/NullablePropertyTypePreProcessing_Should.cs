namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.NullablePropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
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
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);

        TypeInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs");

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