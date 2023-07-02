using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

/// <summary>
///     Сущность категории
/// </summary>
public class Category
{
    /// <summary>
    ///     Идентификатор категории товара
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    ///     Наименование категории
    /// </summary>
    public string Title { get; set; } = "";
}