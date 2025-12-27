using System;
using System.Collections.Generic;
using NabzAfzarSample.Cart;

namespace NabzAfzarSample
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }
        
        public int GetCartCount()
        {
            var cart = Session["CART"] as List<CartItem>;
            if (cart == null) return 0;
            var count = 0;
            foreach (var item in cart) count += item.Quantity;
            return count;
        }
    }
}