using ProductService.Host.Features.Category.Get;

namespace ProductService.Host.Features.Category.GetAll;

public sealed record GetAllCategoriesResponse(IReadOnlyCollection<GetCategoryResponse> Categories);