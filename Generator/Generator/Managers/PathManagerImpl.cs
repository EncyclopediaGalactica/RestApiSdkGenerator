namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

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