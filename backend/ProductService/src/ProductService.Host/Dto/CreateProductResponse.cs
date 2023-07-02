namespace ProductService.Host.Dto;

public record CreateProductResponse(long Id, string Title, string Description, decimal? Price, decimal Quantity, IReadOnlyCollection<long> CategoryIds);