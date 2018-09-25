<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="CategorisePrestoPayTransactionIntoBudgetCenterDetails.aspx.cs" Inherits="PrestoPay.CategorisePrestoPayTransactionIntoBudgetCenterDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">


    <asp:Label ID="lblCategoriseMyPrestoPayTransaction" runat="server" Text="Categorise My PrestoPay Transactions" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
    <br /><hr /><br /><br />

    <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
    <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
        <button type="button" class="close" data-dismiss="alert">
            <span aria-hidden="true">&times;</span>
        </button>
        <asp:Label ID="Lbl_err" runat="server"></asp:Label>
    </asp:Panel>

    <asp:Label ID="Label1" runat="server" Text="Details of Transaction " Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_id" runat="server" Text="Transaction Id: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_id" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_amt" runat="server" Text="Transaction Amount: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_amt" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_description" runat="server" Text="Transaction Description: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_description" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_type" runat="server" Text="Transaction Type: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_type" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_from" runat="server" Text="Transaction From: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_from" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_to" runat="server" Text="Transaction To: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_to" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="trans_date" runat="server" Text="Transaction Date: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="lblTrans_date" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="Label2" runat="server" Text="Category: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="old_Category" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br />

    <asp:Label ID="Label4" runat="server" Text="SubCategory: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:Label ID="old_SubCategory" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br /><br /><br />
    <hr />

    <asp:Label ID="Label8" runat="server" Text="Select Category and SubCategory to Update Your Transaction:" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    <br /><br />

    <asp:Label ID="lblCategory" runat="server" Text="Category: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList>
    <br /><br />

    <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category: " Font-Bold="True" Font-Size="Medium"></asp:Label>
    <asp:DropDownList ID="ddlSubCategory" runat="server">
    </asp:DropDownList>
    <br /><br /><br />

    <asp:Button ID="BtnSubmitCategorisedTransaction" runat="server" class="btn btn-success" Text="Submit Categorised Transaction" OnClick="BtnSubmitCategorisedTransaction_Click" />

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
