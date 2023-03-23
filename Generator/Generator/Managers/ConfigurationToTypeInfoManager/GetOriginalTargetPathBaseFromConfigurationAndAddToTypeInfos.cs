namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationToTypeInfoManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationToTypeInfoManager
{
    /// <inheritdoc />
    public void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(generatorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.TargetDirectory),
                nameof(GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalTargetDirectoryToken = generatorConfiguration.TargetDirectory;
        }
    }
}