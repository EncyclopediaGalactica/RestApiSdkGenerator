namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.Generation.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Namespace_Generation_Should : TestBase
{
    [Fact]
    public void Generate_Namespace_WhenNoDtoNamespaceIsProvided()
    {
        // Arrange && Act
        string filename = "DtoNamespaceIsNotProvidedDto.cs";
        string currentPath = $"{_basePath}/Dto/Generation/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_is_not_provided.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        List<string> referenceResult = File.ReadAllLines(
            $"{currentPath}/Reference/{filename}").ToList();
        List<string> generatedResult = File.ReadAllLines($"{currentPath}/{filename}").ToList();
        for (int i = 0; i < referenceResult.Count; i++)
        {
            generatedResult[i].Should().Be(referenceResult[i],
                $"There is a difference in line {i}. \n" +
                $"Reference file: {currentPath}/Reference/{filename} \n" +
                $"Generated file: {currentPath}/{filename}");
        }
    }

    [Fact]
    public void Preprocess_Namespace_WhenDtoNamespaceIsProvided()
    {
        // Arrange && Act
        string filename = "DtoNamespaceIsProvidedDto.cs";
        string currentPath = $"{_basePath}/Dto/Generation/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_is_provided.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        List<string> referenceResult = File.ReadAllLines(
            $"{currentPath}/Reference/{filename}").ToList();
        List<string> generatedResult = File.ReadAllLines($"{currentPath}/{filename}").ToList();
        for (int i = 0; i < referenceResult.Count; i++)
        {
            generatedResult[i].Should().Be(referenceResult[i],
                $"There is a difference in line {i}. \n" +
                $"Reference file: {currentPath}/Reference/{filename} \n" +
                $"Generated file: {currentPath}/{filename}");
        }
    }
}