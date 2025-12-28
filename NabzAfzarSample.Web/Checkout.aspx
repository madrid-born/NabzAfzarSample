<%@ Page Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Checkout.aspx.cs"
Inherits="NabzAfzarSample.CheckoutPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="checkout-wrap">
        <div class="checkout-card">
            <div class="checkout-head">
                <h1 class="page-title">Checkout</h1>
                <p class="page-subtitle">Confirm your purchase details below.</p>
            </div>

            <div class="checkout-summary">
                <asp:Literal ID="SummaryLiteral" runat="server" />
            </div>

            <div class="checkout-actions">
                <asp:Button ID="ConfirmButton"
                            runat="server"
                            Text="Confirm Purchase"
                            CssClass="btn btn--primary btn--block"
                            OnClick="Confirm_Click" />
            </div>

            <div class="checkout-result">
                <asp:Literal ID="ResultLiteral" runat="server" />
            </div>
        </div>
    </div>

</asp:Content>