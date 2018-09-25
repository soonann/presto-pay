<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="PopulateTransaction.aspx.cs" Inherits="PrestoPay.PopulateTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyMasterPage" runat="server">

    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Populate" OnClick="Button1_Click" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Popout_Alert_OkButtonContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Popout_YesNoButton" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Popout_Textbox" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Popout_SubmitAndCancelButton" runat="server">
</asp:Content>
