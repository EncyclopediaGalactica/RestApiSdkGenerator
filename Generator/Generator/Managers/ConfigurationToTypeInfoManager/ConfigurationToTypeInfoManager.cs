namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationToTypeInfoManager;

using Microsoft.Extensions.Logging;

/// <inheritdoc />
public partial class ConfigurationToTypeInfoManager : IConfigurationToTypeInfoManager
{
    private readonly Logger<ConfigurationToTypeInfoManager> _logger = new Logger<ConfigurationToTypeInfoManager>(
        LoggerFactory.Create(options => options.AddConsole()));
}