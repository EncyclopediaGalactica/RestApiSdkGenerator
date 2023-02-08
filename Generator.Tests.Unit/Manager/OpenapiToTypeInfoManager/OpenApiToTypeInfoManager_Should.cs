namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers;
using TestBase;

[ExcludeFromCodeCoverage]
public partial class OpenApiToTypeInfoManager_Should : TestBase
{
    private readonly IOpenApiToTypeInfoManager _sut;

    public OpenApiToTypeInfoManager_Should()
    {
        _sut = new OpenApiToTypeInfoManager();
    }
}