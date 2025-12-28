<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Login.aspx.cs"
Inherits="NabzAfzarSample.Account.Login" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-wrap">
        <div class="auth-card">
            <div class="auth-head">
                <h1 class="page-title">Welcome back</h1>
                <p class="page-subtitle">Login to access your cart, orders, and favorites.</p>
            </div>

            <div class="form">

                <div class="field">
                    <asp:Label runat="server" AssociatedControlID="Email" Text="Email" CssClass="label" />
                    <asp:TextBox ID="Email" runat="server" CssClass="control" />
                </div>

                <div class="field">
                    <asp:Label runat="server" AssociatedControlID="Password" Text="Password" CssClass="label" />
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="control" />
                </div>

                <div class="actions">
                    <asp:Button Text="Login" runat="server" OnClick="Login_Click" CssClass="btn btn--primary btn--block" />
                    <a class="link-muted" href="/Account/Register.aspx">Don’t have an account? Create one</a>
                </div>

                <div class="auth-error">
                    <asp:Literal ID="ErrorMessage" runat="server" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>