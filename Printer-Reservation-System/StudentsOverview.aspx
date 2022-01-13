<%@ Page Title="3D-Drucker - Nutzerübersicht" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsOverview.aspx.cs" Inherits="Printer_Reservation_System.StudentsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">

	<link rel="stylesheet" runat="server" media="screen" href="/styles.css" /> 
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bulma@0.9.3/css/bulma.min.css">
	
	<div class="navbar1">
		<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server" CssClass="navbar__text">Drucker</asp:HyperLink>
		<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" runat="server" CssClass="navbar__text">Sperrzeiten</asp:HyperLink>
		<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" CssClass="navbar__text">Reservationen</asp:HyperLink>
		<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server" CssClass="active navbar__text">Nutzerverwaltung</asp:HyperLink>
	</div>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<div style="position: absolute; top: 25%; left: 50%; transform: translate(-50%);">
	
		<asp:GridView style="margin-bottom: 5%; height: 20vh; width: 70vw; font-size: 2vh; border: none; padding: 0;" CssClass="table--centered table table is-striped" HeaderStyle-CssClass="thead" RowStyle-CssClass="tr" ID="gridviewRegistrations" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="gridviewRegistrations_PageIndexChanging" OnRowCommand="gridviewRegistrations_RowCommand" >
			<Columns>
				<asp:BoundField DataField="Name" HeaderText="Name" />  
				<asp:BoundField DataField="Vorname" HeaderText="Vorname" />  
				<asp:BoundField DataField="E-Mail" HeaderText="E-Mail" />  
				<asp:BoundField DataField="Handy" HeaderText="Handy" />  
				<asp:Buttonfield buttontype="button" Text="Annehmen" commandname="accept" />
				<asp:Buttonfield buttontype="button" Text="Ablehnen" commandname="deny" />
			</Columns>
		</asp:GridView>

		<asp:GridView style="height: 20vh; width: 70vw; font-size: 2vh; border: none; padding: 0;" CssClass="table--centered table table is-striped" HeaderStyle-CssClass="thead" RowStyle-CssClass="tr" ID="gridviewStudents" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="gridviewStudents_PageIndexChanging" OnRowCancelingEdit="gridviewStudents_RowCancelingEdit" OnRowDeleting="gridviewStudents_RowDeleting" OnRowEditing="gridviewStudents_RowEditing" OnRowUpdating="gridviewStudents_RowUpdating" OnRowDataBound="gv_StatusRowDataBound">
			<Columns>
				<asp:CheckBoxField DataField="Admin" HeaderText="Admin"/>
				<asp:BoundField DataField="Name" HeaderText="Name" />  
				<asp:BoundField DataField="Vorname" HeaderText="Vorname" />  
				<asp:BoundField DataField="E-Mail" HeaderText="E-Mail" readonly="true" />  
				<asp:BoundField DataField="Handy" HeaderText="Handy" />  
				<asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />  

				<asp:TemplateField HeaderText="Status">
					<EditItemTemplate>
						<asp:DropDownList ID="ddl_Status" runat="server">
						</asp:DropDownList>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="Label4" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>

				<asp:CommandField ShowEditButton="true" />  
				<asp:CommandField ShowDeleteButton="true" /> 
			</Columns>
		</asp:GridView>

	</div>

	<asp:Button ID="btnLogout" runat="server" Text="Abmelden" OnClick="btnLogout_Click" class="button is-dark" style="font-weight: bold; bottom: 0; left: 0; position: absolute; margin: 2vh; height: 6vh; width: 15vh;"/>

</asp:Content>
