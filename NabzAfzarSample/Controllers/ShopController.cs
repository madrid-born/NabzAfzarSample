using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Models.Cart;

namespace NabzAfzarSample.Controllers;

public class ShopController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await db.Products
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .ToListAsync();

        // Cart (already done)
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new List<CartItem>();
        ViewBag.Cart = cart;

        // Favorites (NEW)
        var favoriteIds = new HashSet<int>();

        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = userManager.GetUserId(User); // no need to query full user
            favoriteIds = (await db.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => f.ProductId)
                    .ToListAsync())
                .ToHashSet();
        }

        ViewBag.FavoriteIds = favoriteIds;

        return View(products);
    }

}