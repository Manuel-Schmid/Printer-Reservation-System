<%@ Page Title="Login" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Printer_Reservation_System.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">

    <div class="login">
        <asp:Label runat="server" CssClass="label">E-Mail</asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input input-box" placeholder="E-Mail eingeben..."></asp:TextBox>

        <asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtEmail"
            ErrorMessage="Bitte füllen Sie dieses Feld aus" 
            runat="server" Display="Dynamic" CssClass="validator"/>
        <asp:CustomValidator id="customValidator1" ControlToValidate="txtEmail"
            OnServerValidate="email_validator"
            ErrorMessage="Bitte geben Sie eine gültige E-Mail Adresse ein" runat="server" Display="Dynamic" CssClass="validator"/>

        <asp:Label ID="lblPassword" runat="server" Text="Passwort" CssClass="label">Passwort</asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input input-box" placeholder="Passwort eingeben..."></asp:TextBox>
        <asp:RequiredFieldValidator id="requiredFieldValidator5" ControlToValidate="txtPassword"
            ErrorMessage="Bitte füllen Sie diesses Feld aus" 
            runat="server" CssClass="validator"/>

        <asp:Label ID="lblInvalidLogin" runat="server" ForeColor="Red"></asp:Label>

        <div class="submit">
            <asp:Button type="submit" ID="btnLogin" runat="server" Text="Anmelden" CssClass="button is-link is-rounded" OnClick="btnLogin_Click" />
            <asp:Button type="submit" ID="btnSignup" runat="server" Text="Registrieren" CssClass="button is-text is-rounded" OnClick="btnSignup_Click" />
        </div>
        
    </div>

</asp:Content>
