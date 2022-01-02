<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBlockingTime.aspx.cs" Inherits="Printer_Reservation_System.CreateBlockingTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
	<asp:HyperLink NavigateUrl="PrinterOverview.aspx" runat="server">Drucker</asp:HyperLink>
	<asp:HyperLink NavigateUrl="BlockingTimesOverview.aspx" id="blockingTimesLink" runat="server" CssClass="active">Sperrzeiten</asp:HyperLink>
	<asp:HyperLink NavigateUrl="ReservationsOverview.aspx" runat="server" >Reservationen</asp:HyperLink>
	<asp:HyperLink NavigateUrl="StudentsOverview.aspx" runat="server">Nutzerverwaltung</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<p>
		<asp:Literal ID="litInfo" runat="server" Text="Bitte geben Sie die gewünschten Daten für das Sperrfenster an: " ></asp:Literal>
	</p>

	 <br />
	 <asp:Label ID="Label4" runat="server" Text="Grund"></asp:Label>
	 <br />
	 <asp:TextBox ID="txtAreaReason" runat="server" TextMode="MultiLine"></asp:TextBox>
	 <br />
	<br />
	<br />

	Zu sperrender Drucker<br />
	<asp:DropDownList ID="ddlPrinters" runat="server"></asp:DropDownList>
	<br />
	<br />
	<br />
	 <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="Von"></asp:Label>
	<br />

	<asp:Label runat="server">Datum</asp:Label>
	<br />
	<asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
	<asp:RequiredFieldValidator id="requiredFieldValidator3" ControlToValidate="txtFromDate"
		ErrorMessage="Bitte füllen Sie dieses Feld aus" 
		runat="server" Display="Dynamic" />
	<asp:CompareValidator
		id="dateValidator" 
		runat="server" 
		Type="Date"
		Operator="DataTypeCheck"
		ControlToValidate="txtFromDate" 
		ErrorMessage="Bitte geben Sie ein gültiges Datum ein." 
		Display="Dynamic" >
	</asp:CompareValidator>
	<br />
	<br />

	<asp:Label runat="server">Uhrzeit</asp:Label>
	<br />
	<asp:TextBox ID="txtFromTime" runat="server"></asp:TextBox>
	<asp:RequiredFieldValidator id="requiredFieldValidator1" ControlToValidate="txtFromTime"
		ErrorMessage="Bitte füllen Sie dieses Feld aus" 
		runat="server" Display="Dynamic" />
	<asp:RegularExpressionValidator id=RegularExpressionValidator1 runat="server" ErrorMessage="Bitte geben Sie eine gültige Uhrzeit ein." Display="Dynamic" ControlToValidate="txtFromTime" 
		ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
	</asp:RegularExpressionValidator>
	<br />
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="Bis"></asp:Label>
	<br />

	<asp:Label runat="server">Datum</asp:Label>
	<br />
	<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
	<asp:RequiredFieldValidator id="requiredFieldValidator2" ControlToValidate="txtToDate"
		ErrorMessage="Bitte füllen Sie dieses Feld aus" 
		runat="server" Display="Dynamic"/>
	<asp:CompareValidator
		id="CompareValidator1" 
		runat="server" 
		Type="Date"
		Operator="DataTypeCheck"
		ControlToValidate="txtToDate" 
		ErrorMessage="Bitte geben Sie ein gültiges Datum ein." 
		Display="Dynamic" >
	</asp:CompareValidator>
	<br />
	<br />

	<asp:Label runat="server">Uhrzeit</asp:Label>
	<br />
	<asp:TextBox ID="txtToTime" runat="server"></asp:TextBox>
	<asp:RequiredFieldValidator id="requiredFieldValidator4" ControlToValidate="txtToTime"
		ErrorMessage="Bitte füllen Sie dieses Feld aus" 
		runat="server" Display="Dynamic"/>
	<asp:RegularExpressionValidator id=RegularExpressionValidator2 runat="server" ErrorMessage="Bitte geben Sie eine gültige Uhrzeit ein." Display="Dynamic" ControlToValidate="txtToTime" 
		ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$">
	</asp:RegularExpressionValidator>
	<br />
	 <br />
	 <br />

	<asp:Label runat="server" ID="Label5">Von der Sperrzeit ausgenommene Schüler:</asp:Label>
	<br />

	<asp:CheckBoxList ID="listStudents"	runat="server"></asp:CheckBoxList>

	<br />
	<br />
	 <br />
	 <asp:Label ID="Label3" runat="server" Text="Bemerkung"></asp:Label>
	 <br />
	 <asp:TextBox ID="txtAreaComment" runat="server" TextMode="MultiLine"></asp:TextBox>
	 <br />
	<br />

	<asp:Label ID="lblWrongDateOrder" runat="server" ForeColor="Red"></asp:Label>
	<br />

	<asp:Button type="submit" ID="btnCreate" runat="server" Text="Erstellen" OnClick="btnCreate_Click" />

</asp:Content>
