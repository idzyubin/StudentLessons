using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

/// <summary>
///     Базовый класс для сущностей Базы Данных. 
/// </summary>
public abstract class Entity
{
    /// <summary>
    ///     Идентификатор сущности
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    ///     Признак удаления сущности
    /// </summary>
    public bool IsDeleted { get; set; }
}