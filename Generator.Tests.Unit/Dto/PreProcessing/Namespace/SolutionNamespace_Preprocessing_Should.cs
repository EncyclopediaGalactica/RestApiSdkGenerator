namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SolutionNamespace_Preprocessing_Should : TestBase
{
    [Fact]
    public void PreProcess_WhenNoDtoNamespaceIsProvided()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_is_not_provided.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoInfoCollection");
    }

    [Fact]
    public void Preprocess_WhenDtoNamespaceIsProvided()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_is_provided.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Test.Namespace");
    }

    [Fact]
    public void Preprocess_NamespaceIsSmallcap()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/solution_namespace_smallcap.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Smallcap.Namespace");
    }

    [Fact]
    public void Preprocess_NamespaceWithoutDots()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/solution_namespace_no_dots.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Smallcap");
    }

    [Fact]
    public void Preprocess_When_DotIsLastChar()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/solution_namespace_has_dot_as_last_char.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Smallcap");
    }
}