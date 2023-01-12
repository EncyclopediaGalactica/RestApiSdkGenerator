namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

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

    string ReadFileContent(string dotTestTemplatePath);
}

public class FileManager : IFileManager
{
    private ILogger<FileManager> _logger = new Logger<FileManager>(LoggerFactory.Create(c => { c.AddConsole(); }));

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
}