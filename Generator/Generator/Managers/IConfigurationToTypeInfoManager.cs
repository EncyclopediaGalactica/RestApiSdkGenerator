namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

/// <summary>
///     Interface providing method to extract configuration values
/// </summary>
public interface IConfigurationToTypeInfoManager
{
    /// <summary>
    ///     Takes the <b>dto_project_additional_path</b> from the provided generator configuration and adds to the code
    ///     type information objects.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void AddDtoAdditionalPathToTypeInfo(List<TypeInfo> typeInfos, CodeGeneratorConfiguration generatorConfiguration);
}

public class ConfigurationToTypeInfoManager : IConfigurationToTypeInfoManager
{
    private readonly Logger<ConfigurationToTypeInfoManager> _logger = new Logger<ConfigurationToTypeInfoManager>(
        LoggerFactory.Create(options => options.AddConsole()));

    public void AddDtoAdditionalPathToTypeInfo(List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectAdditionalPath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectAdditionalPath))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.DtoProjectAdditionalPath));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{FileInfos} is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(AddDtoAdditionalPathToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectAdditionalPathToken = generatorConfiguration.DtoProjectAdditionalPath;
        }
    }
}