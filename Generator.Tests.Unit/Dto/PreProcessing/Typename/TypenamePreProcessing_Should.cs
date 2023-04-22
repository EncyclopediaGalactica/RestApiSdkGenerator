// namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Typename;
//
// using System.Diagnostics.CodeAnalysis;
// using FluentAssertions;
// using Generator;
// using Xunit;
//
// [ExcludeFromCodeCoverage]
// [SuppressMessage("ReSharper", "InconsistentNaming")]
// public class TypenamePreProcessing_Should : TestBase
// {
//     [Fact]
//     public void PreProcess_FileName()
//     {
//         // Arrange && Act
//         string currentPath = $"{_basePath}/Dto/PreProcessing/Typename";
//         string configFilePath = $"{currentPath}/config.json";
//         CodeGenerator? codeGenerator = null;
//         Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };
//
//         // Assert
//         action.Should().NotThrow();
//         codeGenerator.Should().NotBeNull();
//         codeGenerator.DtoFileInfos.Should().NotBeEmpty();
//         codeGenerator.DtoFileInfos.Where(p => p.Filename == "SimpleDto").ToList().Count.Should().Be(1);
//         codeGenerator.DtoFileInfos.Where(p => p.Filename == "SomeComplexityDto").ToList().Count.Should().Be(1);
//         codeGenerator.DtoFileInfos.Where(p => p.Filename == "ManyComplexityInTheNameDto").ToList().Count.Should().Be(1);
//     }
// }

