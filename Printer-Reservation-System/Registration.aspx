<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Printer_Reservation_System.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">

    <span class="title">3D-Drucker Reservation</span>

    <div class="registration">
        <asp:Label runat="server" CssClass="label">Vorname</asp:Label>

        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input input-box" placeholder="Vorname eingeben..."></asp:TextBox>
        <asp:RequiredFieldValidator id="requiredFieldValidator1" ControlToValidate="txtFirstName"
            ErrorMessage="Bitte füllen Sie dieses Feld aus"
            runat="server" CssClass="validator"/> 

        <asp:Label runat="server" CssClass="label">Nachname</asp:Label>

        <asp:TextBox ID="txtLastName" runat="server" CssClass="input input-box" placeholder="Nachname eingeben..."></asp:TextBox>
            <asp:RequiredFieldValidator id="requiredFieldValidator2" ControlToValidate="txtLastName"
            ErrorMessage="Bitte füllen Sie dieses Feld aus" 
            runat="server" CssClass="validator"/>

        <asp:Label ID="lblPassword" runat="server" Text="Passwort" CssClass="label"></asp:Label>

        <asp:TextBox ID="txtPassword" runat="server" CssClass="input input-box" placeholder="Passwort eingeben..."></asp:TextBox>
        <asp:RequiredFieldValidator id="requiredFieldValidator5" ControlToValidate="txtPassword"
            ErrorMessage="Bitte füllen Sie diesses Feld aus" 
            runat="server" CssClass="validator"/>

        <div class="contact">
            <asp:Label runat="server" CssClass="label">E-Mail</asp:Label>

            <asp:TextBox ID="txtEmail" runat="server" CssClass="input input-box" placeholder="E-Mail eingeben..."></asp:TextBox>
            <asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtEmail"
                ErrorMessage="Bitte füllen Sie dieses Feld aus" 
                runat="server" Display="Dynamic" CssClass="validator"/>
            <asp:CustomValidator id="duplicateEmailValidator" ControlToValidate="txtEmail"
                ErrorMessage="Es existiert bereits ein Konto mit dieser E-Mail Adresse." 
                runat="server" Display="Dynamic" CssClass="validator"/>
            <asp:CustomValidator id="customValidator1" ControlToValidate="txtEmail"
                OnServerValidate="email_validator"
                ErrorMessage="Bitte geben Sie eine gültige E-Mail Adresse ein" runat="server" Display="Dynamic" CssClass="validator"/>

            <asp:Label runat="server" CssClass="label">Handy</asp:Label>

            <asp:TextBox ID="txtHandy" runat="server" CssClass="input input-box" placeholder="Telefonnummer eingeben..."></asp:TextBox>
            <asp:RequiredFieldValidator id="requiredFieldValidator6" ControlToValidate="txtHandy"
            ErrorMessage="Bitte füllen Sie dieses Feld aus" 
            runat="server" CssClass="validator"/>
        </div>

        <asp:Label id="lblMsg" runat="server" CssClass="label" ForeColor="Green"></asp:Label>
    
        <div class="submit">
            <asp:Button type="submit" ID="btnSignup" runat="server" Text="Registrieren" CssClass="button is-link is-rounded" OnClick="btnSignup_Click" />
            <asp:Button type="submit" ID="btnLogin" runat="server" Text="Anmelden" CssClass="button is-text is-rounded" OnClick="btnLogin_Click" />
        </div>
    </div>    
    
</asp:Content>
