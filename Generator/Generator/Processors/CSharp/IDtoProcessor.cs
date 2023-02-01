namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public interface IDtoProcessor
{
    /// <summary>
    ///     Creates C# valid typename using the provided type name in the
    ///     OpenApi yaml file.
    ///     <remarks>
    ///         The provided type is the OriginalTypeNameToken,
    ///         while the processed typename is the Typename property in the
    ///         <see cref="TypeInfo" /> object.
    ///     </remarks>
    /// </summary>
    /// <param name="dtoFileInfos"></param>
    /// <param name="typeNamePostfix"></param>
    void ProcessTypename(List<TypeInfo> dtoFileInfos, string typeNamePostfix);

    /// <summary>
    ///     Creates valid C# filename for the Dto class.
    ///     <remarks>
    ///         The values are coming from the OpenApi yaml file and from configuration
    ///         file.
    ///         <para>
    ///             <list type="bullet">
    ///                 <item>The filename first char will be uppercase</item>
    ///                 <item>The filename postfix first char will be uppercase</item>
    ///                 <item>The file type will be lowercase</item>
    ///             </list>
    ///         </para>
    ///     </remarks>
    /// </summary>
    /// <param name="fileInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="filenamePostfix">Filename postfix</param>
    /// <param name="fileType">File type</param>
    /// <exception cref="ArgumentException">
    ///     When File type is not provided
    /// </exception>
    void ProcessFilename(List<TypeInfo> fileInfos, string filenamePostfix, string fileType);

    /// <summary>
    ///     Takes the namespace information and creates valid C# namespace names.
    /// </summary>
    /// <param name="dtoFileInfos">File infos</param>
    void ProcessDtoNamespace(List<TypeInfo> dtoFileInfos);

    /// <summary>
    ///     Takes the property name information and makes a valid C# property
    ///     name from it.
    /// </summary>
    /// <param name="dtoFileInfos">file infos</param>
    /// <param name="reservedWords">reserved words list</param>
    void ProcessPropertyNames(List<TypeInfo> dtoFileInfos, List<string> reservedWords);

    void ProcessPropertyTypeNames(
        List<TypeInfo> dtoFileInfos,
        List<string> reservedWords,
        List<string> valueTypes);

    /// <summary>
    ///     Takes the property type information and determines if the type is nullable
    /// </summary>
    /// <param name="dtoFileInfos">list of <see cref="TypeInfo" /></param>
    void ProcessNullablePropertyTypes(List<TypeInfo> dtoFileInfos);

    /// <summary>
    ///     Takes the available path related configuration values and create valid absolute path
    /// </summary>
    /// <param name="dtoFileInfos">list of dto file info</param>
    void ProcessTargetPath(List<TypeInfo> dtoFileInfos);

    /// <summary>
    ///     Concatenates the prepared absolute path with the provided filename.
    /// </summary>
    /// <param name="dtoFileInfos">list of dto file infos</param>
    void ProcessPathWithFileName(List<TypeInfo> dtoFileInfos);

    /// <summary>
    ///     Adds template path information to the file information objects
    /// </summary>
    /// <param name="dtoFileInfos">list of <see cref="TypeInfo" /></param>
    /// <param name="dtoTemplatePath">path to template used to generate the file</param>
    void ProcessDtoTemplatePath(List<TypeInfo> dtoFileInfos, string dtoTemplatePath);

    /// <summary>
    ///     Checks if the property name is a reserved word
    /// </summary>
    /// <param name="dtoFileInfos">list of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">list of reserved words</param>
    void CheckIfPropertyNameIsReservedWord(List<TypeInfo> dtoFileInfos, List<string> reservedWords);

    /// <summary>
    ///     Determines the C# type for given OpenApi type using the provided map
    /// </summary>
    /// <param name="dtoFileInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiCsharpTypeMap">C# map of types</param>
    void ProcessOpenApiTypesToCsharpTypes(List<TypeInfo> dtoFileInfos, Dictionary<string, string> openApiCsharpTypeMap);
}