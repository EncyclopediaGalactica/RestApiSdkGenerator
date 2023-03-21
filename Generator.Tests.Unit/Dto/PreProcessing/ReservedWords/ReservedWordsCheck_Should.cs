namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.ReservedWords;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ReservedWordsCheck_Should : TestBase
{
    [Fact]
    public void Throw_WhenBaseNamespaceIsAReservedWord()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/ReservedWords";
        string configFilePath = $"{currentPath}/base_namespace_is_reserved_word.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/ReservedWords";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}