namespace RestfulOrderAPI.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            OrderContext context = services.GetRequiredService<OrderContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
    }
}