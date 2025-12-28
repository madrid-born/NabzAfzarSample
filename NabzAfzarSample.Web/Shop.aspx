<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Shop.aspx.cs"
Inherits="NabzAfzarSample.Shop" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Shop</h1>
            <p class="page-subtitle">Browse products and add your desired quantity.</p>
        </div>
    </div>

    <asp:Panel runat="server" DefaultButton="SearchButton" CssClass="shop-search">

        <div class="search-grid">

            <asp:TextBox ID="SearchTextBox"
                         runat="server"
                         CssClass="control search-input"
                         Placeholder="Search products..." />

            <asp:DropDownList ID="CategoryDropDown"
                              runat="server"
                              CssClass="control search-select" />

            <asp:Button ID="SearchButton"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn--primary"
                        OnClick="Search_Click" />

            <asp:Button ID="ClearButton"
                        runat="server"
                        Text="Clear"
                        CssClass="btn"
                        OnClick="Clear_Click" />

        </div>
    </asp:Panel>
    <br/>
    <asp:Repeater ID="ProductsRepeater" runat="server">
        <HeaderTemplate>
            <div class="product-grid">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="product-card">

                <%# string.IsNullOrEmpty((string)Eval("ImageUrl"))
                        ? "<div class='product-image product-image--placeholder'>No Image</div>"
                        : "<img class='product-image' src='" + Eval("ImageUrl") + "' alt='" + Eval("Name") + "' />" %>

                <div class="product-body">
                    <div class="product-title"><%# Eval("Name") %></div>

                    <div class="product-meta">
                        <div class="meta-pill">
                            <span class="meta-label">Price</span>
                            <span class="meta-value"><%# Eval("Price") %></span>
                        </div>
                        <div class="meta-pill">
                            <span class="meta-label">Stock</span>
                            <span class="meta-value"><%# Eval("StockQuantity") %></span>
                        </div>
                    </div>

                    <div class="product-actions product-actions--stacked">

                        <div class="qty-stepper qty-stepper--full">
                            <asp:Button Text="-"
                                        CssClass="qty-btn"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnCommand="DecreaseQty"
                                        runat="server" />

                            <span class="qty-value">
                                <asp:Literal ID="QtyLiteral" runat="server"
                                             Text='<%# GetQuantity(Eval("Id")) %>' />
                            </span>

                            <asp:Button Text="+"
                                        CssClass="qty-btn"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnCommand="IncreaseQty"
                                        runat="server" />
                        </div>

                        <asp:Button runat="server"
                                    CssClass="btn btn--ghost btn--block"
                                    Text='<%# GetFavoriteText(Eval("Id")) %>'
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnCommand="ToggleFavorite" />

                    </div>
                </div>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
