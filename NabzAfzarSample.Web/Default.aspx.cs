using System;

namespace NabzAfzarSample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {   
            Response.Redirect("~/Shop.aspx");
        }
    }
}