namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

public interface IFileManager
{
    /// <summary>
    ///     Deletes the file specified by the path.
    ///     <remarks>
    ///         If the files does not exist no error will be displayed
    ///     </remarks>
    /// </summary>
    /// <param name="pathToFile">path to the file</param>
    void DeleteFile(string pathToFile);

    /// <summary>
    ///     Checks if the directory exists. If not creates one.
    ///     <remarks>
    ///         It creates only the last segment of the path. If further segments are missing it fails.
    ///     </remarks>
    /// </summary>
    /// <param name="pathToFile">path to be checked</param>
    void CheckIfExistsOrCreate(string pathToFile);

    bool CheckIfFileExist(string pathToFile);

    /// <summary>
    ///     Reads the content of the designated file and stores in memory for further processing
    /// </summary>
    /// <param name="pathToFile">path for the file</param>
    /// <returns>the content of the designated file</returns>
    string ReadAllText(string pathToFile);

    /// <summary>
    ///     Write the provided content into the provided file.
    ///     <remarks>
    ///         This method does not deal with creating the file if it a non-existing one.
    ///         One has to make sure the file exist.
    ///     </remarks>
    /// </summary>
    /// <param name="content">the content</param>
    /// <param name="pathToFile">the file</param>
    void WriteContentIntoFile(string content, string pathToFile);
}

public class FileManagerImpl : IFileManager
{
    private readonly ILogger<FileManagerImpl> _logger =
        new Logger<FileManagerImpl>(LoggerFactory.Create(c => { c.AddConsole(); }));

    /// <inheritdoc />
    public void DeleteFile(string pathToFile)
    {
        if (!string.IsNullOrEmpty(pathToFile)
            || !string.IsNullOrWhiteSpace(pathToFile))
        {
            if (CheckIfFileExist(pathToFile))
            {
                File.Delete(pathToFile);
                _logger.LogInformation("File is deleted: {Path}", pathToFile);
                return;
            }

            _logger.LogInformation("Path does not exist: {Path}", pathToFile);
            return;
        }

        _logger.LogInformation("Path is not provided: {Path}", pathToFile);
    }

    /// <inheritdoc />
    public void CheckIfExistsOrCreate(string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile)
            || string.IsNullOrWhiteSpace(pathToFile))
        {
            throw new ArgumentNullException(nameof(pathToFile));
        }

        if (CheckIfFileExist(pathToFile)) return;

        _logger.LogInformation("Path does not exist, it will be created. {Path}", pathToFile);
        File.Create(pathToFile);
    }

    public bool CheckIfFileExist(string pathToFile)
    {
        if (!string.IsNullOrEmpty(pathToFile)
            && !string.IsNullOrWhiteSpace(pathToFile))
        {
            return File.Exists(pathToFile);
        }

        return false;
    }

    /// <inheritdoc />
    public string ReadAllText(string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile)
            || string.IsNullOrWhiteSpace(pathToFile))
        {
            throw new ArgumentNullException(nameof(pathToFile));
        }

        string? result = File.ReadAllText(pathToFile);

        if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
        {
            _logger.LogInformation(
                "Reading file resulted empty or null. Filepath: {PathToFile}", pathToFile);
        }

        return result;
    }

    public void WriteContentIntoFile(string content, string pathToFile)
    {
        ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        using FileStream fileStream = File.Open(pathToFile,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.Inheritable);
        fileStream.Write(asciiEncoding.GetBytes(content), 0, asciiEncoding.GetByteCount(content));
        _logger.LogInformation("Content is written in the {file}", pathToFile);
    }
}