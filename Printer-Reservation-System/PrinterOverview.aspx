<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrinterOverview.aspx.cs" Inherits="Printer_Reservation_System.PrinterOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
    Druckerverwaltung
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <asp:GridView ID="gridviewPrinters" runat="server">
    </asp:GridView>

</asp:Content>

