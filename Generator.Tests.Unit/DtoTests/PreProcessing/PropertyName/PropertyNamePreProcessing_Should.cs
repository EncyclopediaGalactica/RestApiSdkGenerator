namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.PropertyName;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyNamePreProcessing_Should : TestBase
{
    [Fact]
    public void Collect_PropertyNames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/PropertyName";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Count.Should().Be(1);
        codeGenerator.DtoTestFileInfos
            .Where(p => p.Filename == "PropertyNameInDtoTestsPreprocessingDto").ToList().Count.Should().Be(1);

        FileInfo aSingleDto = codeGenerator.DtoFileInfos
            .First(p => p.Filename == "PropertyNameInDtoTestsPreprocessingDto");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Id").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Name").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "TagName").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerTypeProperty").Should().NotBeNull();
    }
}