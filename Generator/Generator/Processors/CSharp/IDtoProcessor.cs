namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

public interface IDtoProcessor
{
    void ProcessDtoTypeName(List<FileInfo> dtoFileInfos, string typeNamePostfix);
    void ProcessDtoFileNames(List<FileInfo> dtoFileInfos, string dtoFileNamePostFix, string fileType);
    void ProcessDtoNamespace(List<FileInfo> dtoFileInfos);
    void ProcessPropertyNames(List<FileInfo> dtoFileInfos, List<string> reservedWords);

    void ProcessPropertyTypeNames(
        List<FileInfo> dtoFileInfos,
        List<string> reservedWords,
        List<string> valueTypes);
}