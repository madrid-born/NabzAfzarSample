<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs"
Inherits="NabzAfzarSample.MyOrdersPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">My Orders</h1>
            <p class="page-subtitle">Track your purchases and review items anytime.</p>
        </div>
    </div>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <asp:Repeater ID="OrdersRepeater" runat="server" OnItemCommand="OrdersRepeater_ItemCommand">
        <HeaderTemplate>
            <div class="orders-list">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="order-card">
                <div class="order-main">
                    <div class="order-title">
                        Order <span class="order-id">#<%# Eval("Id") %></span>
                    </div>

                    <div class="order-meta">
                        <div class="meta-pill">
                            <span class="meta-label">Date</span>
                            <span class="meta-value"><%# Eval("CreatedAt") %></span>
                        </div>

                        <div class="meta-pill">
                            <span class="meta-label">Status</span>
                            <span class="meta-value">
                                <span class='status-pill <%# "status--" + (Eval("Status") ?? "").ToString().ToLower().Replace(" ", "") %>'>
                                    <%# Eval("Status") %>
                                </span>
                            </span>
                        </div>

                        <div class="meta-pill">
                            <span class="meta-label">Total</span>
                            <span class="meta-value"><%# Eval("TotalAmount") %></span>
                        </div>
                    </div>
                </div>

                <div class="order-actions">
                    <asp:Button Text="View Items"
                        runat="server"
                        CssClass="btn btn--primary"
                        CommandName="View"
                        CommandArgument='<%# Eval("Id") %>' />
                </div>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <asp:Panel ID="ItemsPanel" runat="server" Visible="false">
        <div class="orders-details">
            <div class="details-head">
                <h2>Order Items</h2>
                <p class="details-sub">Items for the selected order are shown below.</p>
            </div>

            <asp:Repeater ID="ItemsRepeater" runat="server">
                <HeaderTemplate>
                    <div class="cart-table-wrap">
                        <table class="cart-table">
                            <thead>
                                <tr>
                                    <th>Product</th>
                                    <th>Unit Price</th>
                                    <th>Qty</th>
                                </tr>
                            </thead>
                            <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td class="cart-product"><%# Eval("ProductName") %></td>
                        <td class="cart-price"><%# Eval("UnitPrice") %></td>
                        <td class="cart-qty"><%# Eval("Quantity") %></td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                            </tbody>
                        </table>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </asp:Panel>

</asp:Content>
