<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Checkout.aspx.cs"
Inherits="NabzAfzarSample.CheckoutPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Checkout</h2>

    <asp:Literal ID="SummaryLiteral" runat="server" />
    <br /><br />

    <asp:Button ID="ConfirmButton" runat="server" Text="Confirm Purchase"
                OnClick="Confirm_Click" />
    <br /><br />
    <asp:Literal ID="ResultLiteral" runat="server" />
</asp:Content>