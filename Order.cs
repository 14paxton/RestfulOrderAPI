using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp;

public class Order
{
    public Guid Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Date { get; set; }

    public string? Email { get; set; }
}