using FluentValidation;
using ProductService.Host.Dto;

namespace ProductService.Host.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(x => "Наименование товара является обязательным");
    }
}