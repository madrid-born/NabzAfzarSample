using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Cart;
using NabzAfzarSample.Models;

namespace NabzAfzarSample
{
    public partial class CheckoutPage : System.Web.UI.Page
    {
        protected Literal SummaryLiteral;
        protected Button ConfirmButton;
        protected Literal ResultLiteral;

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
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (!Cart.Any())
                {
                    Response.Redirect("~/Shop.aspx");
                    return;
                }

                SummaryLiteral.Text = $"You are purchasing <strong>{Cart.Sum(x => x.Quantity)}</strong> items. " +
                                      $"Total: <strong>{Cart.Sum(x => x.Total)}</strong>";
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (!Cart.Any())
            {
                ResultLiteral.Text = "Cart is empty.";
                return;
            }

            var userId = Context.User.Identity.GetUserId();

            
            var productIds = Cart.Select(x => x.ProductId).Distinct().ToList(); 
            var products = _db.Products.Where(p => productIds.Contains(p.Id)).ToList();

            foreach (var item in Cart)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                {
                    ResultLiteral.Text = $"Product not found (ID={item.ProductId}).";
                    return;
                }

                if (!product.IsActive)
                {
                    ResultLiteral.Text = $"'{product.Name}' is not available anymore.";
                    return;
                }

                if (product.StockQuantity < item.Quantity)
                {
                    ResultLiteral.Text = $"Not enough stock for '{product.Name}'. Available: {product.StockQuantity}.";
                    return;
                }
            }

            
            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = Cart.Sum(x => x.Total),
                Status = OrderStatus.Pending
            };

            foreach (var item in Cart)
            {
                var product = products.First(p => p.Id == item.ProductId);

                product.StockQuantity -= item.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity
                });
            }

            _db.Orders.Add(order);
            _db.SaveChanges();

            
            Session["CART"] = new List<CartItem>();

            ConfirmButton.Enabled = false;
            Response.Redirect("~/MyOrders.aspx");
        }
    }
}
