namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Configuration;

using Newtonsoft.Json;

public class CodeGeneratorConfiguration
{
    /// <summary>
    ///     Gets or sets the generated programming language name
    /// </summary>
    [JsonProperty("lang")]
    public string Lang { get; set; }

    [JsonProperty("openapi_specification_path")]
    public string OpenApiSpecificationPath { get; set; }

    [JsonProperty("target_directory")]
    public string TargetDirectory { get; set; }

    [JsonProperty("solution_name")]
    public string SolutionName { get; set; }

    /// <summary>
    ///     Gets or sets the solution base name space.
    ///     <remarks>
    ///         During generation this value is the base.
    ///         If any other subproject namespace is given they will be concatenated
    ///     </remarks>
    /// </summary>
    [JsonProperty("solution_base_namespace")]
    public string SolutionBaseNamespace { get; set; }

    [JsonProperty("dto_project_name")]
    public string DtoProjectName { get; set; }

    /// <summary>
    ///     Sets or gets the DtoProjectBasePath property
    ///     <remarks>
    ///         This directory is the base directory for the whole Dto project
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_project_base_path")]
    public string? DtoProjectBasePath { get; set; }

    /// <summary>
    ///     Sets or gets the DtoProjectAdditionalPath property
    ///     <remarks>
    ///         This path segment will be added to the <see cref="DtoProjectBasePath" />, so that
    ///         for this exact generation a separate path can be defined.
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_project_additional_path")]
    public string? DtoProjectAdditionalPath { get; set; }

    /// <summary>
    ///     Gets or sets dto project namespace
    ///     <remarks>
    ///         This value will be concatenated to solution base namespace
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_project_namespace")]
    public string? DtoProjectNameSpace { get; set; }

    /// <summary>
    ///     Gets or sets the dto test project name
    /// </summary>
    [JsonProperty("dto_test_project_name")]
    public string? DtoTestProjectName { get; set; }

    /// <summary>
    ///     Gets or sets the dto test project base path
    /// </summary>
    [JsonProperty("dto_test_project_base_path")]
    public string? DtoTestProjectBasePath { get; set; }

    /// <summary>
    ///     Gets or sets the dto test project additional path
    ///     <remarks>
    ///         The role of the additional path here is to define the path where the
    ///         generated dto test files will be placed within the dto test project.
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_test_project_additional_path")]
    public string? DtoTestProjectAdditionalPath { get; set; }

    /// <summary>
    ///     Gets or sets dto test project namespace
    ///     <remarks>
    ///         This value will be concatenated with solution base namespace
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_test_project_namespace")]
    public string? DtoTestProjectNameSpace { get; set; }

    /// <summary>
    ///     Gets or sets skip dto preprocess value
    ///     <remarks>
    ///         This value controls if DTO preprocess phase is skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_preprocess")]
    public bool SkipDtoPreProcess { get; set; }

    /// <summary>
    ///     Gets or sets skip dto generating value
    ///     <remarks>
    ///         This value controls if DTO generating phase will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_generating")]
    public bool SkipDtoGenerating { get; set; }

    /// <summary>
    ///     Gets or sets skip dto test preprocess value
    ///     <remarks>
    ///         This value controls if DTO test preprocess will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_test_preprocess")]
    public bool SkipDtoTestPreProcess { get; set; }

    /// <summary>
    ///     Gets or sets skip dto test generating value
    ///     <remarks>
    ///         This value controls if Dto test generating phase will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_test_generating")]
    public bool SkipDtoTestGenerating { get; set; }
}