<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
Inherits="NabzAfzarSample.Admin.Dashboard" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-head">
        <div>
            <h1 class="page-title">Admin Dashboard</h1>
            <p class="page-subtitle">Manage your store content and orders.</p>
        </div>
        <div class="admin-badge">Admin</div>
    </div>

    <div class="admin-grid">

        <a class="admin-card" href="/Admin/Categories.aspx">
            <div class="admin-card__title">Manage Categories</div>
            <div class="admin-card__desc">Create, edit, and organize product categories.</div>
            <div class="admin-card__cta">Open</div>
        </a>

        <a class="admin-card" href="/Admin/Products.aspx">
            <div class="admin-card__title">Manage Products</div>
            <div class="admin-card__desc">Add products, update pricing, manage stock.</div>
            <div class="admin-card__cta">Open</div>
        </a>

        <a class="admin-card" href="/Admin/Orders.aspx">
            <div class="admin-card__title">Manage Orders</div>
            <div class="admin-card__desc">Review orders, update statuses, fulfill items.</div>
            <div class="admin-card__cta">Open</div>
        </a>

    </div>

</asp:Content>