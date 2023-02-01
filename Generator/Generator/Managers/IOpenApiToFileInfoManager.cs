namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using Microsoft.OpenApi.Models;
using Models;
using OpenApiDocumentManager;

public interface IOpenApiToFileInfoManager
{
    /// <summary>
    ///     Takes type names from the provided OpenApi document and puts into the provided file info list.
    /// </summary>
    /// <param name="typeInfos">The file infos</param>
    /// <param name="openApiDocument">The Open Api document</param>
    /// <exception cref="Exception">In case of any error</exception>
    void GetTypeNamesFromOpenApiAndAddToFileInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the required properties for every type in the OpenApi document and puts into file infos.
    ///     <remarks>
    ///         As a result every type will have a list of required properties
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiDocument">The <see cref="OpenApiDocument" /></param>
    void GetRequiredPropertiesByTypeAndAddToFileInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);
}

public class OpenApiToFileInfoManager : IOpenApiToFileInfoManager
{
    private readonly IOpenApiDocumentManager _openApiDocumentManager;

    public OpenApiToFileInfoManager()
    {
        _openApiDocumentManager = new OpenApiDocumentManager.OpenApiDocumentManager();
    }

    /// <inheritdoc />
    public void GetTypeNamesFromOpenApiAndAddToFileInfo(
        List<TypeInfo> typeInfos,
        OpenApiDocument openApiDocument)
    {
        if (typeInfos.Any())
        {
            throw new GeneratorException("File infos list is not empty.");
        }

        List<string> typeNames = _openApiDocumentManager.Components.Schemas.GetSchemaNames(openApiDocument);

        foreach (string typeName in typeNames)
        {
            typeInfos.Add(new TypeInfo
            {
                OriginalTypeNameToken = typeName
            });
        }
    }

    public void GetRequiredPropertiesByTypeAndAddToFileInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
    {
        throw new NotImplementedException();
    }
}