using ProductService.Host.Features.Product.Delete;
using ProductService.Host.Features.Product.Get;
using ProductService.Host.Features.Product.GetAll;
using ProductService.Host.Features.Product.Restore;
using ProductService.Host.Features.Product.Update;
using ProductService.Host.Features.Product.Create;

namespace ProductService.Host.Endpoints;

/// <summary>
///     Эндпоинты для взаимодействия с товарами
/// </summary>
public static class ProductEndpoints
{
    public static WebApplication AddProductEndpoints(this WebApplication application)
    {
        var group = application.MapGroup("/api/product").WithOpenApi();

        group.MapGet("/", GetAllProductsRequest.HandleAsync)
            .WithSummary("Данный метод предназначен для получения всех товаров");

        group.MapGet("/{id:long}", GetProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для получения товара по указанному идентификатору");
        
        group.MapPost("/", CreateProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для добавления нового товара");
        
        group.MapPut("/{id:long}", UpdateProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для обновления данных товара по указанному идентификатору");
        
        group.MapPut("/{id:long}", RestoreProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для обновления данных товара по указанному идентификатору");

        group.MapDelete("/{id:long}", DeleteProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для удаления товара по указанному идентификатору");

        return application;
    }
}