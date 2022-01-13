<%@ Page Title="3D-Drucker - Reservationen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateReservation.aspx.cs" Inherits="Printer_Reservation_System.CreateReservation" %>
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

	<span style="font-size: 3vh; font-weight: bold; position: absolute; top: 20%; left: 50%; transform: translate(-50%, -50%);">Drucker reservieren</span>

    <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); height: 22vh; width: 38vw;">

		<span style="margin-right: 2vh; font-size: 2vh;">Drucker:</span>
		<asp:DropDownList ID="ddlPrinters" runat="server"></asp:DropDownList>
        <br />

		<asp:TextBox ID="txtFromDate" runat="server" placeholder="Datum [DD.MM.YY]" CssClass="input" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtFromDate"
			ErrorMessage="!" 
			runat="server"x Display="Dynamic" />
		<asp:CompareValidator
			id="dateValidator" 
			runat="server" 
			Type="Date"
			Operator="DataTypeCheck"
			ControlToValidate="txtFromDate" 
			ErrorMessage="!" 
			Display="Dynamic" >
		</asp:CompareValidator>

		<asp:TextBox ID="txtFromTime" runat="server" placeholder="Uhrzeit [HH:MM]" CssClass="input" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator1" ControlToValidate="txtFromTime"
			ErrorMessage="!" 
			runat="server" Display="Dynamic" />
		<asp:RegularExpressionValidator id=RegularExpressionValidator1 runat="server" ErrorMessage="!" Display="Dynamic" ControlToValidate="txtFromTime" 
			ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
		</asp:RegularExpressionValidator>

		<span style="font-size: 2vh; margin: 2vh;">bis</span> 

		<asp:TextBox ID="txtToDate" runat="server" placeholder="Datum [DD.MM.YY]" CssClass="input" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator2" ControlToValidate="txtToDate"
			ErrorMessage="!" 
			runat="server" Display="Dynamic"/>
		<asp:CompareValidator
			id="CompareValidator1" 
			runat="server" 
			Type="Date"
			Operator="DataTypeCheck"
			ControlToValidate="txtToDate" 
			ErrorMessage="!" 
			Display="Dynamic" >
		</asp:CompareValidator>

		<asp:TextBox ID="txtToTime" runat="server" placeholder="Uhrzeit [HH:MM]" CssClass="input" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator4" ControlToValidate="txtToTime"
			ErrorMessage="!" 
			runat="server" Display="Dynamic"/>
		<asp:RegularExpressionValidator id=RegularExpressionValidator2 runat="server" ErrorMessage="!" Display="Dynamic" ControlToValidate="txtToTime" 
			ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
		</asp:RegularExpressionValidator>

		<asp:TextBox ID="txtAreaComment" runat="server" TextMode="MultiLine" placeholder="Bemerkung..." CssClass="textarea has-fixed-size" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>

		<asp:Label ID="lblWrongDateOrder" runat="server"></asp:Label>

		<div style="position: absolute; right: 0; bottom: 0;">
			<a href="/ReservationsOverview.aspx" class="button is-danger is-rounded">Abbruch</a>
			<asp:Button type="submit" ID="btnCreate" runat="server" Text="Erstellen" OnClick="btnCreate_Click" Cssclass="button is-success is-rounded" />
        </div>

	</div>


</asp:Content>
