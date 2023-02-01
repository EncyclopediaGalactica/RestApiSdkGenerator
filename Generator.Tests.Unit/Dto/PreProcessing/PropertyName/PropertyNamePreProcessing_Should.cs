namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.PropertyName;

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
    public void Process_PropertyNames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/PropertyName";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);

        TypeInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoFileInfos.First(p => p.Filename == "PetDto.cs");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Id").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Name").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "TagName").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerTypeProperty").Should().NotBeNull();
    }
}