<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Register.aspx.cs"
Inherits="NabzAfzarSample.Account.Register" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Register</h2>

    <asp:TextBox ID="Email" runat="server" Placeholder="Email" />
    <br />

    <asp:TextBox ID="Password" runat="server" TextMode="Password" />
    <br />

    <asp:Button Text="Register" runat="server" OnClick="Register_Click" />
    <br />

    <asp:Literal ID="ResultMessage" runat="server" />
</asp:Content>