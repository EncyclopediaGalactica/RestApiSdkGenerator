namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.TargetDirectory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TargetDirectoryPreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_TargetDirectory_Value()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoTests/PreProcessing/TargetDirectory";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoTestFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoTestFileInfos.Count.Should().Be(1);
        codeGenerator.DtoTestFileInfos
            .Where(p => p.FileName == "TargetDirectoryInDtoTestsPreprocessingDto").ToList().Count.Should().Be(1);

        GeneratedFileInfo aSingleDto = codeGenerator.DtoFileInfos
            .First(p => p.FileName == "TargetDirectoryInDtoTestsPreprocessingDto");
        aSingleDto.TargetDirectory.Should().NotBeNull();
        aSingleDto.TargetDirectory.Should().Be("DtoTests/PreProcessing/TargetDirectory/");
    }
}