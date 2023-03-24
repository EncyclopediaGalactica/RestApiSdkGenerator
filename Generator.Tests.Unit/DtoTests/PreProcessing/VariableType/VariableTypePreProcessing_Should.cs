namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.VariableType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class VariableTypePreProcessing_Should : TestBase
{
    [Fact]
    public void Collect_Types()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/VariableType";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.FileName == "PetDto.cs").ToList().Count.Should()
            .Be(1);

        TypeInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.FileName == "PetDto.cs");
        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerType").VariableTypeName.Should().Be("int");
        aSingleDto.VariableInfos.First(p => p.VariableName == "LongType").VariableTypeName.Should().Be("long");
        aSingleDto.VariableInfos.First(p => p.VariableName == "FloatType").VariableTypeName.Should().Be("float");
        aSingleDto.VariableInfos.First(p => p.VariableName == "DoubleType").VariableTypeName.Should().Be("double");
    }
}