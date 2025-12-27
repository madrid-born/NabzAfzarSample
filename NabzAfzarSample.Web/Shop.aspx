<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Shop.aspx.cs"
Inherits="NabzAfzarSample.Shop" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Shop</h2>

    <asp:Repeater ID="ProductsRepeater" runat="server">
        <ItemTemplate>
            <div style="border:1px solid #ccc;padding:10px;margin-bottom:10px;">
                <%# string.IsNullOrEmpty((string)Eval("ImageUrl"))
                        ? ""
                        : "<img src='" + Eval("ImageUrl") + "' style='width:120px;height:120px;object-fit:cover;border:1px solid #ccc;margin-bottom:8px;' />" %>
                <br />
                <strong><%# Eval("Name") %></strong><br />

                <strong><%# Eval("Name") %></strong><br />
                Price: <%# Eval("Price") %><br />
                Stock: <%# Eval("StockQuantity") %><br />

                <asp:Button Text="-"
                            CommandArgument='<%# Eval("Id") %>'
                            OnCommand="DecreaseQty"
                            runat="server" />

                <asp:Literal ID="QtyLiteral" runat="server"
                             Text='<%# GetQuantity(Eval("Id")) %>' />

                <asp:Button Text="+"
                            CommandArgument='<%# Eval("Id") %>'
                            OnCommand="IncreaseQty"
                            runat="server" />
                
                <br />
                <asp:Button runat="server"
                            Text='<%# GetFavoriteText(Eval("Id")) %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnCommand="ToggleFavorite" />

            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>