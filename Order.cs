using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulOrderAPI;

public class Order
{
    [Required]
    public Guid OrderId { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public string? Email { get; set; }
}