<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Orders.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Orders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Orders</h2>

    <asp:Literal ID="MessageLiteral" runat="server" />

    <h3>All Orders</h3>

    <asp:GridView ID="OrdersGrid" runat="server"
                  AutoGenerateColumns="false"
                  OnRowCommand="OrdersGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Order ID" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Date" />
            <asp:BoundField DataField="Customer" HeaderText="Customer" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
            <asp:BoundField DataField="Status" HeaderText="Status" />

            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button runat="server" Text="View"
                                CommandName="ViewOrder"
                                CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <hr />

    <asp:Panel ID="OrderPanel" runat="server" Visible="false">
        <h3>Order Details</h3>

        <asp:Literal ID="OrderInfoLiteral" runat="server" />
        <br /><br />

        <asp:Label runat="server" Text="Status:" />
        <asp:DropDownList ID="StatusDropDown" runat="server" />
        <asp:Button ID="UpdateStatusButton" runat="server" Text="Update"
                    OnClick="UpdateStatus_Click" />
        <asp:HiddenField ID="SelectedOrderIdHidden" runat="server" />

        <br /><br />

        <asp:Repeater ID="ItemsRepeater" runat="server">
            <HeaderTemplate>
                <table border="1" cellpadding="8" cellspacing="0" style="width:100%;">
                    <tr>
                        <th>Product</th>
                        <th>Unit Price</th>
                        <th>Qty</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("ProductName") %></td>
                    <td><%# Eval("UnitPrice") %></td>
                    <td><%# Eval("Quantity") %></td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>

</asp:Content>
