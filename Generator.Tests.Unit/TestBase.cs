namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit;

using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

public class TestBase
{
    protected readonly string BasePath = Directory.GetCurrentDirectory();
    protected readonly ITestOutputHelper OutputHelper;

    public TestBase(ITestOutputHelper outputHelper)
    {
        ArgumentNullException.ThrowIfNull(outputHelper);
        OutputHelper = outputHelper;
    }

    /// <summary>
    ///     If the pattern can be found in the line it returns true.
    ///     This method is used in generated code testing due to that we have value changes every generation. One of them is
    ///     when the code was generated.
    /// </summary>
    /// <param name="line">the actual line</param>
    /// <param name="pattern">the pattern </param>
    /// <returns>bool</returns>
    protected bool ShouldBeIgnored(string line, string pattern)
    {
        if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(pattern))
        {
            return false;
        }

        if (line.Contains(pattern))
        {
            return true;
        }

        return false;
    }

    [Theory]
    [InlineData("line", "#TEST_IGNORE", false)]
    [InlineData("lines and lines", "#TEST_IGNORE", false)]
    [InlineData("lines and lines", "#TEST_IGNORE", false)]
    [InlineData("#TEST_IGNORE", "#TEST_IGNORE", true)]
    [InlineData("public class Whatever<T,Y> { #TEST_IGNORE", "#TEST_IGNORE", true)]
    public void ShouldBeIgnoredTests(
        string line,
        string pattern,
        bool expectedResult)
    {
        // Arrange && Act
        bool result = ShouldBeIgnored(line, pattern);

        // Assert
        result.Should().Be(expectedResult);
    }
}