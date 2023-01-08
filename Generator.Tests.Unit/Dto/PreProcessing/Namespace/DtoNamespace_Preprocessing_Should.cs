namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DtoNamespace_Preprocessing_Should : TestBase
{
    [Fact]
    public void Preprocess_When_NamespaceIsSmallcap()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_smallcap.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Proper.Solution.Namespace.Dto.Project.Namespace.Smallcap");
    }

    [Fact]
    public void Preprocess_NamespaceWithoutDots()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_without_dots.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Proper.Solution.Namespace.Dtoprojectnamespacesmallcap");
    }

    [Fact]
    public void Preprocess_DotIsLastChar()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_namespace_last_char_dot.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("Proper.Solution.Namespace.Dto.Namespace");
    }
}