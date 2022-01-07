<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrinterOverview.aspx.cs" Inherits="Printer_Reservation_System.PrinterOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">

	<link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">
    
	<div class="navbar1">
		<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server" CssClass=" active navbar__text">Drucker</asp:HyperLink>
		<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server" CssClass="navbar__text">Sperrzeiten</asp:HyperLink>
		<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" CssClass="navbar__text">Reservationen</asp:HyperLink>
		<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server" CssClass="navbar__text">Nutzerverwaltung</asp:HyperLink>
    </div>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<asp:GridView ID="gvPrinters" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvPrinters_PageIndexChanging" OnRowCancelingEdit="gvPrinters_RowCancelingEdit" OnRowDeleting="gvPrinters_RowDeleting" OnRowEditing="gvPrinters_RowEditing" OnRowUpdating="gvPrinters_RowUpdating">
		<Columns>  
			<asp:BoundField DataField="ID" HeaderText="Nr." readonly="true" />  
			<asp:BoundField DataField="Marke" HeaderText="Marke" />  
			<asp:BoundField DataField="Modell" HeaderText="Modell" />  
			<asp:BoundField DataField="Typ" HeaderText="Typ" />
			<asp:BoundField DataField="Beschreibung" HeaderText="Beschreibung" />  
			<asp:BoundField DataField="Druckbereich" HeaderText="Druckbereich (L x B x H)" />  

			<asp:CommandField ShowEditButton="true" />  
			<asp:CommandField ShowDeleteButton="true" /> 
		</Columns>
	</asp:GridView>

</asp:Content>

