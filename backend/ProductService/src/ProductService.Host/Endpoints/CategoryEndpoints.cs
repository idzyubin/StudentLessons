using ProductService.Host.Features.Category.Create;
using ProductService.Host.Features.Category.Delete;
using ProductService.Host.Features.Category.Get;
using ProductService.Host.Features.Category.GetAll;
using ProductService.Host.Features.Category.Restore;
using ProductService.Host.Features.Category.Update;

namespace ProductService.Host.Endpoints;

/// <summary>
///     Эндпоинты для взаимодействия с категориями товаров
/// </summary>
public static class CategoryEndpoints
{
    public static WebApplication AddCategoryEndpoints(this WebApplication application)
    {
        var group = application.MapGroup("/api/category").WithOpenApi();

        group.MapGet("/", GetAllCategoriesHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для получения всех товаров");

        group.MapGet("/{id:long}", GetCategoryHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для получения товара по указанному идентификатору");
        
        group.MapPost("/", CreateCategoryHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для добавления нового товара");
        
        group.MapPut("/{id:long}", UpdateCategoryHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для обновления данных товара по указанному идентификатору");
        
        group.MapPut("/{id:long}", RestoreCategoryHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для обновления данных товара по указанному идентификатору");

        group.MapDelete("/{id:long}", DeleteCategoryHandler.HandleAsync)
            .WithSummary("Данный метод предназначен для удаления товара по указанному идентификатору");

        return application;
    }
}