<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Cart.aspx.cs"
Inherits="NabzAfzarSample.CartPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Your Cart</h1>
            <p class="page-subtitle">Review your items before checkout.</p>
        </div>
    </div>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <div class="cart-layout">

        <div class="cart-table-wrap">
            <asp:Repeater ID="CartRepeater" runat="server">

                <HeaderTemplate>
                    <table class="cart-table">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Image</th>
                                <th>Price</th>
                                <th>Qty</th>
                                <th>Total</th>
                                <th class="cart-actions-col">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td class="cart-product">
                            <%# Eval("ProductName") %>
                        </td>

                        <td>
                            <div class="cart-image">
                                <%# RenderCartImage(Eval("ProductId")) %>
                            </div>
                        </td>

                        <td class="cart-price">
                            <%# Eval("Price") %>
                        </td>

                        <td class="cart-qty">
                            <%# Eval("Quantity") %>
                        </td>

                        <td class="cart-total">
                            <%# Eval("Total") %>
                        </td>

                        <td class="cart-actions">
                            <asp:Button Text="-"
                                CssClass="qty-btn"
                                runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="DecreaseQty" />

                            <asp:Button Text="+"
                                CssClass="qty-btn"
                                runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="IncreaseQty" />

                            
                            <asp:Button Text="Remove"
                                CssClass="btn btn--danger btn--sm"
                                runat="server"
                                CommandArgument='<%# Eval("ProductId") %>'
                                OnCommand="RemoveItem" />
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>

            </asp:Repeater>
        </div>

        <!-- Summary -->
        <div class="cart-summary">
            <div class="card">
                <h2>Order Summary</h2>

                <div class="summary-row">
                    <span>Total</span>
                    <strong>
                        <asp:Literal ID="TotalLiteral" runat="server" />
                    </strong>
                </div>

                <asp:Button ID="CheckoutButton"
                    runat="server"
                    CssClass="btn btn--primary btn--block"
                    Text="Checkout"
                    OnClick="Checkout_Click" />
            </div>
        </div>

    </div>

</asp:Content>
