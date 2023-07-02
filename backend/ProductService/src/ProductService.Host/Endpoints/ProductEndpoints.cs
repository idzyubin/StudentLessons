using ProductService.Host.Handlers;

namespace ProductService.Host.Endpoints;

/// <summary>
///     Эндпоинты для взаимодействия с сервисом товаров
/// </summary>
public static class ProductEndpoints
{
    public static WebApplication AddProductEndpoints(this WebApplication application)
    {
        var group = application.MapGroup("/api/product").WithOpenApi();

        group.MapGet("/", () => { })
            .WithSummary("Данный метод предназначен для получения всех товаров");

        group.MapGet("/{id:long}", () => { })
            .WithSummary("Данный метод предназначен для получения товара по указанному идентификатору");
        
        group.MapPost("/", CreateProductHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для добавления нового товара");
        
        group.MapPut("/{id:long}", () => { })
            .WithSummary("Данный метод предназначен для обновления данных товара по указанному идентификатору");
        
        group.MapDelete("/{id:long}", () => { })
            .WithSummary("Данный метод предназначен для удаления товара по указанному идентификатору");

        return application;
    }
}