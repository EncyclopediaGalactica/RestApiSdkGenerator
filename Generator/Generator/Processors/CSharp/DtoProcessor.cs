namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Managers;
using Microsoft.Extensions.Logging;
using Models;

/// <inheritdoc />
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

    /// <inheritdoc />
    public void ProcessTypename(List<FileInfo> dtoFileInfos, string? typeNamePostfix)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
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

    /// <inheritdoc />
    public void ProcessDtoNamespace(List<FileInfo> dtoFileInfos)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            fileInfo.Namespace = _stringManager.MakeCapitalLetterTheOneAfterTheDot(
                _stringManager.ConcatCsharpNamespaceTokens(
                    fileInfo.OriginalBaseNamespaceToken,
                    fileInfo.OriginalDtoNamespaceToken));
        }
    }

    /// <inheritdoc />
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
                if (string.IsNullOrEmpty(propertyInfo.OriginalPropertyNameToken)
                    || string.IsNullOrWhiteSpace(propertyInfo.OriginalPropertyNameToken))
                {
                    propertyInfo.PropertyName = propertyInfo.OriginalPropertyNameToken;
                    continue;
                }

                if (reservedWords.Contains(propertyInfo.OriginalPropertyNameToken.ToLower()))
                {
                    throw new GeneratorException(
                        $"{propertyInfo.OriginalPropertyNameToken} is a reserved word.");
                }

                propertyInfo.PropertyName = _stringManager.MakeSnakeCaseToPascalCase(
                    propertyInfo.OriginalPropertyNameToken);
            }
        }
    }

    /// <inheritdoc />
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

    public void ProcessNullablePropertyTypes(List<FileInfo> dtoFileInfos)
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
                if (string.IsNullOrEmpty(propertyInfo.OriginalPropertyNameToken)
                    || string.IsNullOrWhiteSpace(propertyInfo.OriginalPropertyNameToken))
                {
                    continue;
                }

                if (fileInfo.RequiredProperties is not null
                    && fileInfo.RequiredProperties.Any()
                    && fileInfo.RequiredProperties.Contains(propertyInfo.OriginalPropertyNameToken.ToLower()))
                {
                    propertyInfo.IsNullable = false;
                    continue;
                }

                propertyInfo.IsNullable = true;
            }
        }
    }

    /// <inheritdoc />
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
                    builder
                        .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalTargetDirectoryToken))
                        .Append("/");
                }
                else
                {
                    builder
                        .Append(_pathManager.GetCurrentDirectory())
                        .Append("/")
                        .Append(_stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalTargetDirectoryToken))
                        .Append("/");
                }
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoPojectBasePathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoPojectBasePathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoPojectBasePathToken);
                builder.Append(
                    _stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoPojectBasePathToken));
            }

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoProjectAdditionalPathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoProjectAdditionalPathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoProjectAdditionalPathToken);
                builder.Append("/").Append(
                    _stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoProjectAdditionalPathToken));
            }

            fileInfo.AbsoluteTargetPath = builder.ToString();
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
            if (string.IsNullOrEmpty(fileInfo.AbsoluteTargetPath) ||
                string.IsNullOrWhiteSpace(fileInfo.AbsoluteTargetPath))
            {
                _logger.LogInformation("Target directory is not defined");
                return;
            }

            if (string.IsNullOrEmpty(fileInfo.Filename) || string.IsNullOrWhiteSpace(fileInfo.Filename))
            {
                _logger.LogInformation("Filename is not defined");
                return;
            }

            fileInfo.TargetPathWithFileName = _pathManager.BuildPathString(
                fileInfo.AbsoluteTargetPath,
                fileInfo.Filename);
        }
    }

    public void ProcessDtoTemplatePath(List<FileInfo> dtoFileInfos, string dtoTemplatePath)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (string.IsNullOrEmpty(dtoTemplatePath) || string.IsNullOrWhiteSpace(dtoTemplatePath))
        {
            _logger.LogInformation("Dto template path is not provided");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            dtoTemplatePath = _pathManager.CheckIfPathAbsoluteOrMakeItOne(dtoTemplatePath);

            fileInfo.TemplateAbsolutePathWithFileName = dtoTemplatePath;
        }
    }

    public void CheckIfPropertyNameIsReservedWord(List<FileInfo> dtoFileInfos, List<string> reservedWords)
    {
        if (!dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (!reservedWords.Any())
        {
            _logger.LogInformation("List of reserved is empty");
            return;
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            foreach (PropertyInfo propertyInfo in fileInfo.PropertyInfos)
            {
                if (string.IsNullOrEmpty(propertyInfo.OriginalPropertyNameToken)
                    || string.IsNullOrWhiteSpace(propertyInfo.OriginalPropertyNameToken))
                {
                    continue;
                }

                if (reservedWords.Contains(propertyInfo.OriginalPropertyNameToken.ToLower()))
                {
                    throw new GeneratorException(
                        $"PropertyName - {propertyInfo.OriginalPropertyNameToken} - is a reserved word.");
                }
            }
        }
    }

    public void ProcessOpenApiTypesToCsharpTypes(
        List<FileInfo> dtoFileInfos,
        Dictionary<string, string> openApiCsharpTypeMap)
    {
        if (dtoFileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
        }

        foreach (FileInfo fileInfo in dtoFileInfos)
        {
            foreach (PropertyInfo propertyInfo in fileInfo.PropertyInfos)
            {
                propertyInfo.PropertyTypeName = DecideCsharpType(
                    propertyInfo.OriginalPropertyTypeNameToken,
                    propertyInfo.OriginalPropertyTypeFormat,
                    openApiCsharpTypeMap);
            }
        }
    }

    public void ProcessFilename(
        List<FileInfo> fileInfos,
        string? filenamePostfix,
        string? fileType)
    {
        if (!fileInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (string.IsNullOrEmpty(filenamePostfix)
            || string.IsNullOrWhiteSpace(filenamePostfix))
        {
            _logger.LogInformation("Typename postfix is empty, null or whitespace");
        }

        if (string.IsNullOrEmpty(fileType)
            || string.IsNullOrWhiteSpace(fileType))
        {
            throw new ArgumentException("No file type provided");
        }

        foreach (FileInfo fileInfo in fileInfos)
        {
            string fileName = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(fileInfo.OriginalTypeNameToken),
                _stringManager.MakeFirstCharUpperCase(filenamePostfix!),
                _stringManager.CheckIfFirstCharIsDotOrAddIt(
                    _stringManager.ToLowerCase(fileType!))
            );
            fileInfo.Filename = fileName;
        }
    }

    private string? DecideCsharpType(
        string? originalPropertyTypenameToken,
        string? originalPropertyTypeFormatToken,
        Dictionary<string, string> openApiCsharpTypeMap)
    {
        if (string.IsNullOrEmpty(originalPropertyTypenameToken)
            || string.IsNullOrWhiteSpace(originalPropertyTypenameToken))
        {
            _logger.LogInformation("Original property type name parameter is empty. Exiting.");
            return string.Empty;
        }

        if (!openApiCsharpTypeMap.Any())
        {
            _logger.LogInformation("Open api type - csharp type map is empty. Exiting.");
            return string.Empty;
        }

        string? type;
        switch (originalPropertyTypenameToken.ToLower())
        {
            case "integer":

                if (string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    throw new GeneratorException($"No format is specified for integer");
                }

                switch (originalPropertyTypeFormatToken.ToLower())
                {
                    case "int32":
                        openApiCsharpTypeMap.TryGetValue("integer-int32", out type);
                        return type;

                    case "int64":
                        openApiCsharpTypeMap.TryGetValue("integer-int64", out type);
                        return type;

                    default:
                        throw new GeneratorException(
                            $"No type for {originalPropertyTypenameToken} - {originalPropertyTypeFormatToken}");
                }

            case "number":

                if (string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    throw new GeneratorException($"No format is specified for number");
                }

                switch (originalPropertyTypeFormatToken.ToLower())
                {
                    case "float":
                        openApiCsharpTypeMap.TryGetValue("number-float", out type);
                        return type;

                    case "double":
                        openApiCsharpTypeMap.TryGetValue("number-double", out type);
                        return type;

                    default:
                        throw new GeneratorException(
                            $"No type for {originalPropertyTypenameToken} - {originalPropertyTypeFormatToken}");
                }

            case "string":
                if (!string.IsNullOrEmpty(originalPropertyTypeFormatToken)
                    || !string.IsNullOrWhiteSpace(originalPropertyTypeFormatToken))
                {
                    switch (originalPropertyTypeFormatToken.ToLower())
                    {
                        case "byte":
                            openApiCsharpTypeMap.TryGetValue("string-byte", out type);
                            return type;
                        case "binary":
                            openApiCsharpTypeMap.TryGetValue("string-binary", out type);
                            return type;
                        case "date":
                            openApiCsharpTypeMap.TryGetValue("string-date", out type);
                            return type;
                        case "date-time":
                            openApiCsharpTypeMap.TryGetValue("string-date-time", out type);
                            return type;
                        default:
                            openApiCsharpTypeMap.TryGetValue("string", out type);
                            return type;
                    }
                }

                openApiCsharpTypeMap.TryGetValue("string", out type);
                return type;

            case "boolean":
                openApiCsharpTypeMap.TryGetValue("boolean", out type);
                return type;
            default:
                throw new GeneratorException();
        }
    }
}