<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Orders.aspx.cs"
    Inherits="NabzAfzarSample.Admin.Orders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Manage Orders</h1>
            <p class="page-subtitle">Review customer orders and update their status.</p>
        </div>
        <a class="btn" href="/Admin/Dashboard.aspx">Back</a>
    </div>

    <div class="admin-message">
        <asp:Literal ID="MessageLiteral" runat="server" />
    </div>

    <div class="card admin-card-table">
        <div class="card-head">
            <h2>All Orders</h2>
            <p class="muted">Select an order to view details and update status.</p>
        </div>

        <div class="table-wrap">
            <asp:GridView ID="OrdersGrid" runat="server"
                CssClass="grid"
                AutoGenerateColumns="false"
                OnRowCommand="OrdersGrid_RowCommand"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Order ID" />
                    <asp:BoundField DataField="CreatedAt" HeaderText="Date" />
                    <asp:BoundField DataField="Customer" HeaderText="Customer" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total" />

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='status-pill <%# "status--" + (Eval("Status") ?? "").ToString().ToLower().Replace(" ", "") %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="View"
                                CssClass="btn btn--sm btn--primary"
                                CommandName="ViewOrder"
                                CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Panel ID="OrderPanel" runat="server" Visible="false">

        <div class="card orders-admin-details">
            <div class="card-head">
                <h2>Order Details</h2>
                <p class="muted">Update the status and review the ordered items.</p>
            </div>

            <div class="order-info">
                <asp:Literal ID="OrderInfoLiteral" runat="server" />
            </div>

            <div class="status-row">
                <div class="field">
                    <asp:Label runat="server" Text="Status" CssClass="label" />
                    <asp:DropDownList ID="StatusDropDown" runat="server" CssClass="control" />
                </div>

                <div class="status-actions">
                    <asp:Button ID="UpdateStatusButton" runat="server" Text="Update"
                        CssClass="btn btn--primary"
                        OnClick="UpdateStatus_Click" />
                </div>

                <asp:HiddenField ID="SelectedOrderIdHidden" runat="server" />
            </div>

            <div class="items-block">
                <h3 class="items-title">Items</h3>

                <asp:Repeater ID="ItemsRepeater" runat="server">
                    <HeaderTemplate>
                        <div class="table-wrap">
                            <table class="grid">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Unit Price</th>
                                        <th>Qty</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ProductName") %></td>
                            <td><%# Eval("UnitPrice") %></td>
                            <td><%# Eval("Quantity") %></td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                                </tbody>
                            </table>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
