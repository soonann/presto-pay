<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="PrestoPay.SignUp" %>
<%@ MasterType VirtualPath="~/Empty.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes"   />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No"  />
</asp:Content>

<asp:Content ID="AlertPopout_Success_Error_Buttons" runat="server" ContentPlaceHolderID="Popout_Alert_OkButtonContent">
   <a href="Login.aspx" class=" btn btn-default" >OK</a>
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
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyMasterPage" runat="server">
     <div class="container-fluid">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-4 form-horizontal">


                <div class="form-group-sm">
                    <div class="col-sm-offset-3 col-sm-9">
                        <h1 class="prestoTitle whiteColor">PrestoPay</h1>
                    </div>

                </div>


                <div class="form-group" >

                    <label class="control-label col-sm-4 whiteColor">Email:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_email" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You are required to field an email" ControlToValidate="TextBox_email" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>


                </div>
                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Password:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please field in a password" ControlToValidate="TextBox_password" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>


                </div>

                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Confirm Password:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_Cfmpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password must be the same!" ControlToCompare="TextBox_password" ControlToValidate="TextBox_Cfmpassword"  ForeColor="Red"></asp:CompareValidator>
                    </div>


                </div>

                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Name:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_name" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You are required to field a name" ControlToValidate="TextBox_name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                    <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Date of birth:</label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox_dob" runat="server" CssClass="form-control" type ="date" ></asp:TextBox>
                    </div>
                        </div>

                <div class="form-group">
                    <label class="control-label col-sm-4 whiteColor">Account type:</label>
                    <div class="col-sm-8 ">
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            <asp:ListItem>Personal</asp:ListItem>
                            <asp:ListItem>Business</asp:ListItem>
                        </asp:DropDownList> 
                    </div>
                        </div>
                 

<div class="form-group" id ="business">
                    <label class="control-label col-sm-4 whiteColor">
                        <asp:Label ID="lblcompanyname" runat="server" Text="CompanyName:"></asp:Label></label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="companynametb" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="You are required to field a name" ControlToValidate="TextBox_name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                <div class="form-group" id ="business">
                    <label class="control-label col-sm-4 whiteColor">
                        <asp:Label ID="Label2" runat="server" Text="Business type:"></asp:Label></label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="You are required to field a Business Type" ControlToValidate="TextBox_name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                <div class="form-group" id ="business">
                    <label class="control-label col-sm-4 whiteColor">
                        <asp:Label ID="Label1" runat="server" Text="Business category:"></asp:Label></label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="You are required to field a Business category" ControlToValidate="TextBox_name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                <div class="form-group" id ="business">
                    <label class="control-label col-sm-4 whiteColor">
                        <asp:Label ID="Label3" runat="server" Text="Country of Regulation:"></asp:Label></label>
                    <div class="col-sm-8 ">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="You are required to field a Country" ControlToValidate="TextBox_name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                <div class="form-group">

                    <div class="col-sm-4 col-sm-offset-4">
                        <asp:Button ID="Button_signup" runat="server" Text="Sign Up" CssClass="btn purpleButton" OnClick="Button1_Click" />
                    </div>
                    
                </div>



            </div>
            <div class="col-sm-4">
            </div>
        </div>

    
            
           
            
        
</asp:Content>

