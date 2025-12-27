<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
Inherits="NabzAfzarSample.Admin.Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin Dashboard</h2>

    <p>Welcome, Admin.</p>

    <ul>
        <li><a href="/Admin/Categories.aspx">Manage Categories</a></li>
        <li><a href="/Admin/Products.aspx">Manage Products</a></li>
        <li><a href="/Admin/Orders.aspx">Manage Orders</a></li>
    </ul>
</asp:Content>