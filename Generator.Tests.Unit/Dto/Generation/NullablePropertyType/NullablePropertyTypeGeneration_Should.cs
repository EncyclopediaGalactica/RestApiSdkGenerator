namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.NullablePropertyType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NullablePropertyTypeGeneration_Should : TestBase
{
    [Fact]
    public void Generate_NullablePropertyTypes()
    {
        // Arrange && Act
        List<string> filenames = new()
        {
            "DoubleTypeNullableDto",
            "FloatTypeNullableDto",
            "IntegerTypeNullableDto",
            "LongTypeNullableDto",
            "StringNullableDto",
        };
        string currentPath = $"{_basePath}/Dto/Generation/NullablePropertyType";
        string configFilePath = $"{currentPath}/property_nullable_type_generation_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        foreach (string filename in filenames)
        {
            List<string> generated = File.ReadAllLines($"{currentPath}/{filename}.cs").ToList();
            List<string> reference = File.ReadAllLines($"{currentPath}/Reference/{filename}.cs").ToList();
            for (int i = 0; i < reference.Count; i++)
            {
                generated[i].Should().Be(reference[i],
                    $"Reference file: {currentPath}/Reference/{filename}.cs; " +
                    $"Generated result: {currentPath}/{filename}.cs");
            }
        }
    }
}