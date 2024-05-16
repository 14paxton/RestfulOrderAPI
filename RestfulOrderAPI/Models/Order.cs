using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.DateTime;
using static System.Guid;


namespace RestfulOrderAPI.Models;

public class Order
{
    public Order()
    {
    }

    public Order(Guid customerId, Customer customer)
    {
        CustomerId = customerId;
        Customer = customer;
    }

    public Order(Customer customer)
    {
        CustomerId = customer.Id;
        Customer = customer;
    }

    [Key] public Guid Id { get; set; } = NewGuid();

    public DateTime CreatedDate { get; set; } = Now;

    [Required] [ForeignKey("CustomerId")] public Guid CustomerId { get; set; }

    [JsonIgnore] public Customer Customer { get; set; }
}