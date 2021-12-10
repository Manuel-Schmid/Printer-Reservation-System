<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Printer_Reservation_System.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="login-styles.css" /> 
    <p>
        <asp:Literal ID="litInfo" runat="server" Text="Bitte registrieren Sie sich:" ></asp:Literal>
    </p>
    <br />
    <asp:Label runat="server" CssClass="firstName">Vorname</asp:Label>
    &nbsp;&nbsp;
    <br />
    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtFirstName"></asp:TextBox>
    <asp:RequiredFieldValidator id="requiredFieldValidator1" ControlToValidate="txtFirstName"
        ErrorMessage="Bitte füllen Sie dieses Feld aus"
        runat="server" CssClass="validator"/> 
    <br />
    <br />
    <asp:Label runat="server" CssClass="lastName">Nachname</asp:Label>
    &nbsp;&nbsp;
    <br />
    <asp:TextBox ID="txtLastName" runat="server" CssClass="txtLastName"></asp:TextBox>
        <asp:RequiredFieldValidator id="requiredFieldValidator2" ControlToValidate="txtLastName"
        ErrorMessage="Bitte füllen Sie dieses Feld aus" 
        runat="server" CssClass="validator"/>
    <br />
    <br />
    <asp:Label runat="server" CssClass="email">E-Mail</asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <br />
    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail"></asp:TextBox>
    <asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtEmail"
        ErrorMessage="Bitte füllen Sie dieses Feld aus" 
        runat="server" Display="Dynamic" CssClass="validator"/>
    <asp:CustomValidator id="duplicateEmailValidator" ControlToValidate="txtEmail"
        ErrorMessage="Es existiert bereits ein Konto mit dieser E-Mail Adresse." 
        runat="server" Display="Dynamic" CssClass="validator"/>
    <asp:CustomValidator id="customValidator1" ControlToValidate="txtEmail"
        OnServerValidate="email_validator"
        ErrorMessage="Bitte geben Sie eine gültige E-Mail Adresse ein" runat="server" Display="Dynamic" CssClass="validator"/>
    <br />
    <br />
    <asp:Label runat="server" CssClass="lblHandy">Handy</asp:Label>
    <br />
    <asp:TextBox ID="txtHandy" runat="server" CssClass="txtHandy"></asp:TextBox>
    <asp:RequiredFieldValidator id="requiredFieldValidator6" ControlToValidate="txtHandy"
    ErrorMessage="Bitte füllen Sie dieses Feld aus" 
    runat="server" CssClass="validator"/>
    <br />
    <br />
    <asp:Label ID="lblPassword" runat="server" Text="Passwort" CssClass="lblPassword"></asp:Label>
    <br />
    <asp:TextBox ID="txtPassword" runat="server" CssClass="txtPassword"></asp:TextBox>
    <asp:RequiredFieldValidator id="requiredFieldValidator5" ControlToValidate="txtPassword"
        ErrorMessage="Bitte füllen Sie diesses Feld aus" 
        runat="server" CssClass="validator"/>
    <br />
    <br />
    
    <asp:Button type="submit" ID="btnSignup" runat="server" Text="Registrieren" CssClass="btnSubmit" OnClick="btnSignup_Click" />

    &nbsp;&nbsp;

    <asp:Button type="submit" ID="btnLogin" runat="server" Text="Stattdessen anmelden " CssClass="btnSubmit" OnClick="btnLogin_Click" />

    
</asp:Content>
