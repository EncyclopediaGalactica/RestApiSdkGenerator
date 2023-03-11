namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using OpenApiDocumentManager;

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

            List<VariableInfo> propertyInfos = new List<VariableInfo>();
            foreach (string property in propertyNamesBySchema)
            {
                propertyInfos.Add(new VariableInfo
                {
                    OriginalVariableNameToken = property
                });
            }

            typeInfo.VariableInfos = propertyInfos;
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
                VariableInfo variableInfo = typeInfo.VariableInfos
                    .First(p => p.OriginalVariableNameToken == keyValuePair.Key);
                variableInfo.OriginalVariableTypeNameToken = keyValuePair.Value["type"];
                variableInfo.OriginalVariableTypeFormat = keyValuePair.Value["format"];
            }
        }
    }

    public void MarkVariablesAsPropertyBasedOnOpenApiSchema(
        List<TypeInfo> typeInfos,
        OpenApiDocument openApiYamlSchema)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("TypeInfos list is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (!typeInfo.VariableInfos.Any())
            {
                _logger.LogInformation("{Type} does not have variables", typeInfo.OriginalTypeNameToken);
                continue;
            }

            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                && string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                continue;
            }

            List<string> variableNames = _openApiDocumentManager.Components.Schemas
                .GetPropertyNamesBySchema(typeInfo.OriginalTypeNameToken, openApiYamlSchema);
            List<string> lowerCasedVariableNames = variableNames.Select(p => p.ToLower()).ToList();

            foreach (VariableInfo variableInfo in typeInfo.VariableInfos)
            {
                if (string.IsNullOrEmpty(variableInfo.OriginalVariableNameToken)
                    || string.IsNullOrWhiteSpace(variableInfo.OriginalVariableNameToken))
                {
                    continue;
                }

                if (lowerCasedVariableNames.Contains(variableInfo.OriginalVariableNameToken.ToLower()))
                {
                    variableInfo.IsProperty = true;
                }
            }
        }
    }
}