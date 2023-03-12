namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using Models;

public interface ITemplateManager
{
    /// <summary>
    ///     Compiles Handlebar template using the provided data structure.
    /// </summary>
    /// <param name="template">the Handlebar template content</param>
    /// <param name="dtoTypeInfoRender">file information</param>
    /// <returns>the compiled template</returns>
    string CompileTemplate(string template, DtoTypeInfoRender dtoTypeInfoRender);
}