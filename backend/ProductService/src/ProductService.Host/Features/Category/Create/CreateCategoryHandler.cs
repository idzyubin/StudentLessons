using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Domain.Interfaces;

namespace ProductService.Host.Features.Category.Create;

public static class CreateCategoryHandler
{
    public static async ValueTask<Results<Ok<CreateCategoryResponse>, BadRequest<List<ValidationFailure>>>> HandleAsync(
        CreateCategoryRequest request,
        IValidator<CreateCategoryRequest> validator,
        IBaseManager<Domain.Entities.Category> categoryManager,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }
        
        var entity = MapRequestToEntity(request);
        var result = await categoryManager.CreateAsync(entity, cancellationToken);
        var response = MapEntityToResponse(result);
        
        return TypedResults.Ok(response);
    }
    
    private static Domain.Entities.Category MapRequestToEntity(CreateCategoryRequest request) => new()
    {
        Title = request.Title
    };

    private static CreateCategoryResponse MapEntityToResponse(Domain.Entities.Category product) => new(
        product.Id,
        product.Title);
}