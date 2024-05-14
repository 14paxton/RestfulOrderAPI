namespace CSharp.Services;

public static class OrderService
{
    static List<Order> Orders { get; }
    static OrderService()
    {
        Orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), Email = "14paxton@gmail.com"},
            new Order { Id = Guid.NewGuid(), Email = "146paxton@gmail.com"},
            new Order { Id = Guid.NewGuid(), Email = "146paxton@gmail.com"},
        };
    }

    public static List<Order> GetAll() => Orders;

    public static Order? Get(Guid id) => Orders.FirstOrDefault(p => p.Id == id);

    public static void Add(Order order)
    {
        order.Id = Guid.NewGuid();
        Orders.Add(order);
    }

    public static void Delete(Guid id)
    {
        Order? order = Get(id);
        if(order is null)
            return;

        Orders.Remove(order);
    }

    public static void Update(Order order)
    {
        int index = Orders.FindIndex(p => p.Id == order.Id);
        if(index == -1)
            return;

        Orders[index] = order;
    }
}