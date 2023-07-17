using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Domain.Interfaces;

namespace ProductService.Host.Features.Product.Create;

/// <summary>
///     Обработчик добавления товара
/// </summary>
public static class CreateProductHandler
{
    public static async ValueTask<Results<Ok<CreateProductResponse>, BadRequest<List<ValidationFailure>>>> HandleAsync(
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
        IBaseManager<Domain.Entities.Product> productManager,
        IBaseManager<Domain.Entities.Category> categoryManager,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }

        var entity = await MapRequestToEntity(request, categoryManager, cancellationToken);
        var result = await productManager.CreateAsync(entity, cancellationToken);
        var response = MapEntityToResponse(result);
        
        return TypedResults.Ok(response);
    }

    private static async ValueTask<Domain.Entities.Product> MapRequestToEntity(CreateProductRequest request, IBaseManager<Domain.Entities.Category> categoryManager, CancellationToken cancellationToken) => new()
    {
        Title = request.Title,
        Description = request.Description,
        Price = request.Price,
        Quantity = request.Quantity,
        Categories = (await categoryManager.GetAsync(x => request.CategoryIds.Contains(x.Id), cancellationToken)).ToList()
    };
    
    private static CreateProductResponse MapEntityToResponse(Domain.Entities.Product product) => new(
        product.Id,
        product.Title,
        product.Description,
        product.Price,
        product.Quantity,
        product.Categories.Select(x => x.Id).ToList());
}