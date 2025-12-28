<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Products.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Products" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Manage Products</h1>
            <p class="page-subtitle">Add new products, update pricing, stock, and images.</p>
        </div>
        <a class="btn" href="/Admin/Dashboard.aspx">Back</a>
    </div>

    <div class="admin-layout">

        <div class="card admin-card-form">
            <div class="card-head">
                <h2>Add / Edit</h2>
                <p class="muted">Fill the form and click Save. Use Clear to reset fields.</p>
            </div>

            <asp:HiddenField ID="ProductIdHidden" runat="server" />

            <div class="form-grid form-grid-2">

                <div class="field span-2">
                    <asp:Label runat="server" Text="Name" CssClass="label" />
                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="control" />
                </div>

                <div class="field span-2">
                    <asp:Label runat="server" Text="Description" CssClass="label" />
                    <asp:TextBox ID="DescTextBox" runat="server" CssClass="control" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="field">
                    <asp:Label runat="server" Text="Category" CssClass="label" />
                    <asp:DropDownList ID="CategoryDropDown" runat="server" CssClass="control" />
                </div>

                <div class="field">
                    <asp:Label runat="server" Text="Price" CssClass="label" />
                    <asp:TextBox ID="PriceTextBox" runat="server" CssClass="control" />
                </div>

                <div class="field">
                    <asp:Label runat="server" Text="Stock Quantity" CssClass="label" />
                    <asp:TextBox ID="StockTextBox" runat="server" CssClass="control" />
                </div>

                <div class="field">
                    <asp:Label runat="server" Text="Primary Image URL" CssClass="label" />
                    <asp:TextBox ID="PrimaryImageUrlTextBox" runat="server" CssClass="control" />
                </div>

                <div class="field field-inline span-2">
                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Text="Active" Checked="true" CssClass="checkbox" />
                </div>

                <div class="form-actions span-2">
                    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="Save_Click" CssClass="btn btn--primary" />
                    <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="Clear_Click" CssClass="btn" />
                </div>

                <div class="admin-message span-2">
                    <asp:Literal ID="MessageLiteral" runat="server" />
                </div>

            </div>
        </div>

        <div class="card admin-card-table">
            <div class="card-head">
                <h2>All Products</h2>
                <p class="muted">Edit or delete products. Low stock is highlighted automatically.</p>
            </div>

            <div class="table-wrap">
                <asp:GridView ID="ProductsGrid" runat="server"
                    CssClass="grid"
                    AutoGenerateColumns="false"
                    OnRowCommand="ProductsGrid_RowCommand"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="StockQuantity" HeaderText="Stock" />
                        <asp:BoundField DataField="IsActive" HeaderText="Active" />

                        <asp:TemplateField HeaderText="Low Stock">
                            <ItemTemplate>
                                <%# (int)Eval("StockQuantity") <= 5
                                    ? "<span class='pill pill--warn'>Low Stock</span>"
                                    : "<span class='pill pill--muted'>OK</span>" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="grid-actions">
                                    <asp:Button runat="server" Text="Edit"
                                        CssClass="btn btn--sm"
                                        CommandName="EditRow"
                                        CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button runat="server" Text="Delete"
                                        CssClass="btn btn--danger btn--sm"
                                        CommandName="DeleteRow"
                                        CommandArgument='<%# Eval("Id") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>

    </div>

</asp:Content>
