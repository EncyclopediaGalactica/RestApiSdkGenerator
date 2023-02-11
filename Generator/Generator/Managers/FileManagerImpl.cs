namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

/// <inheritdoc />
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