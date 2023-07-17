using FluentValidation;

namespace ProductService.Host.Features.Product.Create;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(x => "Наименование товара является обязательным");
    }
}