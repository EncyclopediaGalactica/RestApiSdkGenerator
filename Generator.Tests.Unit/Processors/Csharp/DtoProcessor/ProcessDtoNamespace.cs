namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("first", "second", "First.Second")]
    [InlineData(".first", "second", "First.Second")]
    [InlineData(".first.", "second", "First.Second")]
    [InlineData("first", ".second", "First.Second")]
    [InlineData("first", "second.", "First.Second")]
    [InlineData("first", ".second.", "First.Second")]
    [InlineData("first.First", "second", "First.First.Second")]
    [InlineData("First", "second", "First.Second")]
    [InlineData("first", "Second", "First.Second")]
    [InlineData("First", "Second", "First.Second")]
    public void ProcessDtoNamespace(
        string baseNamespace,
        string additionalNamespace,
        string expected)
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                OriginalBaseNamespaceToken = baseNamespace,
                OriginalDtoNamespaceToken = additionalNamespace
            }
        };

        // Act
        _sut.ProcessDtoNamespace(fileInfos);

        // Assert
        fileInfos[0].Namespace.Should().Be(expected);
    }
}