using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Host.Dto;

namespace ProductService.Host.Handlers;

/// <summary>
///     Обработчик добавления товара
/// </summary>
public static class CreateProductHandler
{
    public static async ValueTask<Results<Ok<CreateProductResponse>, BadRequest<List<ValidationFailure>>>> HandleAsync(
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
        IProductManager productManager,
        ICategoryManager categoryManager,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }
        
        var product = new Product
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            Categories = (await categoryManager.GetAsync(x => request.CategoryIds.Contains(x.Id), cancellationToken)).ToList()
        };
        
        var result = await productManager.CreateAsync(product, cancellationToken);

        var response = new CreateProductResponse(
            result.Id,
            result.Title,
            result.Description,
            result.Price,
            result.Quantity,
            result.Categories.Select(x => x.Id).ToList());
        
        return TypedResults.Ok(response);
    }
}