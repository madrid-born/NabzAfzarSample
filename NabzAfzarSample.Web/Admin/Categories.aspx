<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Categories.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Categories" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Manage Categories</h1>
            <p class="page-subtitle">Create, edit, and activate categories for your products.</p>
        </div>
        <a class="btn" href="/Admin/Dashboard.aspx">Back</a>
    </div>

    <div class="admin-layout">

        <!-- Form Card -->
        <div class="card admin-card-form">
            <div class="card-head">
                <h2>Add / Edit</h2>
                <p class="muted">Use the form to create a new category or edit an existing one.</p>
            </div>

            <asp:HiddenField ID="CategoryIdHidden" runat="server" />

            <div class="form-grid">
                <div class="field">
                    <asp:Label runat="server" Text="Name" CssClass="label" />
                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="control" />
                </div>

                <div class="field">
                    <asp:Label runat="server" Text="Description" CssClass="label" />
                    <asp:TextBox ID="DescTextBox" runat="server" CssClass="control" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="field field-inline">
                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Text="Active" Checked="true" CssClass="checkbox" />
                </div>

                <div class="form-actions">
                    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="Save_Click" CssClass="btn btn--primary" />
                    <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="Clear_Click" CssClass="btn" />
                </div>

                <div class="admin-message">
                    <asp:Literal ID="MessageLiteral" runat="server" />
                </div>
            </div>
        </div>

        <!-- Table Card -->
        <div class="card admin-card-table">
            <div class="card-head">
                <h2>All Categories</h2>
                <p class="muted">Edit or delete existing categories below.</p>
            </div>

            <div class="table-wrap">
                <asp:GridView ID="CategoriesGrid" runat="server"
                    CssClass="grid"
                    AutoGenerateColumns="false"
                    OnRowCommand="CategoriesGrid_RowCommand"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="IsActive" HeaderText="Active" />

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
