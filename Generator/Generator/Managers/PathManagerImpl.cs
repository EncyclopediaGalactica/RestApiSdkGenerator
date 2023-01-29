namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

/// <summary>
///     Path manager interfaces
///     <remarks>
///         Deals with all the path related tasks during code generation
///     </remarks>
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

    /// <summary>
    ///     Checks if given path exists
    /// </summary>
    /// <param name="path">the path to the directory</param>
    /// <returns>bool</returns>
    bool IsPathExists(string path);

    /// <summary>
    ///     Creates the designated path
    /// </summary>
    /// <param name="path">the path</param>
    void CreatePath(string path);

    /// <summary>
    ///     Deletes the designated path
    /// </summary>
    /// <param name="path">the path</param>
    /// <param name="recursive">should the deletion be recursive or not</param>
    void DeletePath(string path, bool recursive = false);

    /// <summary>
    ///     Checks if the given path has "/" as last character. If so, it removes it and returns this new version.
    /// </summary>
    /// <param name="path">the path</param>
    /// <returns>string</returns>
    string CheckAndRemoveEndSlash(string path);

    /// <summary>
    ///     It checks if a path is absolute or relative. If relative it makes an absolute one. It uses the directory path where
    ///     the code runs and creates an absolute path
    /// </summary>
    /// <param name="path">the path</param>
    /// <returns>the new path</returns>
    string CheckIfPathAbsoluteOrMakeItOne(string path);
}

/// <inheritdoc />
public class PathManagerImpl : IPathManager
{
    private readonly Logger<PathManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

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
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        if (string.IsNullOrEmpty(fileName)
            || string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        path = CheckAndRemoveEndSlash(path);

        return $"{path}/{fileName}";
    }

    public void CheckOrCreatePath(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

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

    public bool IsPathExists(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        return Directory.Exists(path);
    }

    public void CreatePath(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        Directory.CreateDirectory(path);
    }

    public void DeletePath(string path, bool recursive = false)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        Directory.Delete(path, recursive);
    }

    public string CheckAndRemoveEndSlash(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (path[^1].ToString() == "/")
        {
            return path[..^1];
        }

        return path;
    }

    public string CheckIfPathAbsoluteOrMakeItOne(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        if (path[0].ToString() == "/")
        {
            return path;
        }

        StringBuilder builder = new StringBuilder();
        builder
            .Append(GetCurrentDirectory())
            .Append("/")
            .Append(path);

        _logger.LogInformation(
            "Path is not absolute path. Will make it one" +
            "Old path: {OldPath}, new path: {NewPath}",
            path, builder.ToString());

        return builder.ToString();
    }

    private string ConcatenatePathSegments(
        string? pathSegment1 = null,
        string? pathSegment2 = null,
        string? pathSegment3 = null)
    {
        StringBuilder builder = new StringBuilder();
        bool isFirstParamNotUsable = false;
        bool isSecondParamNotUsable = false;

        if (!string.IsNullOrEmpty(pathSegment1)
            && !string.IsNullOrWhiteSpace(pathSegment1))
        {
            builder.Append(pathSegment1.Trim());
        }
        else
        {
            isFirstParamNotUsable = true;
        }

        if (!string.IsNullOrEmpty(pathSegment2)
            && !string.IsNullOrWhiteSpace(pathSegment2))
        {
            if (isFirstParamNotUsable)
            {
                builder.Append(pathSegment2.Trim());
            }
            else
            {
                builder.Append("/").Append(pathSegment2.Trim());
            }
        }
        else
        {
            isSecondParamNotUsable = true;
        }

        if (!string.IsNullOrEmpty(pathSegment3)
            && !string.IsNullOrWhiteSpace(pathSegment3))
        {
            if (isFirstParamNotUsable && isSecondParamNotUsable)
            {
                builder.Append(pathSegment3.Trim());
            }
            else
            {
                builder.Append("/").Append(pathSegment3.Trim());
            }
        }

        return builder.ToString();
    }
}