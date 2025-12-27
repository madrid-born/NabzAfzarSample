using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Cart;
using NabzAfzarSample.Models;

namespace NabzAfzarSample
{
    public partial class Shop : System.Web.UI.Page
    {
        protected Repeater ProductsRepeater;

        private AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _db.Products
                .Where(p => p.IsActive)
                .ToList()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.StockQuantity,
                    ImageUrl = _db.ProductImages
                        .Where(i => i.ProductId == p.Id && i.IsPrimary)
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
                })
                .ToList();

            ProductsRepeater.DataSource = products;
            ProductsRepeater.DataBind();
        }

        private List<CartItem> Cart
        {
            get
            {
                if (Session["CART"] == null)
                    Session["CART"] = new List<CartItem>();

                return (List<CartItem>)Session["CART"];
            }
        }

        protected void IncreaseQty(object sender, CommandEventArgs e)
        {
            int productId = int.Parse(e.CommandArgument.ToString());
            var product = _db.Products.Find(productId);
            if (product == null) return;

            var item = Cart.FirstOrDefault(x => x.ProductId == productId);

            if (item == null)
            {
                if (product.StockQuantity <= 0) return;

                Cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                if (item.Quantity < product.StockQuantity)
                    item.Quantity++;
            }

            LoadProducts();
        }

        protected void DecreaseQty(object sender, CommandEventArgs e)
        {
            int productId = int.Parse(e.CommandArgument.ToString());
            var item = Cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    Cart.Remove(item);
            }

            LoadProducts();
        }

        protected string GetQuantity(object productIdObj)
        {
            int productId = Convert.ToInt32(productIdObj);
            var item = Cart.FirstOrDefault(x => x.ProductId == productId);
            return item?.Quantity.ToString() ?? "0";
        }
        protected string GetFavoriteText(object productIdObj)
        {
            if (!Request.IsAuthenticated)
                return "Login to Favorite";

            int productId = Convert.ToInt32(productIdObj);
            var userId = Context.User.Identity.GetUserId();

            var exists = _db.Favorites.Any(f => f.UserId == userId && f.ProductId == productId);
            return exists ? "Remove from Favorites" : "Add to Favorites";
        }

        protected void ToggleFavorite(object sender, CommandEventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            int productId = int.Parse(e.CommandArgument.ToString());
            var userId = Context.User.Identity.GetUserId();

            var fav = _db.Favorites.FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

            if (fav == null)
            {
                _db.Favorites.Add(new Models.Favorite
                {
                    UserId = userId,
                    ProductId = productId,
                    CreatedAt = DateTime.UtcNow
                });
            }
            else
            {
                _db.Favorites.Remove(fav);
            }

            _db.SaveChanges();
            LoadProducts();
        }
    }
}
