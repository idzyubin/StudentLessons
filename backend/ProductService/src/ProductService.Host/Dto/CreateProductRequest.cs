namespace ProductService.Host.Dto;

public record CreateProductRequest(string Title, string Description, decimal? Price, decimal Quantity, IReadOnlyCollection<long> CategoryIds);