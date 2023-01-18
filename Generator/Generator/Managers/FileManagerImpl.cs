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
    /// <param name="pathWithFilename">path to the file</param>
    void DeleteFile(string pathWithFilename);

    /// <summary>
    ///     Checks if the directory exists. If not creates one.
    ///     <remarks>
    ///         It creates only the last segment of the path. If further segments are missing it fails.
    ///     </remarks>
    /// </summary>
    /// <param name="dir">path to be checked</param>
    void CheckIfExistsOrCreate(string dir);

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
    /// <param name="compiledContent">the content</param>
    /// <param name="pathToFile">the file</param>
    void WriteContentIntoFile(string compiledContent, string pathToFile);
}

public class FileManagerImpl : IFileManager
{
    private ILogger<FileManagerImpl> _logger =
        new Logger<FileManagerImpl>(LoggerFactory.Create(c => { c.AddConsole(); }));

    /// <inheritdoc />
    public void DeleteFile(string pathWithFilename)
    {
        if (string.IsNullOrEmpty(pathWithFilename) || string.IsNullOrWhiteSpace(pathWithFilename))
        {
            if (File.Exists(pathWithFilename))
            {
                File.Delete(pathWithFilename);
                _logger.LogInformation("File is deleted: {Path}", pathWithFilename);
                return;
            }

            _logger.LogInformation("Path does not exist: {Path}", pathWithFilename);
            return;
        }

        _logger.LogInformation("Path is not provided: {Path}", pathWithFilename);
    }

    /// <inheritdoc />
    public void CheckIfExistsOrCreate(string dir)
    {
        if (!File.Exists(dir))
        {
            _logger.LogInformation("Path does not exist, it will be created. {Path}", dir);
            File.Create(dir);
        }
    }

    /// <inheritdoc />
    public string ReadAllText(string pathToFile)
    {
        string? result = File.ReadAllText(pathToFile);

        if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
        {
            _logger.LogInformation(
                "Reading file resulted empty or null. Filepath: {PathToFile}", pathToFile);
            throw new Exception($"Reading file resulted empty or null. " +
                                $"Filepath: {pathToFile}");
        }

        return result;
    }

    public void WriteContentIntoFile(string compiledContent, string pathToFile)
    {
        ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        using FileStream fileStream = File.Open(pathToFile,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.Inheritable);
        fileStream.Write(asciiEncoding.GetBytes(compiledContent), 0, asciiEncoding.GetByteCount(compiledContent));
    }
}