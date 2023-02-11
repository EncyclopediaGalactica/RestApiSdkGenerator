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
    ///     Takes the <b>dto_project_additional_path</b> from the provided generator configuration and adds to the
    ///     type information objects used by the generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoAdditionalPathAndAddToTypeInfo(List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_project_base_path</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoProjectBasePathAndAddToTypeInfos(List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration generatorConfiguration);
}

public class ConfigurationToTypeInfoManager : IConfigurationToTypeInfoManager
{
    private readonly Logger<ConfigurationToTypeInfoManager> _logger = new Logger<ConfigurationToTypeInfoManager>(
        LoggerFactory.Create(options => options.AddConsole()));

    /// <inheritdoc />
    public void GetOriginalDtoAdditionalPathAndAddToTypeInfo(
        List<TypeInfo> typeInfos,
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
                nameof(GetOriginalDtoAdditionalPathAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectAdditionalPathToken = generatorConfiguration.DtoProjectAdditionalPath;
        }
    }

    public void GetOriginalDtoProjectBasePathAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectBasePath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectBasePath))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.DtoProjectBasePath));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{FileInfos} is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoAdditionalPathAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectBasePathToken = generatorConfiguration.DtoProjectBasePath;
        }
    }
}