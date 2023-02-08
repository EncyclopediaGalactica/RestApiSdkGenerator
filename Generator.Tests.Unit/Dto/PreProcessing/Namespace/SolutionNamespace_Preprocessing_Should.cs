namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
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
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs")
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
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs")
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
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs")
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
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs")
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
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.Filename == "PetDto.cs").ToList().Count.Should()
            .Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.Filename == "PetDto.cs")
            .Namespace.Should().Be("Smallcap");
    }
}