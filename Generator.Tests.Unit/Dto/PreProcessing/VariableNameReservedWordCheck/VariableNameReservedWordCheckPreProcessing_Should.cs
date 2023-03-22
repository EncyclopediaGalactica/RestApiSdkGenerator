namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.
    VariableNameReservedWordCheck;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class VariableNameReservedWordCheckPreProcessing_Should : TestBase
{
    [Fact]
    public void Process_PropertyNames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/Dto/PreProcessing/VariableNameReservedWordCheck";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}