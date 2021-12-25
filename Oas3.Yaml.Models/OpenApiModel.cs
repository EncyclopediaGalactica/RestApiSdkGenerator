namespace Oas3.Yaml.Models;

public class OpenApiModel
{
    public string OpenApi { get; set; }
    public InfoModel Info { get; set; }
    public IList<ServerObjectModel> Servers { get; set; }
    public string Paths { get; set; }
    public string Components { get; set; }
    public string Security { get; set; }
    public string Tags { get; set; }
    public string ExternalDocs { get; set; }
}