namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Managers;
using Microsoft.Extensions.Logging;
using Models;

public class DtoProcessor : IDtoProcessor
{
    private readonly IFileManager _fileManager;
    private readonly Logger<DtoProcessor> _logger = new(LoggerFactory.Create(c => c.AddConsole()));
    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;

    public DtoProcessor(
        IFileManager fileManager,
        IStringManager stringManager,
        IPathManager pathManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        ArgumentNullException.ThrowIfNull(stringManager);
        ArgumentNullException.ThrowIfNull(pathManager);

        _fileManager = fileManager;
        _stringManager = stringManager;
        _pathManager = pathManager;
    }

    public void ProcessDtoTypeName(List<FileInfo> dtoFileInfos, string typeNamePostfix)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
        }

        if (string.IsNullOrEmpty(typeNamePostfix) || string.IsNullOrWhiteSpace(typeNamePostfix))
        {
            _logger.LogInformation("Typename postfix is empty, null or whitespace");
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            string typeName = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(fileInfo.OriginalTypeNameToken),
                typeNamePostfix
            );
            fileInfo.Typename = typeName;
        }
    }

    public void ProcessDtoNamespace(List<FileInfo> dtoFileInfos)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            fileInfo.Namespace = _stringManager.ValidateCsharpNamespace(
                _stringManager.ConcatCsharpNamespaceTokens(
                    fileInfo.OriginalBaseNamespaceToken,
                    fileInfo.OriginalDtoNamespaceToken));
        }
    }

    public void ProcessPropertyNames(List<FileInfo> dtoFileInfos, List<string> reservedWords)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            foreach (PropertyInfo propertyInfo in fileInfo.PropertyInfos)
            {
                if (reservedWords.Contains(propertyInfo.OriginalPropertyNameToken))
                {
                    _logger.LogInformation(
                        "Property name - {PN} - is a reserved word. Please fix it",
                        propertyInfo.OriginalPropertyNameToken);
                }

                propertyInfo.PropertyName = _stringManager.MakeFirstCharUpperCase(
                    propertyInfo.OriginalPropertyNameToken);
            }
        }
    }

    public void ProcessPropertyTypeNames(
        List<FileInfo> dtoFileInfos,
        List<string> reservedWords,
        List<string> valueTypes)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            foreach (PropertyInfo propertyInfo in fileInfo.PropertyInfos)
            {
                if (reservedWords.Contains(propertyInfo.OriginalPropertyTypeNameToken))
                {
                    _logger.LogInformation(
                        "Property name - {PN} - is a reserved word. Please fix it",
                        propertyInfo.OriginalPropertyTypeNameToken);
                }

                if (valueTypes.Contains(propertyInfo.OriginalPropertyTypeNameToken))
                {
                    propertyInfo.PropertyTypeName = _stringManager.ToLowerCase(
                        propertyInfo.OriginalPropertyTypeNameToken);
                    continue;
                }

                propertyInfo.PropertyTypeName = _stringManager.MakeFirstCharUpperCase(
                    propertyInfo.OriginalPropertyTypeNameToken);
            }
        }
    }

    public void ProcessTargetPath(List<FileInfo> dtoFileInfos)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(fileInfo.OriginalTargetDirectoryToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalTargetDirectoryToken))
            {
                if (_stringManager.IsFirstCharIsASlash(fileInfo.OriginalTargetDirectoryToken))
                {
                    builder.Append(_pathManager.GetCurrentDirectory());
                }
                else
                {
                    if (_stringManager.IsLastCharASlash(fileInfo.OriginalTargetDirectoryToken))
                    {
                        builder.Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(
                            fileInfo.OriginalTargetDirectoryToken));
                    }
                }
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoPojectBasePathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoPojectBasePathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoPojectBasePathToken);
                builder.Append("/").Append(
                    _stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoPojectBasePathToken));
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoProjectAdditionalPathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoProjectAdditionalPathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoPojectBasePathToken);
                builder.Append("/").Append(
                    _stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoProjectAdditionalPathToken));
            }

            fileInfo.TargetDirectory = builder.ToString();
        }
    }

    public void ProcessPathWithFileName(List<FileInfo> dtoFileInfos)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            if (string.IsNullOrEmpty(fileInfo.TargetDirectory) || string.IsNullOrWhiteSpace(fileInfo.TargetDirectory))
            {
                _logger.LogInformation("Target directory is not defined");
            }

            if (string.IsNullOrEmpty(fileInfo.Filename) || string.IsNullOrWhiteSpace(fileInfo.Filename))
            {
                _logger.LogInformation("Filename is not defined");
            }

            fileInfo.TargetPathWithFileName = _stringManager.Concat(
                fileInfo.TargetDirectory,
                fileInfo.Filename);
        }
    }

    public void ProcessDtoTemplatePath(List<FileInfo> dtoFileInfos, string dtoTemplatePath)
    {
        if (dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
        }

        if (string.IsNullOrEmpty(dtoTemplatePath) || string.IsNullOrWhiteSpace(dtoTemplatePath))
        {
            _logger.LogInformation("Dto template path is not provided");
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            StringBuilder builder = new StringBuilder();
            if (!_stringManager.IsFirstCharIsASlash(dtoTemplatePath))
            {
                builder.Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(_pathManager.GetCurrentDirectory()))
                    .Append("/")
                    .Append(dtoTemplatePath);
            }
            else
            {
                builder.Append(dtoTemplatePath);
            }

            fileInfo.TemplateAbsolutePathWithFileName = builder.ToString();
        }
    }

    public void ProcessDtoFileNames(List<FileInfo> dtoFileInfos, string dtoFileNamePostFix, string fileType)
    {
        if (dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
        }

        if (string.IsNullOrEmpty(dtoFileNamePostFix) || string.IsNullOrWhiteSpace(dtoFileNamePostFix))
        {
            _logger.LogInformation("Typename postfix is empty, null or whitespace");
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            string fileName = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(fileInfo.OriginalTypeNameToken),
                dtoFileNamePostFix,
                fileType
            );
            fileInfo.Filename = fileName;
        }
    }
}