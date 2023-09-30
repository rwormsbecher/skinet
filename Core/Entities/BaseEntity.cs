using System.ComponentModel.DataAnnotations;

namespace Core;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
