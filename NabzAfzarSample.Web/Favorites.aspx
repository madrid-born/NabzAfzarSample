<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Favorites.aspx.cs"
Inherits="NabzAfzarSample.FavoritesPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">My Favorites</h1>
            <p class="page-subtitle">Your saved products in one place.</p>
        </div>
    </div>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <asp:Repeater ID="FavoritesRepeater" runat="server" OnItemCommand="FavoritesRepeater_ItemCommand">
        <HeaderTemplate>
            <div class="product-grid">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="product-card">
                <%# string.IsNullOrEmpty((string)Eval("ImageUrl"))
                        ? "<div class='product-image product-image--placeholder'>No Image</div>"
                        : "<img class='product-image' src='" + Eval("ImageUrl") + "' alt='"  + "' />" %>
                
                <div class="product-body">
                    <div class="product-title"><%# Eval("Product.Name") %></div>

                    <div class="product-meta">
                        <div class="meta-pill">
                            <span class="meta-label">Price</span>
                            <span class="meta-value"><%# Eval("Product.Price") %></span>
                        </div>
                        <div class="meta-pill">
                            <span class="meta-label">Status</span>
                            <span class="meta-value">Saved</span>
                        </div>
                    </div>

                    <div class="product-actions">
                        <asp:Button Text="Remove"
                            runat="server"
                            CssClass="btn btn--danger btn--block"
                            CommandName="Remove"
                            CommandArgument='<%# Eval("ProductId") %>' />
                    </div>
                </div>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
