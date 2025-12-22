using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;
using NabzAfzarSample.ViewModels.Shop;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public ShopController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string? q, int? categoryId, string sort = "new", int page = 1)
    {
        const int pageSize = 9;

        var query = _db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Name.Contains(q));

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var totalCount = await query.CountAsync();

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var categories = await _db.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync();

        // Cart + Favorites for the +/- and fav toggle UI
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        ViewBag.Cart = cart;

        var favoriteIds = new HashSet<int>();
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = _userManager.GetUserId(User);
            favoriteIds = (await _db.Favorites
                    .AsNoTracking()
                    .Where(f => f.UserId == userId)
                    .Select(f => f.ProductId)
                    .ToListAsync())
                .ToHashSet();
        }
        ViewBag.FavoriteIds = favoriteIds;

        var vm = new ShopIndexVm
        {
            Products = products,
            Categories = categories,
            Q = q,
            CategoryId = categoryId,
            Sort = sort,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return View(vm);
    }
}
