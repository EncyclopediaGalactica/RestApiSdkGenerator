namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiDocumentManager;

using Microsoft.OpenApi.Models;

public class Schemas : ISchemas
{
    /// <inheritdoc />
    public List<string> GetSchemaNames(Microsoft.OpenApi.Models.OpenApiDocument openApiDocument)
    {
        if (!openApiDocument.Components.Schemas.Any())
        {
            throw new ArgumentException("Openapi schema does not any components.schemas");
        }

        return openApiDocument.Components.Schemas.Select(p => p.Key).ToList();
    }

    /// <inheritdoc />
    public IDictionary<string, OpenApiSchema> GetSchemas(OpenApiDocument openApiDocument)
    {
        if (!openApiDocument.Components.Schemas.Any())
        {
            throw new ArgumentException("Openapi schema does not any components.schemas");
        }

        if (!openApiDocument.Components.Schemas.Any())
        {
            return new Dictionary<string, OpenApiSchema>();
        }

        return openApiDocument.Components.Schemas;
    }

    /// <inheritdoc />
    public List<string> GetRequiredPropertiesBySchema(string schemaName, OpenApiDocument openApiDocument)
    {
        if (string.IsNullOrEmpty(schemaName) || string.IsNullOrWhiteSpace(schemaName))
        {
            throw new ArgumentException(nameof(schemaName));
        }

        IDictionary<string, OpenApiSchema> schemas = GetSchemas(openApiDocument);

        if (!schemas.Any() || schemas.Where(p => p.Key == schemaName).ToList().Count == 0)
        {
            return new List<string>();
        }

        return schemas.First(p => p.Key == schemaName).Value.Required.ToList();
    }
}