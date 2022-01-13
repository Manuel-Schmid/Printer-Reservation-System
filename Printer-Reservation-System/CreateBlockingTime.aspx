<%@ Page Title="3D-Drucker - Druckerübersicht" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBlockingTime.aspx.cs" Inherits="Printer_Reservation_System.CreateBlockingTime" %>
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

    <span style="font-size: 3.7vh; font-weight: bold; position: absolute; top: 20%; left: 50%; transform: translate(-50%, -50%);">neues Sperrfenster hinzufügen</span>

    <div style="position: absolute; top: 30%; left: 50%; transform: translate(-50%); height: auto; width: 50vw;">

        <span style="margin-right: 2vh; font-size: 2.5vh;">Drucker:</span>
		<asp:DropDownList ID="ddlPrinters" runat="server" CssClass="dropdown"></asp:DropDownList>
        <br />

		<asp:TextBox ID="txtFromDate" runat="server" placeholder="Datum [DD.MM.YY]" CssClass="input" style="width: 11vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtFromDate" style="color:red;"
			ErrorMessage="!" 
			runat="server" Display="Dynamic" />
		<asp:CompareValidator style="color:red;"
			id="dateValidator" 
			runat="server" 
			Type="Date"
			Operator="DataTypeCheck"
			ControlToValidate="txtFromDate" 
			ErrorMessage="!" 
			Display="Dynamic" >
		</asp:CompareValidator>

		<asp:TextBox ID="txtFromTime" runat="server" placeholder="Uhrzeit [HH:MM]" CssClass="input" style="width: 10vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator1" ControlToValidate="txtFromTime" style="color:red;"
			ErrorMessage="!" 
			runat="server" Display="Dynamic" />
		<asp:RegularExpressionValidator id=RegularExpressionValidator1 runat="server" ErrorMessage="!" Display="Dynamic" ControlToValidate="txtFromTime" 
			ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
		</asp:RegularExpressionValidator>


		<span style="font-size: 3vh; margin: 2vh;">bis</span> 

		<asp:TextBox ID="txtToDate" runat="server" placeholder="Datum [DD.MM.YY]" CssClass="input" style="width: 11vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator2" ControlToValidate="txtToDate" style="color:red;"
			ErrorMessage="!" 
			runat="server" Display="Dynamic"/>
		<asp:CompareValidator style="color:red;"
			id="CompareValidator1" 
			runat="server" 
			Type="Date"
			Operator="DataTypeCheck"
			ControlToValidate="txtToDate" 
			ErrorMessage="!" 
			Display="Dynamic" >
		</asp:CompareValidator>

		<asp:TextBox ID="txtToTime" runat="server" placeholder="Uhrzeit [HH:MM]" CssClass="input" style="width: 10vw; margin-top: 1vh;"></asp:TextBox>
		<asp:RequiredFieldValidator id="requiredFieldValidator4" ControlToValidate="txtToTime" style="color:red;"
			ErrorMessage="!" 
			runat="server" Display="Dynamic"/>
		<asp:RegularExpressionValidator style="color:red;" id=RegularExpressionValidator2 runat="server" ErrorMessage="!" Display="Dynamic" ControlToValidate="txtToTime" 
			ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
		</asp:RegularExpressionValidator>

		<asp:Label ID="lblWrongDateOrder" runat="server" style="color: red;"></asp:Label>

		<asp:TextBox ID="txtAreaReason" runat="server" TextMode="MultiLine" CssClass="textarea has-fixed-size" placeholder="Grund des Sperrfensters beschreiben..." style="width: 9vw; margin-top: 1vh; margin-bottom: 1vh;"></asp:TextBox>

        <span style="font-size: 2.4vh; margin-top: 1vh;">nicht betroffene Nutzer:</span>

		<asp:CheckBoxList ID="listStudents"	runat="server"></asp:CheckBoxList>

		 <asp:TextBox ID="txtAreaComment" runat="server" TextMode="MultiLine" placeholder="Bemerkung..." CssClass="textarea has-fixed-size" style="width: 8vw; margin-top: 1vh;"></asp:TextBox>
		
        <div style="position: absolute; right: 0; margin-top: 2vh;">
			<a href="/BlockingTimesOverview.aspx" class="button is-danger is-rounded">Abbruch</a>
			<asp:Button type="submit" ID="btnCreate" runat="server" Text="Erstellen" OnClick="btnCreate_Click" Cssclass="button is-success is-rounded" />
        </div>

	</div>

</asp:Content>
