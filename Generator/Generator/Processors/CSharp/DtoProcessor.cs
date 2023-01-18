namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers;
using Microsoft.Extensions.Logging;
using Models;

public class DtoProcessor : IDtoProcessor
{
    private readonly IFileManager _fileManager;
    private readonly Logger<DtoProcessor> _logger = new(LoggerFactory.Create(c => c.AddConsole()));
    private readonly IStringManager _stringManager;

    public DtoProcessor(
        IFileManager fileManager,
        IStringManager stringManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        ArgumentNullException.ThrowIfNull(stringManager);

        _fileManager = fileManager;
        _stringManager = stringManager;
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