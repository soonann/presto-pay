<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PpBizLoanRepaymentDetails.aspx.cs" Inherits="PrestoPay.PpBizLoanRepaymentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    #divStartDate, #divEndDate, #idBack 
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

    #divTables, #idBack
    {
        clear:both;
        width:inherit;
        height:auto;
        position:static;       
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">

    <asp:Label ID="Label1" runat="server" Text="PrestoPay Loan " Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
    <asp:Label ID="lblBizLoanId" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large" ForeColor="#990099"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text=" Repayment Details" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
    <hr />

            <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel><br />

        <asp:Panel ID="DailyRepaymentGraphPanel" runat="server">
        <asp:Label ID="lblChooseDailyRepaymentDateRange" runat="server" Text="Choose Daily Repayment Date Range" ForeColor="#993399" Font-Bold="True" Font-Size="X-Large"></asp:Label><br />

        <div id="divStartDate">
            <asp:Label ID="StartDateLabel" runat="server" Text="Start Date:" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
            <asp:Calendar ID="StartDateCalendar" runat="server" OnSelectionChanged="StartDateCalendar_SelectionChanged" OnVisibleMonthChanged="StartDateCalendar_VisibleMonthChanged"></asp:Calendar>
        </div>

        <div id="divEndDate">
            <asp:Label ID="EndDateLabel" runat="server" Text="End Date:" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
            <asp:Calendar ID="EndDateCalendar" runat="server" OnSelectionChanged="EndDateCalendar_SelectionChanged" OnVisibleMonthChanged="EndDateCalendar_VisibleMonthChanged"></asp:Calendar>
        </div>

       <div id="divTables">
        <asp:Button ID="BtnSubmitDateRange" runat="server" class="btn btn-primary" Text="Show Daily Loan Repayment" Visible="false" OnClick="BtnSubmitDateRange_Click" /><br />

        <asp:Chart ID="DailyRepaymentGraphChart" runat="server" Height="385px" Width="687px">
            <Series>
                <asp:Series Name="DailyRepaymentGraphSeries1" ChartType="Column" ChartArea="DailyRepaymentGraphSeriesChartArea" Legend="DailyRepaymentGraphLegend"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="DailyRepaymentGraphSeriesChartArea">
                    <AxisY Title="Total Loan Repayment Amount ($)">
                    </AxisY>
                    <AxisX Title="Repayment Date">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="RepaymentGraphTitle" Alignment="TopCenter" Text="Daily Total Loan Repayment Amount"> </asp:Title>
            </Titles>
        </asp:Chart>
        <br />
        <br />

        <div id="idBack">
            <asp:Button ID="BtnBack" runat="server" class="btn btn-primary" Text="Back To Loan Summary Page" OnClick="BtnBack_Click" />
        </div>
        
        <br />
        </div>
        </asp:Panel>

</asp:Content>
