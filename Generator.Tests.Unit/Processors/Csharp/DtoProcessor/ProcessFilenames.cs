namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Fact]
    public void ProcessFilename()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };
        string typenamePostfix = "Dto";
        string filetype = ".cs";

        // Act
        _sut.ProcessFilename(fileInfos, typenamePostfix, filetype);

        // Assert
        fileInfos[0].Filename.Should().Be("OriginalTypenameTokenDto.cs");
    }

    [Fact]
    public void NoOperationExecuted_When_FileInfosListIsEmpty()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>();
        string typenamePostfix = "Dto";
        string filetype = ".cs";

        // Act
        Action action = () => { _sut.ProcessFilename(fileInfos, typenamePostfix, filetype); };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Throw_WhenFilenamePostfix_IsNotProvided(string? filetype)
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };
        string typenamePostfix = "Dto";

        // Act
        Action action = () => { _sut.ProcessFilename(fileInfos, typenamePostfix, filetype!); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}