<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateReservation.aspx.cs" Inherits="Printer_Reservation_System.CreateReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <p>
        <asp:Literal ID="litInfo" runat="server" Text="Bitte melden Sie sich an:" ></asp:Literal>
    </p>
    <br />
    <br />
    <asp:Label runat="server" CssClass="email">E-Mail</asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <br />
    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail"></asp:TextBox>
    <asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtEmail"
        ErrorMessage="Bitte füllen Sie dieses Feld aus" 
        runat="server" Display="Dynamic" CssClass="validator"/>
    <br />
    <br />

    <asp:Button type="submit" ID="btnLogin" runat="server" Text="Anmelden" CssClass="btnSubmit" OnClick="btnLogin_Click" />


</asp:Content>
