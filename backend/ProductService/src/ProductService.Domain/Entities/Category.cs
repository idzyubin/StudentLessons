using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

/// <summary>
///     Сущность категории
/// </summary>
public sealed class Category : Entity
{
    /// <summary>
    ///     Наименование категории
    /// </summary>
    public string Title { get; set; } = "";
}