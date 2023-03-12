namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

/// <summary>
///     Generated File Information object
///     <remarks>
///         <list type="bullet">
///             <item>The data stored in this object describes the generated file.</item>
///             <item>
///                 As of now only c-sharp code is generated. Despite the fact that a c-sharp
///                 file may contain multiple classes we assume that one file one class.
///             </item>
///             <item>
///                 This object is not used in code generation. It serves only the purpose of collecting the necessary
///                 information, storing them during preprocessing
///             </item>
///         </list>
///     </remarks>
/// </summary>
public class TypeInfo
{
    /// <summary>
    ///     Gets or sets the original type name property.
    ///     <remarks>
    ///         The original type name value comes directly from the yaml file and may contains postfix
    ///         like "Dto".
    ///     </remarks>
    /// </summary>
    public string? OriginalTypename { get; set; }

    /// <summary>
    ///     Gets or set the original base namespace token.
    ///     <remarks>
    ///         The original value comes from the configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalBaseNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto namespace token
    ///     <remarks>
    ///         This value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the original type name token.
    ///     <remarks>
    ///         It comes from the yaml file without any changes.
    ///     </remarks>
    /// </summary>
    public string? OriginalTypeNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the reference type name.
    /// </summary>
    public string? TypeName { get; set; }

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
    public string? TypenameAsVariableName { get; set; }

    /// <summary>
    ///     Gets or sets the filename
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    ///     Gets or sets the namespace
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    ///     Gets or sets the target directory
    ///     <remarks>
    ///         The provided path is absolute path.
    ///     </remarks>
    /// </summary>
    public string? AbsoluteTargetPath { get; set; }

    /// <summary>
    ///     Gets or sets the original target directory token value
    ///     <remarks>
    ///         The value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalTargetDirectoryToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto project base path token
    ///     <remarks>
    ///         This value comes from the configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoProjectBasePathToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto project additional path token
    ///     <remarks>
    ///         This value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoProjectAdditionalPathToken { get; set; }

    /// <summary>
    ///     Gets or sets the property info
    /// </summary>
    public ICollection<VariableInfo> VariableInfos { get; set; } = new List<VariableInfo>();

    /// <summary>
    ///     Gets or sets the target path with file name value
    /// </summary>
    public string? TargetPathWithFileName { get; set; }

    /// <summary>
    ///     Gets or sets the used template absolute path with file name
    /// </summary>
    public string? TemplateAbsolutePathWithFileName { get; set; }

    /// <summary>
    ///     Gets or sets the list of required properties
    /// </summary>
    public List<string> RequiredProperties { get; set; } = new List<string>();
}