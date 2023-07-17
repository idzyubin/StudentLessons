using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Domain.Interfaces;

namespace ProductService.Host.Features.Category.Get;

public static class GetCategoryHandler
{
    public static async ValueTask<Results<Ok<GetCategoryResponse>, NotFound, BadRequest<List<ValidationFailure>>>> HandleAsync(
        GetCategoryRequest request,
        IValidator<GetCategoryRequest> validator,
        IBaseManager<Domain.Entities.Category> categoryManager,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }
        
        var result = await categoryManager.GetAsync(request.Id, cancellationToken);
        if (result is null)
        {
            return TypedResults.NotFound();
        }
        
        var response = MapEntityToResponse(result);

        return TypedResults.Ok(response);
    }
    
    private static GetCategoryResponse MapEntityToResponse(Domain.Entities.Category product) => new(
        product.Id,
        product.Title);
}