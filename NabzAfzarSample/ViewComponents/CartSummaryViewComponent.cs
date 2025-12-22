using Microsoft.AspNetCore.Mvc;
using NabzAfzarSample.Extensions;
using NabzAfzarSample.Models.Cart;

namespace NabzAfzarSample.ViewComponents;

public class CartSummaryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("CART") ?? new();
        var count = cart.Sum(x => x.Quantity);
        return View(count);
    }
}