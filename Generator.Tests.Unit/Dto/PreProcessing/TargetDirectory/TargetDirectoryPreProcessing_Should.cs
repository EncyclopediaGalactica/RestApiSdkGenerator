namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.TargetDirectory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TargetDirectoryPreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_TargetDirectory_Value()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/TargetDirectory";
        string configFilePath = $"{currentPath}/target_directory_preprocessing_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "TargetDirectoryDto").ToList().Count.Should().Be(1);

        GeneratedFileInfo aSingleDto = codeGenerator.DtoFileInfos.First(p => p.FileName == "TargetDirectoryDto");
        aSingleDto.TargetDirectory.Should().NotBeNull();
        aSingleDto.TargetDirectory.Should().Be("Dto/PreProcessing/TargetDirectory/");
    }
}