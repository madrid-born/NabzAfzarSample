using System;
using System.Linq;
using System.Web.UI.WebControls;
using NabzAfzarSample.App_DataAccess;
using NabzAfzarSample.Identity;
using NabzAfzarSample.Models;

namespace NabzAfzarSample.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        // Rider: declare controls manually
        protected Literal MessageLiteral;
        protected GridView OrdersGrid;

        protected Panel OrderPanel;
        protected Literal OrderInfoLiteral;
        protected DropDownList StatusDropDown;
        protected Button UpdateStatusButton;
        protected HiddenField SelectedOrderIdHidden;
        protected Repeater ItemsRepeater;

        private readonly AppDbContext _db = new AppDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStatusDropdown();
                BindOrders();
                OrderPanel.Visible = false;
                MessageLiteral.Text = "";
            }
        }

        private void BindStatusDropdown()
        {
            StatusDropDown.DataSource = Enum.GetNames(typeof(OrderStatus));
            StatusDropDown.DataBind();
        }

        private string GetCustomerEmail(string userId)
        {
            using (var identityDb = new ApplicationIdentityDbContext())
            {
                var user = identityDb.Users.FirstOrDefault(u => u.Id == userId);
                return user?.Email ?? userId;
            }
        }

        private void BindOrders()
        {
            var orders = _db.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToList()
                .Select(o => new
                {
                    o.Id,
                    CreatedAt = o.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                    Customer = GetCustomerEmail(o.UserId),
                    o.TotalAmount,
                    Status = o.Status.ToString()
                })
                .ToList();

            OrdersGrid.DataSource = orders;
            OrdersGrid.DataBind();
        }

        private void ShowOrderDetails(int orderId)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                OrderPanel.Visible = false;
                MessageLiteral.Text = "Order not found.";
                return;
            }

            SelectedOrderIdHidden.Value = order.Id.ToString();

            var customer = GetCustomerEmail(order.UserId);

            OrderInfoLiteral.Text =
                $"<strong>Order #{order.Id}</strong><br/>" +
                $"Customer: <strong>{customer}</strong><br/>" +
                $"Date: {order.CreatedAt.ToLocalTime():yyyy-MM-dd HH:mm}<br/>" +
                $"Total: {order.TotalAmount}<br/>";

            // Ensure dropdown has values (in case someone calls this before BindStatusDropdown)
            if (StatusDropDown.Items.Count == 0)
                BindStatusDropdown();

            StatusDropDown.SelectedValue = order.Status.ToString();

            var items = _db.OrderItems
                .Where(i => i.OrderId == orderId)
                .ToList();

            ItemsRepeater.DataSource = items;
            ItemsRepeater.DataBind();

            OrderPanel.Visible = true;
        }

        protected void OrdersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ViewOrder")
                return;

            if (!int.TryParse(e.CommandArgument?.ToString(), out var orderId))
                return;

            MessageLiteral.Text = "";
            ShowOrderDetails(orderId);
        }

        protected void UpdateStatus_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SelectedOrderIdHidden.Value, out var orderId))
                return;

            var order = _db.Orders.Find(orderId);
            if (order == null)
            {
                OrderPanel.Visible = false;
                MessageLiteral.Text = "Order not found.";
                return;
            }

            if (!Enum.TryParse(StatusDropDown.SelectedValue, out OrderStatus newStatus))
            {
                MessageLiteral.Text = "Invalid status value.";
                return;
            }

            order.Status = newStatus;
            _db.SaveChanges();

            MessageLiteral.Text = "Order status updated.";
            BindOrders();

            // keep details panel open and refreshed
            ShowOrderDetails(orderId);
        }
    }
}
