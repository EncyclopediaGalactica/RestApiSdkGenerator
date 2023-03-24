namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.TargetDirectory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TargetDirectoryPreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_WhenOnlyBasePath()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/TargetDirectory";
        string configFilePath = $"{currentPath}/basepath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should");
        typeInfo!.AbsoluteTargetPath.Should()
            .Be($"{_basePath}/DtoTests/PreProcessing/TargetDirectory");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/TargetDirectory";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should");
        typeInfo.Should().NotBeNull();
        typeInfo!.AbsoluteTargetPath.Should()
            .Be($"{_basePath}/DtoTests/PreProcessing/TargetDirectory/testprojectBasepath");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject_AndAdditionalPath()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/TargetDirectory";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_additionalpath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetDirectoryInDtoTestsPreprocessingDto_Should");
        typeInfo.Should().NotBeNull();
        typeInfo!.AbsoluteTargetPath.Should()
            .Be($"{_basePath}/DtoTests/PreProcessing/TargetDirectory/testprojectBasepath/additionalPath");
    }
}