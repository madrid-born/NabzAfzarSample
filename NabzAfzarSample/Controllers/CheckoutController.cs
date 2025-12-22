using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        var user = await _userManager.GetUserAsync(User);

        var order = new Order
        {
            UserId = user!.Id,
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

        HttpContext.Session.Remove(CartKey);

        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success() => View();
}