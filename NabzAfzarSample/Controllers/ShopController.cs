using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;

namespace NabzAfzarSample.Controllers;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _db;
    public ShopController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var products = await _db.Products
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .ToListAsync();

        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? [];

        ViewBag.Cart = cart;

        return View(products);
    }

}