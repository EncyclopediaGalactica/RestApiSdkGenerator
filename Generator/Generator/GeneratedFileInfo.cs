namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

/// <summary>
///     Generated File Information object
///     <remarks>
///         The data stored in this object describes the generated file.
///         As of now only c-sharp code is generated. Despite the fact that a c-sharp
///         file may contain multiple classes we assume that one file one class.
///     </remarks>
/// </summary>
public class GeneratedFileInfo
{
    /// <summary>
    ///     Gets or sets the filename
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     Gets or sets the namespace
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    ///     Gets or sets the target directory
    /// </summary>
    public string? TargetDirectory { get; set; }

    /// <summary>
    ///     Gets or sets the property info
    /// </summary>
    public ICollection<PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo>();
}