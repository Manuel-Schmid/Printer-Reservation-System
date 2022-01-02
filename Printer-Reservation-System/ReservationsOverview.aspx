<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReservationsOverview.aspx.cs" Inherits="Printer_Reservation_System.ReservationsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
	<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server">Drucker</asp:HyperLink>
	<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server">Sperrzeiten</asp:HyperLink>
	<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" CssClass="active">Reservationen</asp:HyperLink>
	<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server">Nutzerverwaltung</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvReservations_PageIndexChanging" OnRowCancelingEdit="gvReservations_RowCancelingEdit" OnRowDeleting="gvReservations_RowDeleting" OnRowEditing="gvReservations_RowEditing" OnRowUpdating="gvReservations_RowUpdating" OnRowDataBound="gvReservationsRowDataBound">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
			<asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
			<asp:BoundField DataField="Vorname" HeaderText="Vorname" ReadOnly="true" />

			<asp:TemplateField HeaderText="Drucker">
				<EditItemTemplate>
					<asp:DropDownList ID="ddl_Drucker" runat="server">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="Label4" runat="server" Text='<%# Eval("Drucker") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

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
