using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Domain.Interfaces;

namespace ProductService.Host.Features.Category.Delete;

public static class DeleteCategoryHandler
{
    public static async ValueTask<Results<Ok<DeleteCategoryResponse>, NotFound, BadRequest<List<ValidationFailure>>>> HandleAsync(
        DeleteCategoryRequest request,
        IValidator<DeleteCategoryRequest> validator,
        IBaseManager<Domain.Entities.Category> categoryManager,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }
        
        var result = await categoryManager.DeleteAsync(request.Id, cancellationToken);
        if (result is null)
        {
            return TypedResults.NotFound();
        }
        
        var response = MapEntityToResponse(result);
        
        return TypedResults.Ok(response);
    }

    private static DeleteCategoryResponse MapEntityToResponse(Domain.Entities.Category product) => new(
        product.Id,
        product.Title);
}