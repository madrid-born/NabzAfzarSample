using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NabzAfzarSample.Data;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoriesController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var items = await _db.Categories
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
        return View(items);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category model)
    {
        if (!ModelState.IsValid) return View(model);

        _db.Categories.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Categories.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        _db.Categories.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var item = await _db.Categories.FindAsync(id);
        if (item == null) return NotFound();

        item.IsActive = !item.IsActive;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}