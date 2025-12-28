<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Register.aspx.cs"
Inherits="NabzAfzarSample.Account.Register" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-wrap">
        <div class="auth-card">
            <div class="auth-head">
                <h1 class="page-title">Create your account</h1>
                <p class="page-subtitle">Register to save favorites and track your orders.</p>
            </div>

            <div class="form">

                <div class="field">
                    <asp:Label runat="server" AssociatedControlID="Email" Text="Email" CssClass="label" />
                    <asp:TextBox ID="Email" runat="server" CssClass="control" Placeholder="Email" />
                </div>

                <div class="field">
                    <asp:Label runat="server" AssociatedControlID="Password" Text="Password" CssClass="label" />
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="control" Placeholder="Password" />
                </div>

                <div class="actions">
                    <asp:Button Text="Register" runat="server" OnClick="Register_Click" CssClass="btn btn--primary btn--block" />
                    <a class="link-muted" href="/Account/Login.aspx">Already have an account? Login</a>
                </div>

                <div class="auth-message">
                    <asp:Literal ID="ResultMessage" runat="server" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>