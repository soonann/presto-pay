<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="BudgetDebtRepaymentDetails.aspx.cs" Inherits="PrestoPay.BudgetDebtRepaymentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
    #idBudgetDebtRepaymentChart, #idBudgetDebtRepaymentDetails 
    {
        margin-right:20px;
        float:left;
    }

    #idBudgetDebtRepaymentChart, label
    {
        margin-left:20px;
    }

    #idBudgetDebtRepaymentDetails{
        float:left;
        margin-right:10px;
    }

    #idBudgetDebtRepaymentSubCategoryChart 
    {
        clear:both;
        width:inherit;
        height:auto;
        position:static;       
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    
        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel>

        <asp:Label ID="lblMyBudget" runat="server" Text="My Budget " Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <asp:Label ID="lblbudgetId" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large" ForeColor="#990099"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=" Debt Repayment Details" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>

    <hr />

    <div id="idBudgetDebtRepaymentChart">
        <asp:Label ID="lblBudgetDebtRepayment" runat="server" Text="My Debt Repayment Overall Spending" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
        <asp:Chart ID="BudgetDebtRepaymentChart" runat="server" Width="305px" OnClick="BudgetDebtRepaymentChart_Click">
            <Series>
                <asp:Series Name="BudgetDebtRepaymentSeries" ChartType="Pie" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="BudgetDebtRepaymentChartArea"></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="">
                </asp:Legend>
            </Legends>
        </asp:Chart>
        <hr />
    </div>
    <br />


    <div id="idBudgetDebtRepaymentDetails">
            <asp:Label ID="TotalDebtRepaymentAllocated" runat="server" Text="Total Debt Repayment Allocated: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalDebtRepaymentAllocated" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />

            <asp:Label ID="TotalDebtRepaymentSpent" runat="server" Text="Total Debt Repayment Spent: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalDebtRepaymentSpent" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />

            <asp:Label ID="TotalDebtRepaymentLeftOver" runat="server" Text="Total Debt Repayment Left To Spend: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalDebtRepaymentLeftOver" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />
            <br />

            <asp:Label ID="lblPercentageSpent1" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblPercentageSpent2" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblYouAreInThe" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

            <br />
    </div>

    <div id="idBudgetDebtRepaymentSubCategoryChart">
        <asp:Label ID="lblBudgetDebtRepaymentSubCategory" runat="server" Text="My Debt Repayment SubCategory Overall Spending"  ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
        <asp:Chart ID="BudgetDebtRepaymentSubCategoryChart" runat="server" Height="280px" Width="838px" OnClick="BudgetDebtRepaymentSubCategoryChart_Click">
            <Series>
                <asp:Series Name="BudgetDebtRepaymentSubCategorySeriesLeftToSpend" Label="Left To Spend" LegendText="Left To Spend" ChartType="StackedColumn" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                <asp:Series Name="BudgetDebtRepaymentSubCategorySeriesSpent" Label="Spent" LegendText="Spent" ChartType="StackedColumn" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="BudgetDebtRepaymentSubCategoryChartArea"></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetSetUpDebtRepayment" runat="server" Text="My Debt Repayment Amount Allocated" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetSetUpDebtRepayment" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
            <asp:BoundField DataField="Debt Repayment Category" HeaderText="Debt Repayment Category" />
            <asp:BoundField DataField="Debt Repayment SubCategory" HeaderText="Debt Repayment SubCategory" />
            <asp:BoundField DataField="Debt Repayment Amount Allocated" HeaderText="Debt Repayment Amount Allocated" />
        </Columns>
    </asp:gridview>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetExpenditureDebtRepayment" runat="server" Text="My Debt Repayment Spending" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetExpenditureDebtRepayment" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Expenditure ID" HeaderText="Expenditure ID" />
            <asp:BoundField DataField="Debt Repayment Category" HeaderText="Debt Repayment Category" />
            <asp:BoundField DataField="Debt Repayment SubCategory" HeaderText="Debt Repayment SubCategory" />
            <asp:BoundField DataField="Debt Repayment Amount Spent" HeaderText="Debt Repayment Amount Spent" />
            <asp:BoundField DataField="Expenditure Date" HeaderText="Expenditure Date" />
            <asp:BoundField DataField="Expenditure Remarks" HeaderText="Expenditure Remarks" />
        </Columns>
    </asp:gridview>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetTransactionDebtRepayment" runat="server" Text="My Debt Repayment Transactions" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetTransactionDebtRepayment" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Transaction ID" HeaderText="Transaction ID" />
            <asp:BoundField DataField="Debt Repayment Category" HeaderText="Debt Repayment Category" />
            <asp:BoundField DataField="Debt Repayment SubCategory" HeaderText="Debt Repayment SubCategory" />
            <asp:BoundField DataField="Debt Repayment Amount Spent" HeaderText="Debt Repayment Amount Spent" />
            <asp:BoundField DataField="Transaction Date" HeaderText="Transaction Date" />
            <asp:BoundField DataField="Transaction Type" HeaderText="Transaction Type" />
            <asp:BoundField DataField="Transaction From" HeaderText="Transaction From" />
            <asp:BoundField DataField="Transaction To" HeaderText="Transaction To" />
            <asp:BoundField DataField="Transaction Remarks" HeaderText="Transaction Remarks" />

        </Columns>
    </asp:gridview>
    <br />
    <br />
    <div id="idBtnBackToBudgetSummary">
        <asp:Button ID="BtnBackToBudgetSummary" runat="server" class="btn btn-primary" Text="Back To Budget Summary Page" OnClick="BtnBackToBudgetSummary_Click"/>
    </div>
    <br />

    <div id="idBtnBackToBudgetDashBoard">
        <asp:Button ID="BtnBackToBudgetDashBoard" runat="server" class="btn btn-primary" Text="Back To Budget DashBoard Page" OnClick="BtnBackToBudgetDashBoard_Click"/>
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
