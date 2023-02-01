namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp.DtoTestsProcessor;

using FluentAssertions;
using Generator.Managers;
using Generator.Processors.CSharp;
using Xunit;

public partial class DtoTestProcessor_Should
{
    public static IEnumerable<object[]> Throw_WhenCtorParamIsNull_Data = new List<object[]>
    {
        new object[] { null, new StringManagerImpl(), new PathManagerImpl() },
        new object[] { new FileManagerImpl(), null, new PathManagerImpl() },
        new object[] { new FileManagerImpl(), new StringManagerImpl(), null },
    };


    [Theory]
    [MemberData(nameof(Throw_WhenCtorParamIsNull_Data))]
    public void Throw_WhenCtorParamIsNull(
        IFileManager fileManager,
        IStringManager stringManager,
        IPathManager pathManager)
    {
        // Arrange && Act
        Action action = () => { new DtoTestProcessor(fileManager, stringManager, pathManager); };

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}