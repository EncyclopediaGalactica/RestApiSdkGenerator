namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.PropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyTypeGeneration_Should : TestBase
{
    [Fact]
    public void Generate_Types()
    {
        // Arrange && Act
        string filename = "PetDto";
        string currentPath = $"{_basePath}/Dto/Generation/PropertyType";
        string configFilePath = $"{currentPath}/property_type_generation_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        List<string> reference = File.ReadAllLines($"{currentPath}/Reference/{filename}.cs").ToList();
        List<string> generated = File.ReadAllLines($"{currentPath}/{filename}.cs").ToList();
        for (int i = 0; i < reference.Count; i++)
        {
            generated[i].Should().Be(reference[i],
                $"Reference: {currentPath}/Reference/{filename}.cs; " +
                $"Generated: {currentPath}/{filename}.cs");
        }
    }
}