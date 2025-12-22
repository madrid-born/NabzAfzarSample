using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Data.Seed;

public static class ShopSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await db.Categories.AnyAsync()) return;

        var cat1 = new Category { Name = "Electronics", Description = "Phones, accessories, gadgets" };
        var cat2 = new Category { Name = "Home", Description = "Home and kitchen products" };
        var cat3 = new Category { Name = "Books", Description = "Books and learning" };

        db.Categories.AddRange(cat1, cat2, cat3);
        await db.SaveChangesAsync();

        db.Products.AddRange(
            new Product { Name = "Wireless Mouse", CategoryId = cat1.Id, Price = 12.50m, StockQuantity = 20, IsActive = true },
            new Product { Name = "USB-C Cable", CategoryId = cat1.Id, Price = 6.00m, StockQuantity = 5, IsActive = true },
            new Product { Name = "Coffee Mug", CategoryId = cat2.Id, Price = 8.75m, StockQuantity = 15, IsActive = true },
            new Product { Name = "C# Clean Architecture", CategoryId = cat3.Id, Price = 25.00m, StockQuantity = 7, IsActive = true }
        );

        await db.SaveChangesAsync();
    }
}