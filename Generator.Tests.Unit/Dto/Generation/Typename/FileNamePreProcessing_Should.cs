namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.Typename;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FileName_DtoGeneration_Should : TestBase
{
    [Fact]
    public void Generate_TheProperFilename()
    {
        // Arrange && Act
        List<string> filenames = new List<string>
        {
            "SimpleInGenerationPhaseDto.cs",
            "SomeComplexityInGenerationPhaseDto.cs",
            "ManyComplexityInTheNameInGenerationPhaseDto.cs"
        };
        string currentPath = $"{_basePath}/Dto/Generation/Typename";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        foreach (string filename in filenames)
        {
            List<string> reference = File.ReadAllLines($"{currentPath}/Reference/{filename}").ToList();
            List<string> result = File.ReadAllLines($"{currentPath}/{filename}").ToList();
            for (int i = 0; i < reference.Count; i++)
            {
                result[i].Should().Be(result[i],
                    $"Reference file: {currentPath}/Reference/{reference}. " +
                    $"Result file: {currentPath}/{result}");
            }
        }
    }
}