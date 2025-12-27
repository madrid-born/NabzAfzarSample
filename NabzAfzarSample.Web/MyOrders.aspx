<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs"
Inherits="NabzAfzarSample.MyOrdersPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Orders</h2>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <asp:Repeater ID="OrdersRepeater" runat="server" OnItemCommand="OrdersRepeater_ItemCommand">
        <ItemTemplate>
            <div style="border:1px solid #ccc;padding:10px;margin-bottom:10px;">
                <strong>Order #<%# Eval("Id") %></strong><br/>
                Date: <%# Eval("CreatedAt") %><br/>
                Status: <%# Eval("Status") %><br/>
                Total: <%# Eval("TotalAmount") %><br/>

                <asp:Button Text="View Items"
                            runat="server"
                            CommandName="View"
                            CommandArgument='<%# Eval("Id") %>' />
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <hr />

    <asp:Panel ID="ItemsPanel" runat="server" Visible="false">
        <h3>Order Items</h3>

        <asp:Repeater ID="ItemsRepeater" runat="server">
            <HeaderTemplate>
                <table border="1" cellpadding="8" cellspacing="0" style="width:100%;">
                <tr>
                    <th>Product</th>
                    <th>Unit Price</th>
                    <th>Qty</th>
                </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("ProductName") %></td>
                    <td><%# Eval("UnitPrice") %></td>
                    <td><%# Eval("Quantity") %></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
            </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>