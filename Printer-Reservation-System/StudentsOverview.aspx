<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsOverview.aspx.cs" Inherits="Printer_Reservation_System.StudentsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    /* AutoGenerateColumns="false" DataKeyNames="id" */
    <asp:GridView ID="gridviewStudents" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="gridviewStudents_PageIndexChanging" OnRowCancelingEdit="gridviewStudents_RowCancelingEdit" OnRowDeleting="gridviewStudents_RowDeleting" OnRowEditing="gridviewStudents_RowEditing" OnRowUpdating="gridviewStudents_RowUpdating">
        <Columns>  
            <asp:BoundField DataField="Name" HeaderText="Name" />  
            <asp:BoundField DataField="Vorname" HeaderText="Vorname" />  
            <asp:BoundField DataField="E-Mail" HeaderText="Mail" />  
            <asp:BoundField DataField="Handy" HeaderText="Handy" />  
            <asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />  
            <asp:BoundField DataField="Status" HeaderText="Status" />  
            <asp:BoundField DataField="Beschreibung" HeaderText="Beschreibung" />  
            <asp:BoundField DataField="Admin" HeaderText="Admin" />  

            <asp:CommandField ShowEditButton="true" />  
            <asp:CommandField ShowDeleteButton="true" /> 
        </Columns>
    </asp:GridView>

    <asp:Label ID="lbltest" runat="server" Text="Label"></asp:Label>

</asp:Content>
