namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.SingleDto;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("Generation")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SingleDto_Generating_Should : TestBase
{
    [Fact]
    public async Task SingleDto()
    {
        // Arrange && Act
        string referenceFileName = "PetDto.cs";
        string generatedFileName = "PetDto.cs";
        string currentPath = $"{_basePath}/Dto/Generation/SingleDto";
        string configFilePath = $"{currentPath}/single_dto_generating_should.json";
        CodeGenerator? codeGenerator;
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        List<string> referenceResult = File.ReadLines($"{currentPath}/Reference/{referenceFileName}").ToList();
        List<string> generatedResult = File.ReadLines($"{currentPath}/{generatedFileName}").ToList();
        for (int i = 0; i < referenceResult.Count(); i++)
        {
            generatedResult[i].Should().Be(referenceResult[i],
                $"There is a difference in line {i}. \n" +
                $"Reference file: {currentPath}/{referenceFileName} \n" +
                $"Generated file: {currentPath}/{generatedFileName}");
        }
    }
}