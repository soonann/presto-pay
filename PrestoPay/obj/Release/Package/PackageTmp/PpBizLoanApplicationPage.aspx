<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PpBizLoanApplicationPage.aspx.cs" Inherits="PrestoPay.PpBizLoanApplicationPage" %>

<%@ MasterType VirtualPath="~/Client.Master" %>


<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false"  OnClick="Popout_Alert_Yes_Click"  Text="Yes" />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default"  UseSubmitBehavior="false"  OnClick="Popout_Alert_No_Click" Text="No"  />
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


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <%--<form runat="server">--%>

        <asp:Label ID="Label1" runat="server" Text="PrestoPay Biz Loan Application" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
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
            <asp:TextBox ID="tbBizID" runat="server" ReadOnly="true" Font-Bold="True" Font-Size="Medium"></asp:TextBox>

            <asp:Button ID="BtnBizIDSubmit" runat="server" class="btn btn-primary" Text="Submit" Visible="false" OnClick="BtnBizIDSubmit_Click" />
            <br />
            <asp:Label ID="BizCompanyName" runat="server" Text="Company Name: " Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblBizCompanyName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br /><br /><br />

            <asp:Button ID="btnGetLoanLimitsAndStatusByBizId" runat="server" class="btn btn-primary" Text="Get Loan Limits and Status" Visible="false" OnClick="btnGetLoanLimitsAndStatusByBizId_Click" />
        </asp:Panel> <hr /><br />

        <asp:Panel ID="panelBizLoanApplicationpageQN" runat="server">
            <asp:Label ID="AverageAnnualSales" runat="server" Text="Your Loan Amount Limits: " Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label><br />
            
            <asp:GridView ID="LoanLimitsGridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" >
              <Columns>
                    <asp:BoundField  DataField="Average Annual Sales" HeaderText="Average Annual Sales" />
                    <asp:BoundField DataField="Maximum Loan Amount Allowed" HeaderText="Maximum Loan Amount Allowed" />
                    <asp:BoundField DataField="Total Loan Amount Approved" HeaderText="Total Loan Amount Approved" />
                    <asp:BoundField DataField="Loan Amount Available" HeaderText="Loan Amount Available" /> 
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
            <asp:Label ID="LoanStatus" runat="server" Text="Your Loan Repayment Status: " Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label><br />
         <asp:GridView ID="LoanRepaymentStatusGridView" runat="server" AutoGenerateColumns="False" 
             BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
               <Columns>
                    <asp:BoundField  DataField="PENDING" HeaderText="PENDING" />
                    <asp:BoundField DataField="FULL" HeaderText="FULL" />
                    <asp:BoundField DataField="OUTSTANDING" HeaderText="OUTSTANDING" />
                    <asp:BoundField DataField="REJECTED" HeaderText="REJECTED" /> 
                   <asp:BoundField DataField="CANCELLED" HeaderText="CANCELLED" /> 
                   <asp:BoundField DataField="OTHERS" HeaderText="OTHERS" /> 
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
            <asp:Label ID="lbDesiredLoanAmount" runat="server" Text="Enter Desired Loan Amount:" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="tbDesiredLoanAmount" runat="server"></asp:TextBox>
            <asp:Button ID="btnCalculateLoanAmount" runat="server"  Text="Calculate" class="btn btn-primary" Visible="true" OnClick="btnCalculateLoanAmount_Click" />
        </asp:Panel><hr /><br />

         <asp:GridView ID="GridViewLoanCalculation" runat="server" AutoGenerateColumns="False" Height="164px" Width="792px" 
             BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanged="GridViewLoanCalculation_SelectedIndexChanged">
            <Columns>
                <asp:BoundField  DataField="Repayment percentage (%)" HeaderText="Repayment Percentage" />
                <asp:BoundField DataField="Percentage you keep (%)" HeaderText="Percentage You Keep" />
                <asp:BoundField DataField="One-time fixed fee ($)" HeaderText="One Time Fixed Fee" />
                <asp:BoundField DataField="Total to be repaid ($)" HeaderText="Total Amount To Be Paid" />
                <asp:CommandField HeaderText="Apply For Loan" ShowSelectButton="True" SelectText="Apply For Loan" ControlStyle-ForeColor="#009933" />
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
        <asp:Button ID="BtnViewLoanSummary" runat="server" class="btn btn-primary" Text="View Business Loan Summary" OnClick="BtnViewLoanSummary_Click" />
   <%-- </form>--%>
</asp:Content>
