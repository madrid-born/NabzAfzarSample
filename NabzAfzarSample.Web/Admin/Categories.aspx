<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Categories.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Categories" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Categories</h2>

    <h3>Add / Edit</h3>
    <asp:HiddenField ID="CategoryIdHidden" runat="server" />

    <div>
        <asp:Label runat="server" Text="Name:" />
        <asp:TextBox ID="NameTextBox" runat="server" />
    </div>

    <div>
        <asp:Label runat="server" Text="Description:" />
        <asp:TextBox ID="DescTextBox" runat="server" TextMode="MultiLine" Rows="3" />
    </div>

    <div>
        <asp:CheckBox ID="IsActiveCheckBox" runat="server" Text="Active" Checked="true" />
    </div>

    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="Save_Click" />
    <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="Clear_Click" />
    <br /><br />

    <asp:Literal ID="MessageLiteral" runat="server" />

    <hr />

    <h3>All Categories</h3>

    <asp:GridView ID="CategoriesGrid" runat="server"
                  AutoGenerateColumns="false"
                  OnRowCommand="CategoriesGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="IsActive" HeaderText="Active" />

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
