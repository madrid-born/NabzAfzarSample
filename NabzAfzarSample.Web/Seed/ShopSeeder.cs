using System;
using System.Linq;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Seed
{
    public static class ShopSeeder
    {
        public static void Seed()
        {
            using (var db = new AppDbContext())
            {
                var electronics = db.Categories.FirstOrDefault(c => c.Name == "Electronics");
                if (electronics == null)
                {
                    electronics = new Category { Name = "Electronics", Description = "Gadgets and accessories", IsActive = true, CreatedAt = DateTime.UtcNow };
                    db.Categories.Add(electronics);
                }

                var home = db.Categories.FirstOrDefault(c => c.Name == "Home");
                if (home == null)
                {
                    home = new Category { Name = "Home", Description = "Home and kitchen items", IsActive = true, CreatedAt = DateTime.UtcNow };
                    db.Categories.Add(home);
                }

                var books = db.Categories.FirstOrDefault(c => c.Name == "Books");
                if (books == null)
                {
                    books = new Category { Name = "Books", Description = "Books and learning", IsActive = true, CreatedAt = DateTime.UtcNow };
                    db.Categories.Add(books);
                }

                db.SaveChanges();

                void UpsertProduct(
                    string name,
                    string description,
                    decimal price,
                    int stock,
                    int categoryId,
                    string primaryImageUrl)
                {
                    var product = db.Products.FirstOrDefault(p => p.Name == name);

                    if (product == null)
                    {
                        product = new Product
                        {
                            Name = name,
                            Description = description,
                            Price = price,
                            StockQuantity = stock,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            CategoryId = categoryId
                        };

                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                    else
                    {
                        product.Description = description;
                        product.Price = price;
                        product.StockQuantity = stock;
                        product.IsActive = true;
                        product.CategoryId = categoryId;
                        db.SaveChanges();
                    }

                    if (!string.IsNullOrWhiteSpace(primaryImageUrl))
                    {
                        var existingPrimary = db.ProductImages
                            .FirstOrDefault(i => i.ProductId == product.Id && i.IsPrimary);

                        if (existingPrimary == null)
                        {
                            db.ProductImages.Add(new ProductImage
                            {
                                ProductId = product.Id,
                                ImageUrl = primaryImageUrl,
                                IsPrimary = true
                            });
                        }
                        else
                        {
                            existingPrimary.ImageUrl = primaryImageUrl;
                        }

                        db.SaveChanges();
                    }
                }

                UpsertProduct(
                    name: "Wireless Mouse",
                    description: "2.4G wireless mouse",
                    price: 12.5m,
                    stock: 20,
                    categoryId: electronics.Id,
                    primaryImageUrl: "https://www.shutterstock.com/pixelsquid/assets_v2/352/3525632974441682649/previews/G03-260nw.jpg"
                );

                UpsertProduct(
                    name: "USB-C Cable",
                    description: "1 meter USB-C cable",
                    price: 6m,
                    stock: 5,
                    categoryId: electronics.Id,
                    primaryImageUrl: "https://res.cloudinary.com/subtel/image/upload/h_600/q_auto:low,f_auto/bhxcnddrffwzfptcd0u4.webp"
                );

                UpsertProduct(
                    name: "Coffee Mug",
                    description: "Ceramic mug",
                    price: 8.75m,
                    stock: 12,
                    categoryId: home.Id,
                    primaryImageUrl: "https://sashays.co.in/cdn/shop/products/CoffeeHuskMug.jpg?v=1663836440&width=1346"
                );

                UpsertProduct(
                    name: "C# Fundamentals Book",
                    description: "Learn C# basics",
                    price: 25m,
                    stock: 7,
                    categoryId: books.Id,
                    primaryImageUrl: "https://introprogramming.info/wp-content/uploads/2017/08/csharp-book-nakov-en-v2013-cover.jpg"
                );
            }
        }
    }
}
