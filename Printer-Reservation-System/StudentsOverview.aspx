<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsOverview.aspx.cs" Inherits="Printer_Reservation_System.StudentsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <asp:GridView ID="gridviewRegistrations" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="gridviewRegistrations_PageIndexChanging" OnRowCommand="gridviewRegistrations_RowCommand" >
        <Columns>  
            <asp:BoundField DataField="Name" HeaderText="Name" />  
            <asp:BoundField DataField="Vorname" HeaderText="Vorname" />  
            <asp:BoundField DataField="E-Mail" HeaderText="Mail" />  
            <asp:BoundField DataField="Handy" HeaderText="Handy" />  
            <asp:Buttonfield buttontype="button" Text="Annehmen" commandname="accept"  />
            <asp:Buttonfield buttontype="button" Text="Ablehnen" commandname="deny"  />

        </Columns>
    </asp:GridView>

    <asp:GridView ID="gridviewStudents" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="gridviewStudents_PageIndexChanging" OnRowCancelingEdit="gridviewStudents_RowCancelingEdit" OnRowDeleting="gridviewStudents_RowDeleting" OnRowEditing="gridviewStudents_RowEditing" OnRowUpdating="gridviewStudents_RowUpdating">
        <Columns>  
            <asp:BoundField DataField="Name" HeaderText="Name" />  
            <asp:BoundField DataField="Vorname" HeaderText="Vorname" />  
            <asp:BoundField DataField="E-Mail" HeaderText="Mail" />  
            <asp:BoundField DataField="Handy" HeaderText="Handy" />  
            <asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />  


            <asp:BoundField DataField="Status" HeaderText="Status" />  
            <asp:CheckBoxField DataField="Admin" HeaderText="Admin" />

            <asp:CommandField ShowEditButton="true" />  
            <asp:CommandField ShowDeleteButton="true" /> 
        </Columns>
    </asp:GridView>

    <asp:Label ID="lbltest" runat="server" Text="testlabel"></asp:Label>

</asp:Content>
