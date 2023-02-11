namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using OpenApiDocumentManager;

public interface IOpenApiToTypeInfoManager
{
    /// <summary>
    ///     Takes type names from the provided OpenApi document and puts into the provided file info list.
    /// </summary>
    /// <param name="typeInfos">The file infos</param>
    /// <param name="openApiDocument">The Open Api document</param>
    /// <exception cref="Exception">In case of any error</exception>
    void GetTypeNamesFromOpenApiAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the required properties for every type in the OpenApi document and puts into file infos.
    ///     <remarks>
    ///         As a result every type will have a list of required properties
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiDocument">The <see cref="OpenApiDocument" /></param>
    void GetRequiredPropertiesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the property names from the provided <see cref="OpenApiDocument" /> and adds them to the
    ///     provided <see cref="TypeInfo" />s
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    void GetPropertyNamesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the property types from the provided <see cref="OpenApiDocument" /> and adds them to the provided
    ///     <see cref="TypeInfo" />s
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" />s</param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);
}

public class OpenApiToTypeInfoManager : IOpenApiToTypeInfoManager
{
    private readonly Logger<OpenApiToTypeInfoManager> _logger = new Logger<OpenApiToTypeInfoManager>(
        LoggerFactory.Create(c => { c.AddConsole(); }));

    private readonly IOpenApiDocumentManager _openApiDocumentManager;

    public OpenApiToTypeInfoManager()
    {
        _openApiDocumentManager = new OpenApiDocumentManager.OpenApiDocumentManager();
    }

    /// <inheritdoc />
    public void GetTypeNamesFromOpenApiAndAddToTypeInfo(
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

    /// <inheritdoc />
    public void GetRequiredPropertiesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available type infos");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                _logger.LogInformation("Original type name token is null, empty or whitespace");
                continue;
            }

            List<string> requiredProperties = _openApiDocumentManager.Components.Schemas.GetRequiredPropertiesBySchema(
                typeInfo.OriginalTypeNameToken,
                openApiDocument);
            typeInfo.RequiredProperties = requiredProperties;
        }
    }

    /// <inheritdoc />
    public void GetPropertyNamesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available type infos");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                _logger.LogInformation("Original type name token is null, empty or whitespace");
                continue;
            }

            List<string> propertyNamesBySchema = _openApiDocumentManager.Components.Schemas.GetPropertyNamesBySchema(
                typeInfo.OriginalTypeNameToken,
                openApiDocument);

            if (!propertyNamesBySchema.Any())
            {
                continue;
            }

            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            foreach (string property in propertyNamesBySchema)
            {
                propertyInfos.Add(new PropertyInfo
                {
                    OriginalPropertyNameToken = property
                });
            }

            typeInfo.PropertyInfos = propertyInfos;
        }
    }

    /// <inheritdoc />
    public void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available type infos");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                _logger.LogInformation("Original type name token is null, empty or whitespace");
                continue;
            }

            IDictionary<string, Dictionary<string, string>> propertyTypesBySchemas = _openApiDocumentManager.Components
                .Schemas.GetPropertyTypesBySchema(typeInfo.OriginalTypeNameToken, openApiDocument);

            if (!propertyTypesBySchemas.Any())
            {
                continue;
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in propertyTypesBySchemas)
            {
                PropertyInfo propertyInfo = typeInfo.PropertyInfos
                    .First(p => p.OriginalPropertyNameToken == keyValuePair.Key);
                propertyInfo.OriginalPropertyTypeNameToken = keyValuePair.Value["type"];
                propertyInfo.OriginalPropertyTypeFormat = keyValuePair.Value["format"];
            }
        }
    }
}