namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

/// <summary>
///     Generated File Information object
///     <remarks>
///         The data stored in this object describes the generated file.
///         As of now only c-sharp code is generated. Despite the fact that a c-sharp
///         file may contain multiple classes we assume that one file one class.
///     </remarks>
/// </summary>
public class FileInfo
{
    /// <summary>
    ///     Gets or sets the original type name property.
    ///     <remarks>
    ///         The original type name value comes directly from the yaml file and may contains postfix
    ///         like "Dto".
    ///     </remarks>
    /// </summary>
    public string OriginalTypename { get; set; }

    public string OriginalBaseNamespaceToken { get; set; }
    public string OriginalDtoNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the original type name token.
    ///     <remarks>
    ///         It comes from the yaml file without any changes.
    ///     </remarks>
    /// </summary>
    public string OriginalTypeNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the reference type name.
    /// </summary>
    public string Typename { get; set; }

    /// <summary>
    ///     Gets or sets the reference type name as variable name
    ///     <remarks>
    ///         <para>
    ///             In C# the variable name, which is the type name, starts with a small capital letter
    ///             while the type name starts with capital letter.
    ///             Type name: GeneratedFileInfo
    ///             Variable name: generatedFileInfo
    ///         </para>
    ///     </remarks>
    /// </summary>
    public string TypenameAsVariableName { get; set; }

    /// <summary>
    ///     Gets or sets the filename
    /// </summary>
    public string Filename { get; set; }

    /// <summary>
    ///     Gets or sets the namespace
    /// </summary>
    public string Namespace { get; set; }

    /// <summary>
    ///     Gets or sets the target directory
    /// </summary>
    public string? TargetDirectory { get; set; }

    public string OriginalTargetDirectoryToken { get; set; }
    public string OriginalDtoPojectBasePathToken { get; set; }
    public string OriginalDtoProjectAdditionalPathToken { get; set; }

    /// <summary>
    ///     Gets or sets the property info
    /// </summary>
    public ICollection<PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo>();
}