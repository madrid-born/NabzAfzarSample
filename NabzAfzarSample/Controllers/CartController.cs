using Microsoft.AspNetCore.Mvc;
using NabzAfzarSample.Data;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;

namespace NabzAfzarSample.Controllers;

public class CartController : Controller
{
    private const string CartKey = "CART";
    private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db) => _db = db;

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();
        return View(cart);
    }

    public async Task<IActionResult> Increase(int productId, string? returnUrl = null)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return NotFound();

        if (product.StockQuantity <= 0)
        {
            TempData["Toast"] = "This product is out of stock.";
            return Redirect(returnUrl ?? Url.Action("Index", "Shop")!);
        }

        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();

        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item == null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });
        }
        else
        {
            if (item.Quantity >= product.StockQuantity)
            {
                TempData["Toast"] = "You reached the maximum available stock for this product.";
                return Redirect(returnUrl ?? Url.Action("Index", "Shop")!);
            }
            item.Quantity++;
        }

        HttpContext.Session.SetObject(CartKey, cart);

        return Redirect(returnUrl ?? Url.Action("Index", "Shop")!);
    }

    public IActionResult Decrease(int productId, string? returnUrl = null)
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();

        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Quantity--;
            if (item.Quantity <= 0)
                cart.Remove(item);
        }

        HttpContext.Session.SetObject(CartKey, cart);

        return Redirect(returnUrl ?? Url.Action("Index", "Shop")!);
    }


}