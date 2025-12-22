using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;
using NabzAfzarSample.Models.Orders;

namespace NabzAfzarSample.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private const string CartKey = "CART";
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public CheckoutController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey);
        if (cart == null || !cart.Any())
            return RedirectToAction("Index", "Shop");

        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey);
        if (cart == null || !cart.Any())
            return RedirectToAction("Index", "Shop");

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction("Index", "Shop");

        var strategy = _db.Database.CreateExecutionStrategy();

        try
        {
            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _db.Database.BeginTransactionAsync();

                var productIds = cart.Select(x => x.ProductId).Distinct().ToList();

                var products = await _db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id);

                // Validate stock & availability
                foreach (var item in cart)
                {
                    if (!products.TryGetValue(item.ProductId, out var product))
                        throw new InvalidOperationException($"Product not found (ID={item.ProductId}).");

                    if (!product.IsActive)
                        throw new InvalidOperationException($"'{product.Name}' is not available anymore.");

                    if (product.StockQuantity < item.Quantity)
                        throw new InvalidOperationException(
                            $"Not enough stock for '{product.Name}'. Available: {product.StockQuantity}.");
                }

                // Decrease stock
                foreach (var item in cart)
                {
                    products[item.ProductId].StockQuantity -= item.Quantity;
                }

                // Create order
                var order = new Order
                {
                    UserId = userId,
                    TotalAmount = cart.Sum(x => x.Total),
                    Items = cart.Select(x => new OrderItem
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        UnitPrice = x.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };

                _db.Orders.Add(order);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
            });

            HttpContext.Session.Remove(CartKey);
            return RedirectToAction(nameof(Success));
        }
        catch (InvalidOperationException ex)
        {
            // Business rule errors (stock not enough, product inactive, etc.)
            TempData["Toast"] = ex.Message;
            return RedirectToAction("Index", "Cart");
        }
    }

    public IActionResult Success() => View();
}