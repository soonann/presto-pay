<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="BudgetFlexSpendingDetails.aspx.cs" Inherits="PrestoPay.BudgetFlexSpendingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
    #idBudgetFlexSpendingChart, #idBudgetFlexSpendingDetails 
    {
        margin-right:20px;
        float:left;
    }

    #idBudgetFlexSpendingChart, label
    {
        margin-left:20px;
    }

    #idBudgetFlexSpendingDetails{
        float:left;
        margin-right:10px;
    }

    #idBudgetFlexSpendingSubCategoryChart 
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
        <asp:Label ID="Label2" runat="server" Text=" Flex Spending Details" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>

    <hr />

    <div id="idBudgetFlexSpendingChart">
        <asp:Label ID="lblBudgetFlexSpending" runat="server" Text="My Flex Spending Overall Spending" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
        <asp:Chart ID="BudgetFlexSpendingChart" runat="server" Width="305px" OnClick="BudgetFlexSpendingChart_Click">
            <Series>
                <asp:Series Name="BudgetFlexSpendingSeries" ChartType="Pie" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="BudgetFlexSpendingChartArea"></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="">
                </asp:Legend>
            </Legends>
        </asp:Chart>
        <hr />
    </div>
    <br />


    <div id="idBudgetFlexSpendingDetails">
            <asp:Label ID="TotalFlexSpendingAllocated" runat="server" Text="Total Flex Spending Allocated: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalFlexSpendingAllocated" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />

            <asp:Label ID="TotalFlexSpendingSpent" runat="server" Text="Total Flex Spending Spent: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalFlexSpendingSpent" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />

            <asp:Label ID="TotalFlexSpendingLeftOver" runat="server" Text="Total Flex Spending Left To Spend: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblTotalFlexSpendingLeftOver" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
            <br />
            <br />

            <asp:Label ID="lblPercentageSpent1" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <asp:Label ID="lblPercentageSpent2" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblYouAreInThe" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

            <br />
    </div>

    <div id="idBudgetFlexSpendingSubCategoryChart">
        <asp:Label ID="lblBudgetFlexSpendingSubCategory" runat="server" Text="My Flex Spending SubCategory Overall Spending"  ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
        <asp:Chart ID="BudgetFlexSpendingSubCategoryChart" runat="server" Height="280px" Width="838px" OnClick="BudgetFlexSpendingSubCategoryChart_Click">
            <Series>
                <asp:Series Name="BudgetFlexSpendingSubCategorySeriesLeftToSpend" Label="Left To Spend" LegendText="Left To Spend" ChartType="StackedColumn" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                <asp:Series Name="BudgetFlexSpendingSubCategorySeriesSpent" Label="Spent" LegendText="Spent" ChartType="StackedColumn" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="BudgetFlexSpendingSubCategoryChartArea"></asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" Title="">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </div>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetSetUpFlexSpending" runat="server" Text="My Flex Spending Amount Allocated" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetSetUpFlexSpending" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
            <asp:BoundField DataField="Flex Spending Category" HeaderText="Flex Spending Category" />
            <asp:BoundField DataField="Flex Spending SubCategory" HeaderText="Flex Spending SubCategory" />
            <asp:BoundField DataField="Flex Spending Amount Allocated" HeaderText="Flex Spending Amount Allocated" />
        </Columns>
    </asp:gridview>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetExpenditureFlexSpending" runat="server" Text="My Flex Spending Spending" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetExpenditureFlexSpending" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Expenditure ID" HeaderText="Expenditure ID" />
            <asp:BoundField DataField="Flex Spending Category" HeaderText="Flex Spending Category" />
            <asp:BoundField DataField="Flex Spending SubCategory" HeaderText="Flex Spending SubCategory" />
            <asp:BoundField DataField="Flex Spending Amount Spent" HeaderText="Flex Spending Amount Spent" />
            <asp:BoundField DataField="Expenditure Date" HeaderText="Expenditure Date" />
            <asp:BoundField DataField="Expenditure Remarks" HeaderText="Expenditure Remarks" />
        </Columns>
    </asp:gridview>
    <br />
    <br />

    <asp:Label ID="lblGridviewBudgetTransactionFlexSpending" runat="server" Text="My Flex Spending Transactions" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />
    <asp:gridview ID="GridviewBudgetTransactionFlexSpending" runat="server" ShowFooter="false" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Transaction ID" HeaderText="Transaction ID" />
            <asp:BoundField DataField="Flex Spending Category" HeaderText="Flex Spending Category" />
            <asp:BoundField DataField="Flex Spending SubCategory" HeaderText="Flex Spending SubCategory" />
            <asp:BoundField DataField="Flex Spending Amount Spent" HeaderText="Flex Spending Amount Spent" />
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
