namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.VariableName_Properties;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class VariableNamePreProcessing_Should : TestBase
{
    [Fact]
    public void Process_PropertyNames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/VariableName_Properties";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.FileName == "PetDto.cs").ToList().Count.Should()
            .Be(1);

        TypeInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.FileName == "PetDto.cs");
        aSingleDto.VariableInfos.First(p => p.VariableName == "Id").Should().NotBeNull();
        aSingleDto.VariableInfos.First(p => p.VariableName == "Name").Should().NotBeNull();
        aSingleDto.VariableInfos.First(p => p.VariableName == "TagName").Should().NotBeNull();
        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerTypeProperty").Should().NotBeNull();
    }
}