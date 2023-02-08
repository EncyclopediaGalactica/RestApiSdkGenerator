namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.PropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyTypePreProcessing_Should : TestBase
{
    [Fact]
    public void Collect_Types()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/PropertyType";
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
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerType").PropertyTypeName.Should().Be("int");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongType").PropertyTypeName.Should().Be("long");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatType").PropertyTypeName.Should().Be("float");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleType").PropertyTypeName.Should().Be("double");
    }
}