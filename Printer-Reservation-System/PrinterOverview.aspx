<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrinterOverview.aspx.cs" Inherits="Printer_Reservation_System.PrinterOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
	<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server" CssClass="active">Drucker</asp:HyperLink>
	<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" id="blockingTimesLink" runat="server">Sperrzeiten</asp:HyperLink>
	<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" >Reservationen</asp:HyperLink>
	<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server">Nutzerverwaltung</asp:HyperLink>
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

