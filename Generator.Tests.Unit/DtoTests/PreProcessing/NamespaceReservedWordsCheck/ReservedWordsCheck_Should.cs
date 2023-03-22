namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.
    NamespaceReservedWordsCheck;

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
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_is_reserved_word.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_AtStart()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word1.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_InTheMiddle()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word2.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_AtTheEnd()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word3.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}