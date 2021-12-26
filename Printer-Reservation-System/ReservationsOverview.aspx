<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReservationsOverview.aspx.cs" Inherits="Printer_Reservation_System.ReservationsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
	Reservationenverwaltung
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvReservations_PageIndexChanging" OnRowCancelingEdit="gvReservations_RowCancelingEdit" OnRowDeleting="gvReservations_RowDeleting" OnRowEditing="gvReservations_RowEditing" OnRowUpdating="gvReservations_RowUpdating">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
			<asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
			<asp:BoundField DataField="Vorname" HeaderText="Vorname" ReadOnly="true" />
			<asp:BoundField DataField="Drucker" HeaderText="Drucker" />  
			<asp:BoundField DataField="Von" HeaderText="Von" />
			<asp:BoundField DataField="Bis" HeaderText="Bis" />  
			<asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />

			<asp:CommandField ShowEditButton="true" />
			<asp:CommandField ShowDeleteButton="true" />
		</Columns>
	</asp:GridView>

	<br />
	<asp:Button ID="btnAddReservation" runat="server" OnClick="btnAddReservation_Click" Text="Hinzufügen" />

</asp:Content>
