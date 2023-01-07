namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

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
    }
}