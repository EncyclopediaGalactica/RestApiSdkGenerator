namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.FileName;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FileNamePreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_FileName()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/FileName";
        string configFilePath = $"{currentPath}/filename_preprocessing_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "NewPetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "ErrorModelDto").ToList().Count.Should().Be(1);
    }
}