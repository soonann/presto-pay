<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="AddPayeePage.aspx.cs" Inherits="PrestoPay.AddPayeePage" %>
<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes"   />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No"  />
</asp:Content>

<asp:Content ID="AlertPopout_Success_Error_Buttons" runat="server" ContentPlaceHolderID="Popout_Alert_OkButtonContent">
    <asp:Button ID="Popout_Alert_OkButton" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Ok"  OnClick="Popout_Alert_OkButton_Click"  />
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
     <div class="container-fluid">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-4 form-horizontal">


                <div class="form-group-sm">
                    <div class="col-sm-offset-3 col-sm-9">
                        <h1 class="prestoTitle whiteColor">Link an Account</h1>
                    </div>

                </div>


                <div class="form-group">

                    <label class="control-label col-sm-4 whiteColor">Payee Email Address: </label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_ccnum" runat="server" CssClass="form-control"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You are required to field a credit card number" ControlToValidate="TextBox_ccnum" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>


                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">First Name:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_fname" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You are required to field a name" ForeColor="Red" ControlToValidate="TextBox_fname"></asp:RequiredFieldValidator>
                    </div>


                </div>

                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Last Name</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_lname" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You are required to field a namer" ForeColor="Red" ControlToValidate="TextBox_lname"></asp:RequiredFieldValidator>
                    </div>


                </div>

                

                    


                <div class="form-group">

                    <div class="col-sm-4 col-sm-offset-4">
                        <asp:Button ID="Button_login" runat="server" Text="Link credit Card" CssClass="btn purpleButton" OnClick="Button1_Click" />
                    </div>
                    
                </div>


            </div>
            <div class="col-sm-4">
            </div>
        </div>

    
</asp:Content>

