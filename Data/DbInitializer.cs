
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OrderContext context)
        {
            if (context.Orders.Any())
            {
                return; // DB has been seeded
            }


            Order[] orders = new Order[]
            {
                new Order
                {
                    Email = "M14paxton@gmail.com",
                },
                new Order
                {
                    Email = "mike@mailinator.com",
                },
                new Order
                {
                    Email = "frank@aol.com",
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}