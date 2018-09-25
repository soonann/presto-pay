<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="BudgetSummary.aspx.cs" Inherits="PrestoPay.BudgetSummary" %>
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

        <asp:Label ID="lblMyBudgetSummary" runat="server" Text="My Budget Summary" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <br /><hr /><br />

        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel>

        <div id="label">
            <asp:Label ID="Label1" runat="server" Text="Select Start And End Dates To Filter Your Budget Summary" Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label>
            <br /><br />
        </div>

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

    <div id="divTables" class="col-lg-1 col-centered">
        <asp:Button ID="btnShowBudgetSummary" runat="server" class="btn btn-primary" Text="Show Budget Summary" OnClick="btnShowBudgetSummary_Click" /><br />
        <hr />
        <asp:Panel ID="panelBudgetSummary" runat="server">
            <br />
            <asp:Label ID="BudgetSummary1" runat="server" Text="Budget Summary Table " Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label><br /><br />
         <asp:GridView ID="BudgetSummaryGridView" runat="server" AutoGenerateColumns="False" 
             BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1070px" OnRowCommand="BudgetSummaryGridView_RowCommand1" >
               <Columns>
                   <asp:BoundField  DataField="budget_id" HeaderText="Budget Id" />
                   <asp:BoundField DataField="budget_startDate" HeaderText="Start Date" />                 
                   <asp:BoundField DataField="budget_endDate" HeaderText="End Status" />

                   <asp:BoundField DataField="budget_incomeAmountAllocated" HeaderText="Income Allocated" />
                   <asp:BoundField DataField="budget_incomeAmountReceived" HeaderText="Income Received" /> 

                   <asp:BoundField DataField="budget_fixedCostAmountAllocated" HeaderText="Fixed Cost Allocated" /> 
                   <asp:BoundField DataField="budget_fixedCostAmountSpent" HeaderText="Fixed Cost Spent" />  

                   <asp:BoundField DataField="budget_flexSpendingAmountAllocated" HeaderText="Flex Spending Allocated" />            
                   <asp:BoundField DataField="budget_flexSpendingAmountSpent" HeaderText="Flex Spending Spent" />     
                   
                   <asp:BoundField DataField="budget_debtRepaymentAmountAllocated" HeaderText="Debt Repayment Allocated" />            
                   <asp:BoundField DataField="budget_debtRepaymentAmountSpent" HeaderText="Debt Repayment Spent" />   
                   
                   <asp:BoundField DataField="budget_priorityGoalsAmountAllocated" HeaderText="Priority Goals Allocated" />            
                   <asp:BoundField DataField="budget_priorityGoalsAmountSpent" HeaderText="Priority Goals Spent" />   
                   
                   <asp:BoundField DataField="budget_totalExpenditureAmountAllocated" HeaderText="Total Expenditure Allocated" /> 
                   <asp:BoundField DataField="budget_totalExpenditureAmountSpent" HeaderText="Total Expenditure Spent" />  

                   <asp:BoundField DataField="budget_totalExpenditureAmountLeftOver" HeaderText="Left Over Amount" />    
                   
                   <asp:ButtonField CommandName="SHOW_BUDGET_DASHBOARD" Text="Show DashBoard" HeaderText="Show DashBoard" ControlStyle-ForeColor="#009933" />
                   <asp:ButtonField CommandName="SHOW_BUDGET_DETAILS" Text="Show Budget" HeaderText="Show Budget" ControlStyle-ForeColor="#009933" />
                   <asp:ButtonField CommandName="DELETE_BUDGET_DETAILS" Text="Delete Budget" HeaderText="Delete Budget" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White" />

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
            <br />
            <br />
        </asp:Panel><br />
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
