using Microsoft.EntityFrameworkCore;
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Data;

public class OrderContext : DbContext
{
    // public DbSet<Order> Orders { get; set; }
    // public string DbPath { get; }
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        // Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        // string path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "RestfulOrderAPI.db");
    }

    public DbSet<Order> Orders => Set<Order>();

    // protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}