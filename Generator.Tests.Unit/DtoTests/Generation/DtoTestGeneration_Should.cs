namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.Generation;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DtoTestGeneration_Should : TestBase
{
    // [Fact]
    public void Generate()
    {
        // Arrange
        List<string> filenames = new List<string>
        {
            "SimpleExample_Should"
        };
        string configFilePath = $"{_basePath}/DtoTests/Generation/config.json";
        CodeGenerator codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
    }
}