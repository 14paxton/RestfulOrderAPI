using System.ComponentModel.DataAnnotations;
using static System.DateTime;
using static System.Guid;

namespace RestfulOrderAPI.Models;

public class Customer(string email)
{
    [Key] public Guid Id { get; set; } = NewGuid();

    public DateTime CreatedDate { get; set; } = Now;

    [Required]
    [DataType(DataType.EmailAddress)]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Email needs to be between 5 and 50 characters.")]
    public string Email { get; set; } = email;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}