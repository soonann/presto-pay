<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="BudgetCenterDashBoard.aspx.cs" Inherits="PrestoPay.BudgetCenterDashBoard" %>

<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    #idBudgetExpenditureChart, #idBudgetDetails 
    {
        margin-right:20px;
        float:left;
    }

    #idBudgetExpenditureChart
    {
        margin-left:20px;
    }

    #idBudgetDetails{
        float:left;
        margin-right:200px;
    }

    #divCharts 
    {
        clear:both;
        width:inherit;
        height:auto;
        position:static;       
    }

    #idBudgetFixedCostChart, #idBudgetFlexSpendingChart, #idBudgetDebtRepaymentChart, #idBudgetPriorityGoalsChart
    {
        margin-right:20px;
        float:left;
    }

    #idBudgetFixedCostChart
    {
        margin-left:20px;
    }

    #idBudgetFlexSpendingChart{
        float:left;
        margin-right:10px;
    }

    #idBudgetDebtRepaymentChart{
    float:left;
    margin-right:10px;
}

    #idBudgetPriorityGoalsChart{
    float:left;
    margin-right:10px;
}

    #idBtnBack {
    clear:both;
    width:inherit;
    height:auto;
    position:static;  
    margin-top:50px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <%--<form runat="server">--%>

        <asp:Label ID="lblMyBudget" runat="server" Text="My Budget " Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <asp:Label ID="lblbudgetId" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large" ForeColor="#990099"></asp:Label>
        <asp:Label ID="Label1" runat="server" Text=" DashBoard" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <br />
        <hr />


        <asp:Panel ID="PanelBudgetSummary" runat="server">

        <div>
                <div id="idBudgetExpenditureChart">
                    <asp:Chart ID="BudgetExpenditureChart" runat="server" OnClick="BudgetExpenditureChart_Click" Height="323px" Width="323px">
                        <Series>
                            <asp:Series Name="BudgetExpenditureSeries" ChartType="Doughnut" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="BudgetExpenditureChartArea"></asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Legend1" Title="My Budget">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="My Budget"> </asp:Title>
                        </Titles>
                    </asp:Chart>
                </div>

                <div id="idBudgetDetails">

                    <asp:Label ID="TotalExpenditureAllocated" runat="server" Text="Total Expenditure Allocated: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <asp:Label ID="lblTotalExpenditureAllocated" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
                    <br />

                    <asp:Label ID="TotalExpenditureSpent" runat="server" Text="Total Expenditure Spent: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <asp:Label ID="lblTotalExpenditureSpent" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
                    <br />

                    <asp:Label ID="TotalExpenditureLeftOver" runat="server" Text="Total Expenditure Left To Spend: " Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <asp:Label ID="lblTotalExpenditureLeftOver" runat="server" Text="Label"  Font-Size="X-Large"></asp:Label>
                    <br />
                    <br />

                    
                    <asp:Label ID="lblPercentageSpent1" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <asp:Label ID="lblPercentageSpent2" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <br />
                    <br />

                    <asp:Label ID="lblYouAreInThe" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                    <br />
                </div>
                <hr />
        </div>

        <div id="divCharts">                           
            <div id="idBudgetFixedCostChart">
                <asp:Chart ID="BudgetFixedCostChart" runat="server" OnClick="BudgetFixedCostChart_Click" Width="305px">
                    <Series>
                        <asp:Series Name="BudgetFixedCostSeries" ChartType="Doughnut" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="BudgetFixedCostChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="My Fixed Costs">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="My Fixed Costs"> </asp:Title>
                    </Titles>
                </asp:Chart>
            </div>

            <div id="idBudgetFlexSpendingChart">
                <asp:Chart ID="BudgetFlexSpendingChart" runat="server" OnClick="BudgetFlexSpendingChart_Click" Width="291px">
                    <Series>
                        <asp:Series Name="BudgetFlexSpendingSeries" ChartType="Doughnut" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="BudgetFlexSpendingChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="My Flex Spending">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="My Flex Spending"> </asp:Title>
                    </Titles>
                </asp:Chart>
            </div>

            <div id="idBudgetDebtRepaymentChart">
                <asp:Chart ID="BudgetDebtRepaymentChart" runat="server" OnClick="BudgetDebtRepaymentChart_Click">
                    <Series>
                        <asp:Series Name="BudgetDebtRepaymentSeries" ChartType="Doughnut" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="BudgetDebtRepaymentChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="My Debt Repayment">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="My Debt Repayment"> </asp:Title>
                    </Titles>
                </asp:Chart>
            </div>

            <div id="idBudgetPriorityGoalsChart">
                <asp:Chart ID="BudgetPriorityGoalsChart" runat="server" OnClick="BudgetPriorityGoalsChart_Click">
                    <Series>
                        <asp:Series Name="BudgetPriorityGoalsSeries" ChartType="Doughnut" Legend="Legend1" LabelPostBackValue="#VALX" LegendPostBackValue="#VALX" PostBackValue="#VALX"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="BudgetPriorityGoalsChartArea"></asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" Title="My Priority Goals">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="My Priority Goals"> </asp:Title>
                    </Titles>
                </asp:Chart>
            </div>

        </div>

        <div id="idBtnBack">
           <asp:Button ID="BtnBack" runat="server" class="btn btn-primary" Text="Back To Budget Summary Page" OnClick="BtnBack_Click"  />
        </div>


        </asp:Panel>
    <%--</form>--%>
</asp:Content>
