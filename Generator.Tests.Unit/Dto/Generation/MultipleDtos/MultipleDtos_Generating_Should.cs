namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.MultipleDtos;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class MultipleDtos_Generating_Should : TestBase
{
    // [Fact]
    public async Task Generate_MultipleDtos()
    {
        // Arrange && Act
        Dictionary<string, string> filenames = new Dictionary<string, string>
        {
            { "MultipleOne.cs", "MultipleOneDto.cs" },
            { "MultipleTwo.cs", "MultipleTwoDto.cs" },
        };
        string currentPath = $"{_basePath}/Dto/Generation/MultipleDtos";
        string configFilePath = $"{currentPath}/multiple_dtos_generating_should.json";
        CodeGenerator? codeGenerator;
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