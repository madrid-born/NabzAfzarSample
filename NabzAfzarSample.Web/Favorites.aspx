<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Favorites.aspx.cs"
Inherits="NabzAfzarSample.FavoritesPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Favorites</h2>

    <asp:Literal ID="EmptyMessage" runat="server" />

    <asp:Repeater ID="FavoritesRepeater" runat="server" OnItemCommand="FavoritesRepeater_ItemCommand">
        <ItemTemplate>
            <div style="border:1px solid #ccc;padding:10px;margin-bottom:10px;">
                <strong><%# Eval("Product.Name") %></strong><br/>
                Price: <%# Eval("Product.Price") %><br/>

                <asp:Button Text="Remove from Favorites"
                            runat="server"
                            CommandName="Remove"
                            CommandArgument='<%# Eval("ProductId") %>' />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>