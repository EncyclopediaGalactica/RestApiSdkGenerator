namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.PropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyTypePreProcessing_Should : TestBase
{
    [Fact]
    public void Collect_Types()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/PropertyType";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Count.Should().Be(1);
        codeGenerator.DtoTestFileInfos
            .Where(p => p.FileName == "PropertyTypeInDtoTestsPreprocessingDto").ToList().Count.Should().Be(1);

        GeneratedFileInfo aSingleDto = codeGenerator.DtoFileInfos
            .First(p => p.FileName == "PropertyTypeInDtoTestsPreprocessingDto");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerType").PropertyTypeName.Should().Be("int");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongType").PropertyTypeName.Should().Be("long");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatType").PropertyTypeName.Should().Be("float");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleType").PropertyTypeName.Should().Be("double");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "StringType").PropertyTypeName.Should().Be("string");
    }
}