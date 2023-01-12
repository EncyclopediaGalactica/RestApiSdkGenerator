namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

public class PathManager : IPathManager
{
    public string BuildPath(string path1, string path2)
    {
        CheckStringInputAndThrow(path1);
        CheckStringInputAndThrow(path2);

        path1 = EnsurePathReadynesOfPathSegment(path1);
        path2 = EnsurePathReadynesOfPathSegment(path2);

        return $"{path1}{path2}";
    }

    /// <inheritdoc />
    public string BuildPath(string path1, string path2, string path3)
    {
        CheckStringInputAndThrow(path1);
        CheckStringInputAndThrow(path2);
        CheckStringInputAndThrow(path3);

        path1 = EnsurePathReadynesOfPathSegment(path1);
        path2 = EnsurePathReadynesOfPathSegment(path2);
        path3 = EnsurePathReadynesOfPathSegment(path3);

        return $"{path1}{path2}{path3}";
    }

    /// <inheritdoc />
    public string BuildPathWithFilename(string path, string fileName)
    {
        CheckStringInputAndThrow(path);
        CheckStringInputAndThrow(fileName);

        path = EnsurePathReadynesOfPathSegment(path);

        return $"{path}{fileName}";
    }

    /// <inheritdoc />
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    private string EnsurePathReadynesOfPathSegment(string path)
    {
        if (path[^1].ToString() == "/")
        {
            return path;
        }

        return $"{path}/";
    }

    private void CheckStringInputAndThrow(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrEmpty(path))
        {
            throw new ArgumentException(
                $"String input {nameof(path)} is null or empty."
            );
        }
    }
}

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
    string BuildPath(
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
    string BuildPath(
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
    string BuildPathWithFilename(string path, string fileName);

    /// <summary>
    ///     Provides the current directory path.
    /// </summary>
    /// <returns>Current directory path</returns>
    string GetCurrentDirectory();
}