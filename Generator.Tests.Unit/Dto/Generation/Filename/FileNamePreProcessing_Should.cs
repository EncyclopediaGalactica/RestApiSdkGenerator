namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.Filename;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("Generation")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FileName_DtoGeneration_Should : TestBase
{
    [Fact]
    public void Generate_TheProperFilename()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/Generation/Filename";
        string configFilePath = $"{currentPath}/filename_dto_generation_should.json";
        CodeGenerator? codeGenerator;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        File.Exists($"{currentPath}/FilenameCheckDto.cs").Should().BeTrue();
    }
}