<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Products.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Products" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Products</h2>

    <h3>Add / Edit</h3>
    <asp:HiddenField ID="ProductIdHidden" runat="server" />

    <div>
        <asp:Label runat="server" Text="Name:" />
        <asp:TextBox ID="NameTextBox" runat="server" />
    </div>

    <div>
        <asp:Label runat="server" Text="Description:" />
        <asp:TextBox ID="DescTextBox" runat="server" TextMode="MultiLine" Rows="3" />
    </div>
    
    <div>
        <asp:Label runat="server" Text="Category:" />
        <asp:DropDownList ID="CategoryDropDown" runat="server" />
    </div>

    <div>
        <asp:Label runat="server" Text="Price:" />
        <asp:TextBox ID="PriceTextBox" runat="server" />
    </div>

    <div>
        <asp:Label runat="server" Text="Stock Quantity:" />
        <asp:TextBox ID="StockTextBox" runat="server" />
    </div>

    <div>
        <asp:Label runat="server" Text="Primary Image URL:" />
        <asp:TextBox ID="PrimaryImageUrlTextBox" runat="server" />
    </div>

    <div>
        <asp:CheckBox ID="IsActiveCheckBox" runat="server" Text="Active" Checked="true" />
    </div>

    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="Save_Click" />
    <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="Clear_Click" />
    <br /><br />

    <asp:Literal ID="MessageLiteral" runat="server" />

    <hr />

    <h3>All Products</h3>

    <asp:GridView ID="ProductsGrid" runat="server"
                  AutoGenerateColumns="false"
                  OnRowCommand="ProductsGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <asp:BoundField DataField="StockQuantity" HeaderText="Stock" />
            <asp:BoundField DataField="IsActive" HeaderText="Active" />

            <asp:TemplateField HeaderText="Low Stock">
                <ItemTemplate>
                    <%# (int)Eval("StockQuantity") <= 5 ? "Stock quantity is Low" : "" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Edit"
                                CommandName="EditRow"
                                CommandArgument='<%# Eval("Id") %>' />
                    <asp:Button runat="server" Text="Delete"
                                CommandName="DeleteRow"
                                CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
