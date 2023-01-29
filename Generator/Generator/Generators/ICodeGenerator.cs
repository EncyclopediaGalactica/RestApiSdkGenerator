namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using Configuration;
using Managers;
using Microsoft.OpenApi.Models;
using Models;

/// <summary>
///     Code Generator interface
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    ///     Gets or sets the DtoFileInfos value.
    ///     <remarks>
    ///         This property stores all the data needed preprocessing Dto generation phase.
    ///     </remarks>
    /// </summary>
    public List<FileInfo> DtoFileInfos { get; }

    /// <summary>
    ///     Gets or sets the DtoFileInfos value.
    ///     <remarks>
    ///         This property stores all the data needed Dto Tests preprocessing.
    ///     </remarks>
    /// </summary>
    public List<FileInfo> DtoTestFileInfos { get; }

    /// <summary>
    ///     Builder method for providing configuration values for the generator.
    /// </summary>
    /// <param name="configuration">The configuration object</param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetGeneratorConfiguration(CodeGeneratorConfiguration configuration);

    /// <summary>
    ///     Builder method for providing the OpenApi instance describing the Rest Api.
    /// </summary>
    /// <param name="openApiDocument">The OpenApi specification</param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetOpenApiYamlSchema(OpenApiDocument openApiDocument);

    /// <summary>
    ///     Builder method for providing instance of <see cref="IFileManager" />.
    /// </summary>
    /// <param name="fileManager">Instance of <see cref="IFileManager" /></param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetFileManager(IFileManager fileManager);

    /// <summary>
    ///     Builder method for providing instance of <see cref="IPathManager" />
    /// </summary>
    /// <param name="pathManager">instance of <see cref="IPathManager" /></param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetPathManager(IPathManager pathManager);

    /// <summary>
    ///     Builder method for providing instance of <see cref="ITemplateManager" />
    /// </summary>
    /// <param name="templateManager">instance of <see cref="ITemplateManager" /></param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetTemplateManager(ITemplateManager templateManager);

    /// <summary>
    ///     Starts the code generation.
    ///     <remarks>
    ///         It returns an instance of <see cref="ICodeGenerator" /> for only testing purposes.
    ///     </remarks>
    /// </summary>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator Generate();

    /// <summary>
    ///     Builder method for providing instance of <see cref="IStringManager" />
    /// </summary>
    /// <param name="stringManager">Instance of <see cref="IStringManager" /></param>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator SetStringManager(IStringManager stringManager);

    /// <summary>
    ///     Builder method for building the generator.
    /// </summary>
    /// <returns>Instance of <see cref="ICodeGenerator" /></returns>
    ICodeGenerator Build();
}