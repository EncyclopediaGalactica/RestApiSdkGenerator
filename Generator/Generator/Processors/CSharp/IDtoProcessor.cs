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
    ///         <see cref="FileInfo" /> object.
    ///     </remarks>
    /// </summary>
    /// <param name="dtoFileInfos"></param>
    /// <param name="typeNamePostfix"></param>
    void ProcessTypename(List<FileInfo> dtoFileInfos, string typeNamePostfix);

    void ProcessFilename(List<FileInfo> fileInfos, string filenamePostfix, string fileType);
    void ProcessDtoNamespace(List<FileInfo> dtoFileInfos);
    void ProcessPropertyNames(List<FileInfo> dtoFileInfos);

    void ProcessPropertyTypeNames(
        List<FileInfo> dtoFileInfos,
        List<string> reservedWords,
        List<string> valueTypes);

    void ProcessNullablePropertyTypes(List<FileInfo> dtoFileInfos);

    /// <summary>
    ///     Converts the OriginalTargetPathToken value from configuration file to an absolute path value.
    /// </summary>
    /// <param name="dtoFileInfos">list of dto file info</param>
    void ProcessTargetPath(List<FileInfo> dtoFileInfos);

    void ProcessPathWithFileName(List<FileInfo> dtoFileInfos);
    void ProcessDtoTemplatePath(List<FileInfo> dtoFileInfos, string dtoTemplatePath);
    void CheckIfPropertyNameIsReservedWord(List<FileInfo> dtoFileInfos, List<string> reservedWords);
    void ProcessOpenApiTypesToCsharpTypes(List<FileInfo> dtoFileInfos, Dictionary<string, string> openApiCsharpTypeMap);
}