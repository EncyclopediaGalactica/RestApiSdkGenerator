namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("RESERVED", "reserved", true)]
    [InlineData("rEsErVeD", "reserved", true)]
    [InlineData("notreserved", "reserved", false)]
    public void CheckIfPropertyNameReservedWord(string word, string reservedWord, bool shouldThrow)
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo { OriginalPropertyNameToken = word }
                }
            }
        };
        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.CheckIfPropertyNameIsReservedWord(fileInfos, reservedWords); };

        // Assert
        if (shouldThrow)
        {
            action.Should().Throw<GeneratorException>();
        }
    }

    [Fact]
    public void CheckIfPropertyNameReservedWord_NotThrow_WhenFileInfoListEmpty()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo> { };
        List<string> reservedWords = new List<string> { "reserved" };

        // Act
        Action action = () => { _sut.CheckIfPropertyNameIsReservedWord(fileInfos, reservedWords); };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void CheckIfPropertyNameReservedWord_WhenReservedWordsListEmpty()
    {
        // Arrange
        List<FileInfo> fileInfos = new List<FileInfo>
        {
            new FileInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new PropertyInfo { OriginalPropertyNameToken = "something" }
                }
            }
        };
        List<string> reservedWords = new List<string>();

        // Act
        Action action = () => { _sut.CheckIfPropertyNameIsReservedWord(fileInfos, reservedWords); };

        // Assert
        action.Should().NotThrow();
    }
}