namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("/asd/something", "filename.txt", "/asd/something/filename.txt")]
    [InlineData(" ", "filename.txt", null)]
    [InlineData("", "filename.txt", null)]
    [InlineData(null, "filename.txt", null)]
    [InlineData("/asd/something", "", null)]
    [InlineData("/asd/something", " ", null)]
    [InlineData("/asd/something", null, null)]
    public void ProcessPathWithFileName(
        string targetPath,
        string fileName,
        string expected)
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                AbsoluteTargetPath = targetPath,
                Filename = fileName
            }
        };

        // Act
        _sut.ProcessPathWithFileName(fileInfos);

        // Assert
        fileInfos[0].TargetPathWithFileName.Should().Be(expected);
    }
}