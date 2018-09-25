<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="CategorisePrestoPayTransactionIntoBudgetCenter.aspx.cs" Inherits="PrestoPay.CategorisePrestoPayTransactionIntoBudgetCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
    #divStartDate, #divEndDate 
    {
        margin-right:20px;
        float:left;
    }

    #divStartDate, label
    {
        margin-left:20px;
    }

    #divEndDate{
        float:left;
        margin-right:10px;
    }

    #divTables 
    {
        clear:both;
        width:inherit;
        height:auto;
        position:static;       
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">

        <asp:Label ID="lblMyPrestoPayTransactions" runat="server" Text="My PrestoPay Transactions" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
         <br /><hr /><br />

        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel><br />



    <asp:Label ID="Label1" runat="server" Text="Select Start And End Dates To Categorise Your Transactions" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    <br />


        <div id="divStartDate">
            <asp:Label ID="lblStartDateLabel" runat="server" Text="Start Date:" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
            <asp:Calendar ID="StartDateCalendar" runat="server" OnSelectionChanged="StartDateCalendar_SelectionChanged"></asp:Calendar>
        </div>

        <div id="divEndDate">
            <asp:Label ID="lblEndDateLabel" runat="server" Text="End Date:" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
            <asp:Calendar ID="EndDateCalendar" runat="server" OnSelectionChanged="EndDateCalendar_SelectionChanged"></asp:Calendar>
        </div>

    <br />


    


    <div id="divTables">
            <br />
            <br />  
        <asp:Button ID="btnSubmit" runat="server" class="btn btn-success" Text="Submit" OnClick="btnSubmit_Click" /><br /><br /><hr />
        <asp:Panel ID="TransactionTablePanel" runat="server">
        <asp:GridView ID="TransactionTableGridView" runat="server" AutoGenerateColumns="False" 
             BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1070px" OnRowCommand="TransactionTableGridView_RowCommand">
            <Columns>
                <asp:BoundField  DataField="Transaction Id" HeaderText="Transaction Id" />
                <asp:BoundField DataField="Transaction Amount" HeaderText="Transaction Amount" />                 
                <asp:BoundField DataField="Transaction Description" HeaderText="Transaction Description" />
                <asp:BoundField DataField="Transaction Type" HeaderText="Transaction Type" />
                <asp:BoundField DataField="Transaction From" HeaderText="Transaction From" /> 
                <asp:BoundField DataField="Transaction To" HeaderText="Transaction To" /> 
                <asp:BoundField DataField="Transaction Date" HeaderText="Transaction Date" />

                <asp:BoundField DataField="Budget Category" HeaderText="Budget Category" /> 
                <asp:BoundField DataField="Budget SubCategory" HeaderText="Budget SubCategory" />

                <asp:ButtonField CommandName="CATEGORISE" Text="Categorise Transaction" HeaderText="Categorise Transaction" ControlStyle-ForeColor="#009933"/>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </asp:Panel>
    </div>


    


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
