using System.ComponentModel.DataAnnotations;

namespace RestfulOrderAPI.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public string? Email { get; set; }
}