<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PpBizLoanDetails.aspx.cs" Inherits="PrestoPay.PpBizLoanDetails" %>

<%@ MasterType VirtualPath="~/Client.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">

    <asp:Label ID="Label1" runat="server" Text="PrestoPay Biz Loan Details" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
    <hr />

    <asp:Panel ID="detailsPanel1" runat="server">
        <asp:Label ID="lblBizLoan" runat="server" Text="PrestoPay Biz Loan: " Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:Label ID="lblBizLoanId" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <br /><br />

        <asp:Label ID="AnnualSales" runat="server" Text="Annual PrestoPay Sales at time of application: $" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:Label ID="lblAnnualSales" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <br /><br />

        <asp:Label ID="LoanAmount" runat="server" Text="Loan Amount: " Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:Label ID="lblLoanAmount" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <br /><br />

        <asp:Label ID="DateOfApplication" runat="server" Text="Date Of Application: " Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:Label ID="lblDateOfApplication" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <br /><br />

        <asp:Label ID="DateOfApproval" runat="server" Text="Date Of Approval: " Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:Label ID="lblDateOfApproval" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <br /><br />

        <asp:GridView ID="IndividualLoanDetailGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <Columns>
                <asp:BoundField DataField="Repayment Percentage" HeaderText="Repayment Percentage" />
                <asp:BoundField DataField="Percentage You Keep" HeaderText="Percentage You Keep" />
                <asp:BoundField DataField="One Time Fixed Fee" HeaderText="One Time Fixed Fee" />
                <asp:BoundField DataField="Total To Be Repaid" HeaderText="Total To Be Repaid" />
                <asp:BoundField DataField="Total Amount Repaid" HeaderText="Total Amount Repaid" />
                <asp:BoundField DataField="Outstanding Amount To Be Repaid" HeaderText="Outstanding Amount To Be Repaid" />
                <asp:BoundField DataField="Repayment Status" HeaderText="Repayment Status" />
            </Columns>
            <EditRowStyle BorderStyle="Solid" />
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" BorderStyle="Solid" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
   </asp:Panel>
   <hr />

   <asp:Panel ID="repaymentPercentGraph" runat="server">
       <asp:Chart ID="RepaymentPercentChart1" runat="server" Height="321px" Width="570px">
           <Series>
               <asp:Series Name="RepaymetPercentSeries" ChartType="Doughnut" LabelBorderWidth="1" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series>
           </Series>
           <ChartAreas>
               <asp:ChartArea Name="RepaymetPercentChartArea"></asp:ChartArea>
           </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1"> </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="Loan Repayment Percentage"> </asp:Title>
            </Titles>
       </asp:Chart>
       <br />
   </asp:Panel>

    <asp:Panel ID="DailyRepaymentGraphPanel" runat="server">
        <br />


        <div id="divStartDate">
        </div>

        <div id="divEndDate">
        </div>


        <br />

    </asp:Panel>
        <asp:Button ID="BtnBack" runat="server" class="btn btn-primary" Text="Back To Loan Summary Page" OnClick="BtnBack_Click" />
        <br />

    <%--</form>--%>
</asp:Content>
