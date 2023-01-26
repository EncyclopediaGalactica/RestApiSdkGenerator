namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Fact]
    public void ProcessDtoTypename()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };
        string typeNamePostfix = "Dto";

        // Act
        _sut.ProcessTypename(fileInfos, typeNamePostfix);

        // Assert
        fileInfos[0].Typename.Should().Be("OriginalTypenameTokenDto");
    }

    [Fact]
    public void MakeNoChange_ThrowNoError_When_EmptyFileInfoInput()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>();
        string typeNamePostfix = "Dto";

        // Act
        Action action = () => { _sut.ProcessTypename(fileInfos, typeNamePostfix); };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void ProcessDtoTypename_IfNoPostfixProvided()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };

        // Act
        _sut.ProcessTypename(fileInfos, null);

        // Assert
        fileInfos[0].Typename.Should().Be("OriginalTypenameToken");
    }
}