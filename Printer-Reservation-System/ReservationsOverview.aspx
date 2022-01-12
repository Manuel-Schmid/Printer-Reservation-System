<%@ Page Title="3D-Drucker - Reservationen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReservationsOverview.aspx.cs" Inherits="Printer_Reservation_System.ReservationsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">

	<link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">
	
	<div class="navbar1">
		<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server" CssClass="navbar__text">Drucker</asp:HyperLink>
		<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server" CssClass="navbar__text">Sperrzeiten</asp:HyperLink>
		<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" CssClass="active navbar__text">Reservationen</asp:HyperLink>
		<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server" CssClass="navbar__text">Nutzerverwaltung</asp:HyperLink>
	</div>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<asp:GridView ID="gvAllRes" runat="server" AutoGenerateColumns="false" >
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" />
			<asp:BoundField DataField="Name" HeaderText="Name" />
			<asp:BoundField DataField="Vorname" HeaderText="Vorname" />
			<asp:BoundField DataField="Drucker" HeaderText="Drucker" />
			<asp:BoundField DataField="Von" HeaderText="Von" />
			<asp:BoundField DataField="Bis" HeaderText="Bis" />  
			<asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />
		</Columns>
	</asp:GridView>

	<br />

	<asp:GridView style="height: 20vh; width: 70vw; font-size: 2vh; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); border: none; padding: 0;" CssClass="table--centered table table is-striped" HeaderStyle-CssClass="thead" RowStyle-CssClass="tr" ID="gvReservations" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvReservations_PageIndexChanging" OnRowCancelingEdit="gvReservations_RowCancelingEdit" OnRowDeleting="gvReservations_RowDeleting" OnRowEditing="gvReservations_RowEditing" OnRowUpdating="gvReservations_RowUpdating" OnRowDataBound="gvReservationsRowDataBound">
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
	<asp:Button ID="btnAddReservation" runat="server" OnClick="btnAddReservation_Click" Text="+ Reservation erstellen" CssClass="button is-link is-rounded" style="font-weight: bold; bottom: 0; left: 0; position: absolute; margin: 2vh; height: 5vh; width: 21vh;"/>

</asp:Content>
