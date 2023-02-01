namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("filenameToken", "Dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("FilenameToken", "Dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", "cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", ".Cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", ".CS", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", "CS", "FilenameTokenDto.cs")]
    public void ProcessFilename(
        string typenameToken,
        string filenamePostfix,
        string fileType,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = typenameToken
            }
        };
        string typenamePostfix = filenamePostfix;
        string filetype = fileType;

        // Act
        _sut.ProcessFilename(fileInfos, typenamePostfix, filetype);

        // Assert
        fileInfos[0].Filename.Should().Be(expected);
    }

    [Fact]
    public void NoOperationExecuted_When_FileInfosListIsEmpty()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
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
    public void Throw_WhenFilenamePostfix_IsNotProvided(string filetype)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
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