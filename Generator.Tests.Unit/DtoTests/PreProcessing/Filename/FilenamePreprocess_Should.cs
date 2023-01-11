namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.Filename;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FilenamePreprocess_Should : TestBase
{
    [Fact]
    public void PreProcess_SingleFilename()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Filename";
        string configFilePath = $"{currentPath}/single_filename.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Where(p => p.FileName == "SingleFilenameDto_Should").ToList().Count.Should()
            .Be(1);
    }

    [Fact]
    public void PreProcess_MultipleFilenames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/Filename";
        string configFilePath = $"{currentPath}/multiple_filename.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Where(p => p.FileName == "FirstDto_Should").ToList().Count.Should().Be(1);
        codeGenerator.DtoTestFileInfos.Where(p => p.FileName == "SecondDto_Should").ToList().Count.Should().Be(1);
    }
}