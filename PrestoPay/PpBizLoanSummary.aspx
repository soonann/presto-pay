<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PpBizLoanSummary.aspx.cs" Inherits="PrestoPay.PpBizLoanSummary" %>

<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <%--<form runat="server">--%>

        <asp:Label ID="Label1" runat="server" Text="PrestoPay Biz Loan Summary" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <hr />

        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel><br />

        <asp:Panel ID="panelBizLoanApplicationpageBIZID" runat="server">
            <asp:Label ID="BizID" runat="server" Text="Business ID: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="tbBizID" runat="server" ReadOnly="true"></asp:TextBox>

            <asp:Button ID="BtnBizIDSubmit" class="btn btn-primary" runat="server" Text="Submit" Visible="false" OnClick="BtnBizIDSubmit_Click" />
            <br />
            <asp:Label ID="BizCompanyName" runat="server" Text="Company Name: "  Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblBizCompanyName" runat="server"  Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br /><br /><br />

            <asp:Button ID="btnGetLoanSummaryByBizId" runat="server" class="btn btn-primary" Text="Get Loan Summary" Visible="false" OnClick="btnGetLoanSummaryByBizId_Click"  />
        </asp:Panel> <br /><hr />

        <asp:Panel ID="panelBizLoanApplicationpageQN" runat="server">
            <br />
            <asp:Label ID="LoanApplications" runat="server" Text="Your Loan Applications: " Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label><br />
         <asp:GridView ID="LoanRepaymentSummaryGridView" runat="server" AutoGenerateColumns="False" 
             BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1070px" OnRowCommand="LoanRepaymentSummaryGridView_RowCommand1" >
               <Columns>
                    <asp:BoundField  DataField="Loan Id" HeaderText="Loan Id" />
                    <asp:BoundField DataField="Application Date" HeaderText="Application Date" />                 
                    <asp:BoundField DataField="Application Status" HeaderText="Application Status" />
                   <asp:BoundField DataField="Repayment Rate" HeaderText="Repayment Rate" />
                    <asp:BoundField DataField="Total Amount To Be Repaid" HeaderText="Total Amount To Be Repaid" /> 
                   <asp:BoundField DataField="Total Amount Repaid" HeaderText="Total Amount Repaid" /> 
                   <asp:BoundField DataField="Repayment Status" HeaderText="Repayment Status" />            
                   <asp:ButtonField CommandName="SELECT_LOAN_DETAILS" Text="View Loan Details" HeaderText="View Loan Details" ControlStyle-ForeColor="#009933" />
                   <asp:ButtonField CommandName="SELECT_LOAN_REPAYMENT" Text="View Loan Repayment" HeaderText="View Loan Repayment" ControlStyle-ForeColor="#009933" />
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
    <%--</form>--%>
</asp:Content>
