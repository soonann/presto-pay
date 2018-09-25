<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="BudgetSetUp.aspx.cs" Inherits="PrestoPay.BudgetSetUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    #divStartDate, #divEndDate 
    {
        margin-right:20px;
        float:left;
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

    .col-centered
    {
        float:none;
        margin:0 auto;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
        <asp:Label ID="lblSetUpMyBudget" runat="server" Text="Set Up My Budget" Font-Bold="True" Font-Size="XX-Large" ForeColor="#660066"></asp:Label>
        <br /><hr />

        <!-- Refer to http://getbootstrap.com/components/#alerts on using Alert -->
        <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
            <button type="button" class="close" data-dismiss="alert">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Label ID="Lbl_err" runat="server"></asp:Label>
        </asp:Panel>

    <div>
        <asp:Label ID="Label1" runat="server" Text="Choose Your Budget Date Range" Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label>
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
            <br />
            <br />
        </div>
    </div>     

    <div id="divTables" class="col-lg-1 col-centered">
        <hr />
        <asp:Label ID="Label2" runat="server" Text="Categorise and Allocate Your Budget" Font-Bold="True" Font-Size="X-Large" ForeColor="#993399"></asp:Label>
        <br /><br />
        <asp:gridview ID="GridviewBudgetSetUpIncome" runat="server" ShowFooter="true" AutoGenerateColumns="false"  OnRowCommand="DeleteRowFromGridviewBudgetSetUpIncome_RowCommand">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />

            <asp:TemplateField HeaderText="Income Category">
                <ItemTemplate>
                    <asp:TextBox ID="tbIncomeCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Income SubCategory">
                <ItemTemplate>
                    <asp:TextBox ID="tbIncomeSubCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Income Amount Allocated">
                <ItemTemplate>
                        <asp:TextBox ID="tbIncomeSubCategoryAmountAllocated" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" class="btn btn-primary" Text="Add New Row" onclick="ButtonAddNewRowToGridviewBudgetSetUpIncome_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Delete SubCategory and Amount Allocated" Text="Delete SubCategory and Amount Allocated" HeaderText="Delete SubCategory and Amount Allocated" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White"/>
            </Columns>
        </asp:gridview>
        <br />
        <br />



            <asp:gridview ID="GridviewBudgetSetUpFixedCost" runat="server" ShowFooter="true" AutoGenerateColumns="false"  OnRowCommand="DeleteRowFromGridviewBudgetSetUpFixedCost_RowCommand">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />

            <asp:TemplateField HeaderText="Fixed Cost Category">
                <ItemTemplate>
                    <asp:TextBox ID="tbFixedCostCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fixed Cost SubCategory">
                <ItemTemplate>
                    <asp:TextBox ID="tbFixedCostSubCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fixed Cost Amount Allocated">
                <ItemTemplate>
                        <asp:TextBox ID="tbFixedCostSubCategoryAmountAllocated" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" class="btn btn-primary" Text="Add New Row" onclick="ButtonAddNewRowToGridviewBudgetSetUpFixedCost_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Delete SubCategory and Amount Allocated" Text="Delete SubCategory and Amount Allocated" HeaderText="Delete SubCategory and Amount Allocated" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White"/>
            </Columns>
        </asp:gridview>
        <br />
        <br />


          <asp:gridview ID="GridviewBudgetSetUpFlexSpending" runat="server" ShowFooter="true" AutoGenerateColumns="false"  OnRowCommand="DeleteRowFromGridviewBudgetSetUpFlexSpending_RowCommand">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />

            <asp:TemplateField HeaderText="Flex Spending Category">
                <ItemTemplate>
                    <asp:TextBox ID="tbFlexSpendingCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Flex Spending SubCategory">
                <ItemTemplate>
                    <asp:TextBox ID="tbFlexSpendingSubCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Flex Spending Amount Allocated">
                <ItemTemplate>
                        <asp:TextBox ID="tbFlexSpendingSubCategoryAmountAllocated" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" class="btn btn-primary" Text="Add New Row" onclick="ButtonAddNewRowToGridviewBudgetSetUpFlexSpending_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Delete SubCategory and Amount Allocated" Text="Delete SubCategory and Amount Allocated" HeaderText="Delete SubCategory and Amount Allocated" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White"/>
            </Columns>
        </asp:gridview>
        <br />
        <br />




            <asp:gridview ID="GridviewBudgetSetUpDebtRepayment" runat="server" ShowFooter="true" AutoGenerateColumns="false"  OnRowCommand="DeleteRowFromGridviewBudgetSetUpDebtRepayment_RowCommand">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />

            <asp:TemplateField HeaderText="Debt Repayment Category">
                <ItemTemplate>
                    <asp:TextBox ID="tbDebtRepaymentCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Debt Repayment SubCategory">
                <ItemTemplate>
                    <asp:TextBox ID="tbDebtRepaymentSubCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Debt Repayment Amount Allocated">
                <ItemTemplate>
                        <asp:TextBox ID="tbDebtRepaymentSubCategoryAmountAllocated" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" class="btn btn-primary" Text="Add New Row" onclick="ButtonAddNewRowToGridviewBudgetSetUpDebtRepayment_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Delete SubCategory and Amount Allocated" Text="Delete SubCategory and Amount Allocated" HeaderText="Delete SubCategory and Amount Allocated" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White"/>
            </Columns>
        </asp:gridview>
        <br />
        <br />


    
        <asp:gridview ID="GridviewBudgetSetUpPriorityGoals" runat="server" ShowFooter="true" AutoGenerateColumns="false"  OnRowCommand="DeleteRowFromGridviewBudgetSetUpPriorityGoals_RowCommand">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />

            <asp:TemplateField HeaderText="Priority Goals Category">
                <ItemTemplate>
                    <asp:TextBox ID="tbPriorityGoalsCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priority Goals SubCategory">
                <ItemTemplate>
                    <asp:TextBox ID="tbPriorityGoalsSubCategory" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priority Goals Amount Allocated">
                <ItemTemplate>
                        <asp:TextBox ID="tbPriorityGoalsSubCategoryAmountAllocated" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" class="btn btn-primary" Text="Add New Row" onclick="ButtonAddNewRowToGridviewBudgetSetUpPriorityGoals_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Delete SubCategory and Amount Allocated" Text="Delete SubCategory and Amount Allocated" HeaderText="Delete SubCategory and Amount Allocated" ControlStyle-BackColor="#CC3300" ControlStyle-ForeColor="White"/>
            </Columns>
        </asp:gridview>
        <br />
        <br />


    <asp:Button ID="btnSaveBudgetSetUp" runat="server" class="btn btn-success" Text="Save Budget Set Up" OnClick="btnSaveBudgetSetUp_Click" />
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
