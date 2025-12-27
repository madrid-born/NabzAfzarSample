<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="NabzAfzarSample.Account.Login" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login</h2>

    <asp:Label runat="server" Text="Email" />
    <asp:TextBox ID="Email" runat="server" />
    <br />

    <asp:Label runat="server" Text="Password" />
    <asp:TextBox ID="Password" runat="server" TextMode="Password" />
    <br />

    <asp:Button Text="Login" runat="server" OnClick="Login_Click" />
    <br />

    <asp:Literal ID="ErrorMessage" runat="server" />
</asp:Content>
