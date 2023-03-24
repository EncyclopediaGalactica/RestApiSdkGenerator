namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DtoGenerating_Should : TestBase
{
    [Fact]
    public void Generate_MultipleDtos()
    {
        // Arrange && Act
        Dictionary<string, string> filenames = new Dictionary<string, string>
        {
            { "MultipleOneDto.cs", "MultipleOneDto.cs" },
            { "MultipleTwoDto.cs", "MultipleTwoDto.cs" },
        };
        string currentPath = $"{_basePath}/Dto/Generation";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        foreach (KeyValuePair<string, string> filename in filenames)
        {
            List<string> reference = File.ReadLines($"{currentPath}/Reference/{filename.Key}").ToList();
            List<string> generated = File.ReadLines($"{currentPath}/{filename.Value}").ToList();
            for (int i = 0; i < reference.Count(); i++)
            {
                generated[i].Should().Be(reference[i],
                    $"There is a difference in line {i}. \n" +
                    $"Reference file: {_basePath}/Reference/{filename.Key} \n" +
                    $"Generated file: {_basePath}{filename.Value}");
            }
        }
    }
}