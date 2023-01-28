namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Configuration;

using FluentValidation;

public class CodeGeneratorConfigurationValidator : AbstractValidator<CodeGeneratorConfiguration>
{
    public CodeGeneratorConfigurationValidator()
    {
        RuleFor(p => p.OpenApiSpecificationPath).NotNull().NotEmpty()
            .WithMessage("OpenApi specification path must be defined.");

        RuleFor(p => p.TargetDirectory).NotNull().NotEmpty()
            .WithMessage("Target directory path must be specified.");

        RuleFor(p => p.SolutionBaseNamespace).NotNull().NotEmpty()
            .WithMessage("Base namespace must be defined.");

        RuleFor(p => p.Lang).NotNull().NotEmpty().NotEqual(string.Empty)
            .WithMessage("Lang must be defined");

        When(p => string.IsNullOrEmpty(p.DtoProjectBasePath) == false
                  && string.IsNullOrWhiteSpace(p.DtoProjectBasePath) == false,
            () =>
            {
                RuleFor(p => p.DtoProjectBasePath[0].ToString() != "/").Equal(true)
                    .WithMessage("Dto project base path url must not be absolute path. It has to be relative path.");
            });

        When(p => string.IsNullOrEmpty(p.DtoProjectAdditionalPath) == false
                  && string.IsNullOrWhiteSpace(p.DtoProjectAdditionalPath) == false,
            () =>
            {
                RuleFor(p => p.DtoProjectAdditionalPath[0].ToString() != "/").Equal(true)
                    .WithMessage("Dto project additional path must not be absolute path. It must be relative path.");
            });
    }
}