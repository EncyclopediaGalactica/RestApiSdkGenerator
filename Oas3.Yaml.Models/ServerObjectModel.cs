namespace Oas3.Yaml.Models;

public class ServerObjectModel
{
    public string Url { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> Variables { get; set; }
}