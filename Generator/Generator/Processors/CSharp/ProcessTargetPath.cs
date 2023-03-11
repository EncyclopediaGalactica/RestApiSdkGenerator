namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using System.Text;
using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessTargetPath(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
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

            if (!string.IsNullOrEmpty(fileInfo.OriginalDtoProjectBasePathToken)
                && !string.IsNullOrWhiteSpace(fileInfo.OriginalDtoProjectBasePathToken))
            {
                _stringManager.CheckIfFirstCharIsSlashAndThrow(fileInfo.OriginalDtoProjectBasePathToken);
                builder.Append(
                    _stringManager.CheckIfLastCharSlashAndRemoveIt(fileInfo.OriginalDtoProjectBasePathToken));
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
}