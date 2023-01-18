namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

/// <inheritdoc />
public class PathManagerImpl : IPathManager
{
    private Logger<PathManagerImpl> _logger = new Logger<PathManagerImpl>(LoggerFactory.Create(c => c.AddConsole()));

    public string BuildPathString(string path1, string path2)
    {
        return ConcatenatePathSegments(path1, path2);
    }

    /// <inheritdoc />
    public string BuildPathString(string path1, string path2, string path3)
    {
        return ConcatenatePathSegments(path1, path2, path3);
    }

    /// <inheritdoc />
    public string BuildPathStringWithFilename(string path, string fileName)
    {
        path = CheckAndRemoveEndSlash(path);

        return $"{path}/{fileName}";
    }

    public void CheckOrCreatePath(string path)
    {
        path = CheckIfPathAbsoluteOrMakeItOne(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            _logger.LogInformation("Directory does not exist, create one: {Path}", path);
        }
    }

    /// <inheritdoc />
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    private string CheckAndRemoveEndSlash(string path)
    {
        if (path[^1].ToString() == "/")
        {
            return path;
        }

        return path;
    }

    private string CheckIfPathAbsoluteOrMakeItOne(string path)
    {
        if (path[0].ToString() == "/")
        {
            return path;
        }

        StringBuilder builder = new StringBuilder();
        builder.Append(GetCurrentDirectory())
            .Append("/")
            .Append(path);

        _logger.LogInformation(
            "Path is not absolute path. Will make it one" +
            "Old path: {OldPath}, new path: {NewPath}",
            path, builder.ToString());

        return builder.ToString();
    }

    private string ConcatenatePathSegments(
        string pathSegment1,
        string pathSegment2 = null,
        string pathSegment3 = null)
    {
        StringBuilder builder = new StringBuilder();

        if (!string.IsNullOrEmpty(pathSegment1) || !string.IsNullOrWhiteSpace(pathSegment1))
        {
            builder.Append(pathSegment1);
        }

        if (!string.IsNullOrEmpty(pathSegment2) || !string.IsNullOrWhiteSpace(pathSegment2))
        {
            builder.Append("/").Append(pathSegment2);
        }

        if (!string.IsNullOrEmpty(pathSegment3) || !string.IsNullOrWhiteSpace(pathSegment3))
        {
            builder.Append("/").Append(pathSegment3);
        }

        return builder.ToString();
    }
}

/// <summary>
///     Deals with all the path related tasks during code generation
/// </summary>
public interface IPathManager
{
    /// <summary>
    ///     Takes the provided strings and concatenates them into a valid path.
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item>ensures slash between string fragment</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="path1">path fragment</param>
    /// <param name="path2">path fragment</param>
    /// <returns>Path constructed from the input path fragments</returns>
    string BuildPathString(
        string path1,
        string path2);

    /// <summary>
    ///     Takes the provided strings and concatenates them into a valid path.
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item>ensures slash between string fragment</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="path1">path fragment</param>
    /// <param name="path2">path fragment</param>
    /// <param name="path3">path fragment</param>
    /// <returns>Path constructed from the input path fragments</returns>
    string BuildPathString(
        string path1,
        string path2,
        string path3);

    /// <summary>
    ///     Builds a path from path and filename input parameters
    ///     <remarks>
    ///         It ensures that there is a slash between path and filename.
    ///     </remarks>
    /// </summary>
    /// <param name="path">the path to the directory where the file is placed</param>
    /// <param name="fileName">the filename</param>
    /// <returns>path to the file</returns>
    string BuildPathStringWithFilename(string path, string fileName);

    /// <summary>
    ///     Checks if the provided path exists. If not it creates it.
    /// </summary>
    /// <param name="path">the path</param>
    void CheckOrCreatePath(string path);

    /// <summary>
    ///     Provides the current directory path.
    /// </summary>
    /// <returns>Current directory path</returns>
    string GetCurrentDirectory();
}