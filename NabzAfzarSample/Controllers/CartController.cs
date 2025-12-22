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

    public async Task<IActionResult> Increase(int productId)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return NotFound();

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
            item.Quantity++;
        }

        HttpContext.Session.SetObject(CartKey, cart);
        return RedirectToAction("Index", "Cart");
    }

    public IActionResult Decrease(int productId)
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
        return RedirectToAction("Index", "Cart");
    }

}