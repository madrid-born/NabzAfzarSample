using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using NabzAfzarSample.App_DataAccess;

namespace NabzAfzarSample
{
    public partial class MyOrdersPage : System.Web.UI.Page
    {
        // Rider: declare controls manually
        protected Literal EmptyMessage;
        protected Repeater OrdersRepeater;
        protected Panel ItemsPanel;
        protected Repeater ItemsRepeater;

        private AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadOrders();
        }

        private void LoadOrders()
        {
            var userId = Context.User.Identity.GetUserId();

            var orders = _db.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            if (!orders.Any())
            {
                EmptyMessage.Text = "You have no orders yet.";
                OrdersRepeater.Visible = false;
                return;
            }

            EmptyMessage.Text = "";
            OrdersRepeater.Visible = true;
            OrdersRepeater.DataSource = orders;
            OrdersRepeater.DataBind();
        }

        protected void OrdersRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "View")
                return;

            int orderId = int.Parse(e.CommandArgument.ToString());
            var userId = Context.User.Identity.GetUserId();

            var order = _db.Orders.FirstOrDefault(o => o.Id == orderId && o.UserId == userId);
            if (order == null) return;

            var items = _db.OrderItems
                .Where(i => i.OrderId == orderId)
                .ToList();

            ItemsPanel.Visible = true;
            ItemsRepeater.DataSource = items;
            ItemsRepeater.DataBind();
        }
    }
}
