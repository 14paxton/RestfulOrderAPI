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

            List<string> emailList = ["M14paxton@gmail.com", "mike@mailinator.com", "frank@aol.com"];
            IEnumerable<Customer> customers = emailList.Select(email => new Customer(email));
            IEnumerable<Order> orders = customers.Select(customer => new Order(customer));

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}