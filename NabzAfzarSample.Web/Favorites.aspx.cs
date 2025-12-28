using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using NabzAfzarSample.App_DataAccess;

namespace NabzAfzarSample
{
    public partial class FavoritesPage : System.Web.UI.Page
    {
        
        protected Literal EmptyMessage;
        protected Repeater FavoritesRepeater;

        private AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadFavorites();
        }

        private void LoadFavorites()
        {
            var userId = Context.User.Identity.GetUserId();

            var favorites2 = _db.Favorites
                .Where(f => f.UserId == userId)
                .ToList();
            
            var query = _db.Favorites.Where(f => f.UserId == userId)
                .ToList();
            var favorites = query
                .Select(p => new
                {
                    p.ProductId,
                    Product = _db.Products.Find(p.ProductId),
                    ImageUrl = _db.ProductImages
                        .Where(i => i.ProductId == p.ProductId && i.IsPrimary)
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
                })
                .ToList();


            
            
            
            
            
            

            if (!favorites.Any())
            {
                EmptyMessage.Text = "You have no favorite products.";
                FavoritesRepeater.Visible = false;
                return;
            }

            EmptyMessage.Text = "";
            FavoritesRepeater.Visible = true;
            FavoritesRepeater.DataSource = favorites;
            FavoritesRepeater.DataBind();
        }

        protected void FavoritesRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Remove")
                return;

            int productId = int.Parse(e.CommandArgument.ToString());
            var userId = Context.User.Identity.GetUserId();

            var fav = _db.Favorites.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if (fav != null)
            {
                _db.Favorites.Remove(fav);
                _db.SaveChanges();
            }

            LoadFavorites();
        }
    }
}
