namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using HandlebarsDotNet;
using HandlebarsDotNet.Helpers;
using Microsoft.Extensions.Logging;
using Models;

public class TemplateManagerImpl : ITemplateManager
{
    private readonly IHandlebars _handlebarsContext;

    private readonly ILogger<TemplateManagerImpl> _logger = new Logger<TemplateManagerImpl>(LoggerFactory.Create(
        c => c.AddConsole()));

    public TemplateManagerImpl()
    {
        _handlebarsContext = Handlebars.Create();
        _logger.LogInformation("handlebar context is initiated");

        if (_handlebarsContext is null)
            throw new Exception($"Handlebar context is null.");

        HandlebarsHelpers.Register(_handlebarsContext);
        _logger.LogInformation("Handlebars Helpers are registered");
    }

    /// <inheritdoc />
    public string CompileTemplate(string template, FileInfoRender fileInfoRender)
    {
        HandlebarsTemplate<object, object>? compiledTemplate = _handlebarsContext.Compile(template);
        string compiledResult = compiledTemplate(fileInfoRender);

        if (string.IsNullOrWhiteSpace(compiledResult) || string.IsNullOrEmpty(compiledResult))
        {
            _logger.LogInformation("Template compile resulted in null or empty string");
            throw new Exception("Template compile resulted in null or empty string");
        }

        return compiledResult;
    }
}

public interface ITemplateManager
{
    /// <summary>
    ///     Compiles Handlebar template using the provided data structure.
    /// </summary>
    /// <param name="template">the Handlebar template content</param>
    /// <param name="fileInfoRender">file information</param>
    /// <returns>the compiled template</returns>
    string CompileTemplate(string template, FileInfoRender fileInfoRender);
}