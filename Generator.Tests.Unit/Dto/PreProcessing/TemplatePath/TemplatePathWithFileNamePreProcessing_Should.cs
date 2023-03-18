namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.TemplatePath;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TemplatePathWithFileNamePreProcessing_Should : TestBase
{
    [Fact]
    public void PreProcess_TemplatePath()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/TemplatePath";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(3);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Where(
                p => !string.IsNullOrEmpty(p.TemplateAbsolutePathWithFileName)
                     && p.TemplateAbsolutePathWithFileName.Contains("dto.handlebars")
            )
            .ToList().Count
            .Should().Be(3);
    }
}