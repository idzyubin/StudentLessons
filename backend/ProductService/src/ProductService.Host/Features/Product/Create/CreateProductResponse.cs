namespace ProductService.Host.Features.Product.Create;

public sealed record CreateProductResponse(long Id, string Title, string Description, decimal? Price, decimal Quantity, IReadOnlyCollection<long> CategoryIds);