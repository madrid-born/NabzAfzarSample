<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Cart.aspx.cs"
Inherits="NabzAfzarSample.CartPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Your Cart</h2>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <asp:Repeater ID="CartRepeater" runat="server">
        <HeaderTemplate>
            <table border="1" cellpadding="8" cellspacing="0" style="width:100%;">
            <tr>
                <th>Product</th>
                <th>Image</th>
                <th>Price</th>
                <th>Qty</th>
                <th>Total</th>
                <th>Actions</th>
            </tr>
            <%-- </table> --%>
        </HeaderTemplate>
                
        <ItemTemplate>
            <tr>
                <td><%# Eval("ProductName") %></td>
                <td>
                    <%# RenderCartImage(Eval("ProductId")) %>
                </td>
                <td><%# Eval("Price") %></td>
                <td><%# Eval("Quantity") %></td>
                <td><%# Eval("Total") %></td>
                <td>
                    <asp:Button Text="-" runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="DecreaseQty" />
                    <asp:Button Text="+" runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="IncreaseQty" />
                    <asp:Button Text="Remove" runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="RemoveItem" />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
        </table>
        </FooterTemplate>
    </asp:Repeater>

    <br />
    <asp:Literal ID="TotalLiteral" runat="server" />

    <br /><br />

    <asp:Button ID="CheckoutButton" runat="server" Text="Checkout"
                OnClick="Checkout_Click" />
</asp:Content>