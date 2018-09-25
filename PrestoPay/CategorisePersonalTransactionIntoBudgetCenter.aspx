<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="CategorisePersonalTransactionIntoBudgetCenter.aspx.cs" Inherits="PrestoPay.CategorisePersonalTransactionIntoBudgetCenter" %>

<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <%--<form runat="server">--%>

        <asp:Label ID="Label1" runat="server" Text="Categorise My Personal Transactions Into Budget Center" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label><br />
        <hr />

        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>


        </asp:Panel><br />

        <asp:Panel ID="PanelAddCashTransaction" runat="server">
            <div class="col-sm-4 ">
            <asp:Label ID="lblNameOfTransaction" runat="server" Text="Name Of Transaction(Remarks): " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="tbNameOfTransaction" runat="server" Font-Size="Medium"></asp:TextBox>
            <br /><br />

            <asp:Label ID="lblAmountForTransaction" runat="server" Text="Amount: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="tbAmountForTransaction" runat="server" Font-Size="Medium"></asp:TextBox>
            <br /><br />

            <asp:Label ID="DateOfTransaction" runat="server" Text="Date: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblDateOfTransaction" runat="server" Text="" Font-Size="Medium"></asp:Label>
            <asp:Calendar ID="CalendarDateOfTransaction" runat="server" OnSelectionChanged="CalendarDateOfTransaction_SelectionChanged"></asp:Calendar>            
            <br /><br />

            <asp:Label ID="lblCategory" runat="server" Text="Category: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Font-Size="Medium" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
            <br /><br />

            <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="ddlSubCategory" Font-Size="Medium" runat="server">
            </asp:DropDownList>
            <br /><br /><br />

            <asp:Button ID="BtnSubmit" runat="server" class="btn btn-success" Text="Submit" OnClick="BtnSubmit_Click" />
            <asp:Button ID="BtnCancel" runat="server" class="btn btn-primary" Text="Cancel" />
           </div>
        </asp:Panel>

   <%-- </form>--%>
</asp:Content>
<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes" />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default"  UseSubmitBehavior="false" Text="No"  />
</asp:Content>

<asp:Content ID="AlertPopout_Success_Error_Buttons" runat="server" ContentPlaceHolderID="Popout_Alert_OkButtonContent">
   <asp:Button ID="Popout_Alert_OkButton" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Ok"   />
</asp:Content>

<asp:Content ID="PromptPopout_Textbox" runat="server" ContentPlaceHolderID="Popout_Textbox">

    <div class="input-group">
        <span class="input-group-addon">$</span>
        <asp:TextBox ID="Popout_Prompt_Textbox" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
</asp:Content>

<asp:Content ID="PromptPopout_Submit_Cancel_Buttons" runat="server" ContentPlaceHolderID="Popout_SubmitAndCancelButton">
    <asp:Button ID="Popout_Prompt_Submit" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Submit"  />
     <asp:Button ID="Popout_Prompt_CancelBtn" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Cancel" />
</asp:Content>
