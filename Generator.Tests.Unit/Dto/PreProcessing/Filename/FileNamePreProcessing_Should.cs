namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Filename;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FileNamePreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_FileName()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/Filename";
        string configFilePath = $"{currentPath}/filename_preprocessing_should.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "NewPetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "ErrorModelDto").ToList().Count.Should().Be(1);
    }
}