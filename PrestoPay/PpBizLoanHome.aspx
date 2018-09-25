<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PpBizLoanHome.aspx.cs" Inherits="PrestoPay.PpBizLoanHome" %>

<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <%--<form runat="server" data-spy="scroll" data-target=".navbar" data-offset="50"> --%>

        <div >
            <h1>Pay as you grow with PrestoPay Biz Loan</h1>
            <p>Try to scroll this section and look at the navigation bar while scrolling! Try to scroll this section and look at the navigation bar while scrolling!</p>
            <p>Try to scroll this section and look at the navigation bar while scrolling! Try to scroll this section and look at the navigation bar while scrolling!</p>
        </div>
        <div >
            <h1>How much can I receive?</h1>
            <p>PrestoPay Biz Loan is based on your PrestoPay sales history, so you receive an amount that fits your business.</p>
            <p>There’s no credit check when you apply, so there’s no effect on your credit score. And if you’re approved, you’ll get your funds in minutes!</p>
        </div>
        <div >
            <h1>How much does it cost?</h1>
            <p>There's just one fixed fee determined by:</p>
            <ul>
                <li>The amount of your loan.</li>
                <li>The repayment percentage you select.</li>
                <li>Your business's PayPal sales history.</li>
            </ul>  
            <p>There are no other fees and no periodic interest.</p>
            <asp:Button ID="BtnCalculator" runat="server" Text="Fee Calculator" OnClick="BtnApplyNow_Click"  class="btn btn-primary"/>
        </div>
        <div >
            <h1>How do I repay?</h1>
            <p>You pay back the loan automatically with a percentage of your sales that you choose when you apply.</p>
            <p>The higher your sales, the faster you repay. On days without sales, you won’t pay a thing, but there is a minimum requirement to pay 10% back every 90 days to keep your loan in good standing.</p>
            <p>You can also make manual payments and even pay the loan in full anytime without penalty.</p>
            <asp:Button ID="BtnApplyNow" runat="server" Text="Apply Now" OnClick="BtnApplyNow_Click" class="btn btn-success"/>
        </div>            
   <%-- </form>--%>
</asp:Content>

