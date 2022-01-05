<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlockingTimesOverview.aspx.cs" Inherits="Printer_Reservation_System.BlockingTimesOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
	<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server">Drucker</asp:HyperLink>
	<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server" CssClass="active">Sperrzeiten</asp:HyperLink>
	<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server">Reservationen</asp:HyperLink>
	<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server">Nutzerverwaltung</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<asp:GridView ID="gvBlockingTimes" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvBlockingTimes_PageIndexChanging" OnRowCancelingEdit="gvBlockingTimes_RowCancelingEdit" OnRowDeleting="gvBlockingTimes_RowDeleting" OnRowEditing="gvBlockingTimes_RowEditing" OnRowUpdating="gvBlockingTimes_RowUpdating" OnRowDataBound="gvBlockingTimesRowDataBound">
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

	<br />
	<asp:Button ID="btnCreateBlockingTime" runat="server" Text="Erstellen" OnClick="btnCreateBlockingTime_Click" />

</asp:Content>
