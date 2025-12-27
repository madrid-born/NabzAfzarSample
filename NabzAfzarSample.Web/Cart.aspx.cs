using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Cart;

namespace NabzAfzarSample
{
    public partial class CartPage : System.Web.UI.Page
    {
        // Rider: declare controls manually
        protected Repeater CartRepeater;
        protected Literal EmptyMessage;
        protected Literal TotalLiteral;
        protected Button CheckoutButton;

        private AppDbContext _db = new AppDbContext();

        private List<CartItem> Cart
        {
            get
            {
                if (Session["CART"] == null)
                    Session["CART"] = new List<CartItem>();

                return (List<CartItem>)Session["CART"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindCart();
        }

        private void BindCart()
        {
            if (!Cart.Any())
            {
                EmptyMessage.Text = "Your cart is empty.";
                CheckoutButton.Visible = false;
                CartRepeater.Visible = false;
                TotalLiteral.Text = "";
                return;
            }

            EmptyMessage.Text = "";
            CheckoutButton.Visible = true;
            CartRepeater.Visible = true;

            CartRepeater.DataSource = Cart;
            CartRepeater.DataBind();

            TotalLiteral.Text = $"<strong>Total:</strong> {Cart.Sum(x => x.Total)}";
        }

        protected void IncreaseQty(object sender, CommandEventArgs e)
        {
            int productId = int.Parse(e.CommandArgument.ToString());
            var product = _db.Products.Find(productId);
            if (product == null) return;

            var item = Cart.FirstOrDefault(x => x.ProductId == productId);
            if (item == null) return;

            if (item.Quantity < product.StockQuantity)
                item.Quantity++;

            BindCart();
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

            BindCart();
        }

        protected void RemoveItem(object sender, CommandEventArgs e)
        {
            int productId = int.Parse(e.CommandArgument.ToString());
            Cart.RemoveAll(x => x.ProductId == productId);
            BindCart();
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx");
        }
        
        protected string RenderCartImage(object productIdObj)
        {
            int productId = Convert.ToInt32(productIdObj);

            var url = _db.ProductImages
                .Where(i => i.ProductId == productId && i.IsPrimary)
                .Select(i => i.ImageUrl)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(url))
                return "";

            return $"<img src='{url}' style='width:60px;height:60px;object-fit:cover;border:1px solid #ccc;' />";
        }

    }
}
