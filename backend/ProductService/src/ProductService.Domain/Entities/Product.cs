using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

/// <summary>
///     Сущность товара
/// </summary>
public class Product
{
    /// <summary>
    ///     Идентификатор товара
    /// </summary>
    [Key]
    public long Id { get; set; }

    /// <summary>
    ///     Наименование товара
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    ///     Описание товара
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    ///     Стоимость товара
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    ///     Кол-во товара
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    ///     Категории к которым принадлежит товар
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}