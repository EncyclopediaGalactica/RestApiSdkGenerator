namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("asd", "Asd")]
    [InlineData("alphaBetaGamma", "AlphaBetaGamma")]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void ProcessPropertyNames(string name, string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo
                    {
                        OriginalPropertyNameToken = name
                    }
                }
            }
        };

        // Act
        _sut.ProcessPropertyNames(fileInfos, new List<string> { "bla" });

        // Assert
        fileInfos[0].PropertyInfos.ToList()[0].PropertyName.Should().Be(expected);
    }

    [Theory]
    [InlineData("namespace", "namespace")]
    [InlineData("Namespace", "namespace")]
    public void Throw_WhenPropertyName_AReservedWord(string name, string reserved)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo
                    {
                        OriginalPropertyNameToken = name
                    }
                }
            }
        };

        // Act
        Action action = () => { _sut.ProcessPropertyNames(fileInfos, new List<string> { reserved }); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}