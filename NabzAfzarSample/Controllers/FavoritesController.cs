using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Models.Favorites;

namespace NabzAfzarSample.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public FavoritesController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        var favorites = await _db.Favorites
            .Include(f => f.Product)
            .Where(f => f.UserId == user!.Id)
            .ToListAsync();

        return View(favorites);
    }

    public async Task<IActionResult> Toggle(int productId, string? returnUrl = null)
    {
        var user = await _userManager.GetUserAsync(User);

        var fav = await _db.Favorites
            .FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == user!.Id);

        if (fav == null)
        {
            _db.Favorites.Add(new Favorite
            {
                ProductId = productId,
                UserId = user.Id
            });
        }
        else
        {
            _db.Favorites.Remove(fav);
        }

        await _db.SaveChangesAsync();

        return Redirect(returnUrl ?? Url.Action("Index", "Shop")!);
    }
}