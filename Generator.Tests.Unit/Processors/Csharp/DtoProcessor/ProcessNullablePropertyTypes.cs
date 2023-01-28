namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoProcessor;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class DtoProcessor_Should
{
    [Theory]
    [InlineData("typename", true, true)]
    [InlineData("typename", false, false)]
    [InlineData(null, false, false)]
    [InlineData("", false, false)]
    [InlineData(" ", false, false)]
    [InlineData(null, true, false)]
    [InlineData("", true, false)]
    [InlineData(" ", true, false)]
    public void ProcessNullablePropertyTypes(string typeName, bool isNullable, bool expected)
    {
        // Arrange
        List<FileInfo> fileInfos = new()
        {
            new FileInfo
            {
                PropertyInfos = new List<PropertyInfo>
                {
                    new()
                    {
                        OriginalPropertyNameToken = typeName,
                    }
                },
            }
        };

        if (!isNullable)
        {
            fileInfos[0].RequiredProperties = new List<string> { typeName };
        }

        // Act
        _sut.ProcessNullablePropertyTypes(fileInfos);

        // Assert
        fileInfos[0].PropertyInfos.ToList()[0].IsNullable.Should().Be(expected);
    }
}