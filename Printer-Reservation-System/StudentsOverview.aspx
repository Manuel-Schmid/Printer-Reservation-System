<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentsOverview.aspx.cs" Inherits="Printer_Reservation_System.StudentsOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    /* AutoGenerateColumns="false" DataKeyNames="id" */
    <asp:GridView ID="gridviewStudents" runat="server" OnPageIndexChanging="gridviewStudents_PageIndexChanging" OnRowCancelingEdit="gridviewStudents_RowCancelingEdit" OnRowDeleting="gridviewStudents_RowDeleting" OnRowEditing="gridviewStudents_RowEditing" OnRowUpdating="gridviewStudents_RowUpdating">
        <Columns>  
            <asp:CommandField ShowEditButton="true" />  
            <asp:CommandField ShowDeleteButton="true" /> 
        </Columns>
    </asp:GridView>

</asp:Content>
