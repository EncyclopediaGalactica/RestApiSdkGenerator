namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationToTypeInfoManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationToTypeInfoManager
{
    /// <inheritdoc />
    public void GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoTestProjectBasePath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoTestProjectBasePath))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.DtoTestProjectBasePath),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            typeInfo.OriginalDtoTestProjectBasePathToken = generatorConfiguration.DtoTestProjectBasePath;
        }
    }
}