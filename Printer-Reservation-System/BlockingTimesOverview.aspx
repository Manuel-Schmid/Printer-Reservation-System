<%@ Page Title="3D-Drucker - Sperrzeiten" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlockingTimesOverview.aspx.cs" Inherits="Printer_Reservation_System.BlockingTimesOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">

	<link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">
    
	<div class="navbar1">
		<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server" CssClass="navbar__text">Drucker</asp:HyperLink>
		<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server" CssClass="active navbar__text">Sperrzeiten</asp:HyperLink>
		<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" CssClass="navbar__text">Reservationen</asp:HyperLink>
		<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server" CssClass="navbar__text">Nutzerverwaltung</asp:HyperLink>
    </div>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<div style="position: absolute; top: 25%; left: 50%; transform: translate(-50%);">

	<span style="font-size: 3vh; font-weight: bold; margin-bottom: 2vh;">Sperrzeiten</span>

	<asp:GridView style="height: 20vh; width: 70vw; font-size: 2vh; border: none; padding: 0;" CssClass="table--centered table table is-striped" HeaderStyle-CssClass="thead" RowStyle-CssClass="tr" ID="gvBlockingTimes" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvBlockingTimes_PageIndexChanging" OnRowCancelingEdit="gvBlockingTimes_RowCancelingEdit" OnRowDeleting="gvBlockingTimes_RowDeleting" OnRowEditing="gvBlockingTimes_RowEditing" OnRowUpdating="gvBlockingTimes_RowUpdating" OnRowDataBound="gvBlockingTimesRowDataBound">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
			<asp:BoundField DataField="Grund" HeaderText="Grund" />

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

			<asp:TemplateField HeaderText="Ausnahmen">
				<ItemTemplate>
					<asp:BulletedList ID="bltAusnahmen" runat="server"></asp:BulletedList>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />

			<asp:CommandField ShowEditButton="true" />
			<asp:CommandField ShowDeleteButton="true" />
		</Columns>
	</asp:GridView>

    </div>

	<asp:Button ID="btnCreateBlockingTime" runat="server" Text="+ Sperrzeit erstellen" OnClick="btnCreateBlockingTime_Click" CssClass="button is-link is-rounded" style="font-weight: bold; bottom: 0; left: 0; position: fixed; margin: 2vh; font-size: 2.3vh"/>

</asp:Content>
