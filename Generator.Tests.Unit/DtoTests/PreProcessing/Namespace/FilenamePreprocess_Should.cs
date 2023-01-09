namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NamespacePreprocess_Should : TestBase
{
    [Fact]
    public void PreProcess_DtoTestNamespaceIsNotProvided_SingleFile()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_not_provided_single.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "SingleFilenameDto_Should")
            .Namespace.Should().Be("Solution.Namespace.Single");
    }

    [Fact]
    public void PreProcess_DtoTestNamespaceIsNotProvided_MultipleFile()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_not_provided_multiple.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "FirstDto_Should")
            .Namespace.Should().Be("Solution.Namespace.Multiple");
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "SecondDto_Should")
            .Namespace.Should().Be("Solution.Namespace.Multiple");
    }

    [Fact]
    public void PreProcess_DtoTestNamespaceIsProvided_SingleFile()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_provided_single.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "SingleDto_Should")
            .Namespace.Should().Be("Solution.Namespace.Prov.Single.Dto.Tests");
    }

    [Fact]
    public void PreProcess_DtoTestNamespaceIsProvided_MultipleFile()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_provided_multiple.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "FirstNamespaceProvidedDto_Should")
            .Namespace.Should().Be("Sol.Namespace.Dto.Tests");
        codeGenerator.DtoTestFileInfos.First(p => p.FileName == "SecondNamespaceProvidedDto_Should")
            .Namespace.Should().Be("Sol.Namespace.Dto.Tests");
    }
}