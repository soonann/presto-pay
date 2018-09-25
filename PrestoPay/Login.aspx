<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Empty.Master" CodeBehind="Login.aspx.cs" Inherits="PrestoPay.Login" %>
<%@ MasterType VirtualPath="~/Empty.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes" />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No"  />
</asp:Content>

<asp:Content ID="AlertPopout_Success_Error_Buttons" runat="server" ContentPlaceHolderID="Popout_Alert_OkButtonContent">
   <asp:Button ID="Popout_Alert_OkButton" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Ok" OnClick="Popout_Alert_OkButton_Click"  />
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



<asp:Content runat="server" ContentPlaceHolderID="head" ID="ContentHead">
     <style>
         body{
             background-color:#7B52AB;
         }
         
        .form-horizontal {
            margin-top: 220px;

        }

        .prestoTitle {
            margin-top: 20px;
            margin-bottom: 30px;
            text-align: center;
        }


    </style>
</asp:Content>
   

<asp:Content ContentPlaceHolderID="EmptyMasterPage" ID="ContentBody" runat="server">  

        <div class="container-fluid">
            <div class="col-sm-4">
         
            </div>
            <div class="col-sm-4 form-horizontal">


                <div class="form-group-sm">
                    <div class="col-sm-offset-3 col-sm-9">
                        <h1 class="prestoTitle whiteColor">PrestoPay</h1>
                    </div>

                </div>


                <div class="form-group">

                    <label class="control-label col-sm-4 whiteColor">Email:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_email" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>


                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Password:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>


                </div>

                <div class="form-group">

                    <div class="col-sm-4 col-sm-offset-4">
                        <asp:Button ID="Button_login" runat="server" Text="Login" CssClass="btn purpleButton" OnClick="Button_login_Click" />
                    </div>
                    <div class="col-sm-offset-1 col-sm-3 pull-right">
                        <a href="SignUp.aspx" class="whiteColor">Sign Up</a>
                    </div>
                </div>


            </div>
            <div class="col-sm-4">
            </div>
        </div>





    </asp:Content>
