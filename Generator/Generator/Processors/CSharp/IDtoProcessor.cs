namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Models;

public interface IDtoProcessor
{
    void ProcessDtoTypeName(List<FileInfo> dtoFileInfos, string typeNamePostfix);
    void ProcessDtoFileNames(List<FileInfo> dtoFileInfos, string dtoFileNamePostFix, string fileType);
    void ProcessDtoNamespace(List<FileInfo> dtoFileInfos);
    void ProcessPropertyNames(List<FileInfo> dtoFileInfos);

    void ProcessPropertyTypeNames(
        List<FileInfo> dtoFileInfos,
        List<string> reservedWords,
        List<string> valueTypes);

    void ProcessTargetPath(List<FileInfo> dtoFileInfos);
    void ProcessPathWithFileName(List<FileInfo> dtoFileInfos);
    void ProcessDtoTemplatePath(List<FileInfo> dtoFileInfos, string dtoTemplatePath);
    void CheckIfPropertyNameIsReservedWord(List<FileInfo> dtoFileInfos, List<string> reservedWords);
}