namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.TargetDirectory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
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
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoFileInfos.Where(p => p.Filename == "TargetDirectoryDto.cs")
            .ToList().Count.Should().Be(1);

        FileInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoFileInfos
            .First(p => p.Filename == "TargetDirectoryDto.cs");
        aSingleDto.AbsoluteTargetPath.Should().NotBeNull();
        aSingleDto.AbsoluteTargetPath.Should().Be(
            $"{Directory.GetCurrentDirectory()}/Dto/PreProcessing/TargetDirectory/");
    }
}