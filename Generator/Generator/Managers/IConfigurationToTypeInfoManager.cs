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
    void GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_project_base_path</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>target_directory</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_project_namespace</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>solution_base_namespace</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_test_project_base_path</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_test_project_additional_path</b> from the provided generator configuration and adds to the
    ///     type information objects will be used by the generator for code generation
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration generatorConfiguration);

    /// <summary>
    ///     Takes the <b>dto_test_project_namespace</b> from the provided configuration and adds to the type information
    ///     objects will be used by teh generator
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration generatorConfiguration);
}

public class ConfigurationToTypeInfoManager : IConfigurationToTypeInfoManager
{
    private readonly Logger<ConfigurationToTypeInfoManager> _logger = new Logger<ConfigurationToTypeInfoManager>(
        LoggerFactory.Create(options => options.AddConsole()));

    /// <inheritdoc />
    public void GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(
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
                nameof(GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectAdditionalPathToken = generatorConfiguration.DtoProjectAdditionalPath;
        }
    }

    public void GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(
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
                nameof(GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectBasePathToken = generatorConfiguration.DtoProjectBasePath;
        }
    }

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

    /// <inheritdoc />
    public void GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectNameSpace)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectNameSpace))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.DtoProjectNameSpace),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoNamespaceToken = generatorConfiguration.DtoProjectNameSpace;
        }
    }

    /// <inheritdoc />
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.SolutionBaseNamespace)
            || string.IsNullOrWhiteSpace(generatorConfiguration.SolutionBaseNamespace))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.SolutionBaseNamespace),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalBaseNamespaceToken = generatorConfiguration.SolutionBaseNamespace;
        }
    }

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

    public void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoTestProjectAdditionalPath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoTestProjectAdditionalPath))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.DtoTestProjectAdditionalPath));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{FileInfos} is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoTestProjectAdditionalPathToken = generatorConfiguration.DtoTestProjectAdditionalPath;
        }
    }

    /// <inheritdoc />
    public void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoTestProjectNameSpace)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoTestProjectNameSpace))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.DtoTestProjectNameSpace));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{FileInfos} is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoTestProjectNamespaceToken = generatorConfiguration.DtoTestProjectNameSpace;
        }
    }
}