<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="PrestoPay.Settings"  %>
<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes"  OnClick="Popout_Alert_Yes_Click" />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No"  OnClick="Popout_Alert_No_Click" />
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
    <asp:Button ID="Popout_Prompt_Submit" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Submit"   />
     <asp:Button ID="Popout_Prompt_CancelBtn" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Cancel" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Content/page-tweak.css" rel="stylesheet" />

    <style>
        .shift-center{
            margin:0 auto;
            text-align:center;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">

    <div class="container-fluid bodyDiv">


        <asp:Panel ID="Panel" runat="server">
            <div class="col-sm-4 col-sm-offset-4">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="page-header">
                            <h2>Add new employees</h2>
                            <p>You can add employees with Presto Pay personal accounts for them to request payment with QR pay on your behalf.</p>
                        </div>
                        <div class="row">
                             <div class="form-horizontal">

                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        Email:
                                    </label>
                                    <div class="col-sm-6">
                                    <asp:TextBox ID="TextBox_Request" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    <div>
                                        <asp:Button ID="Button_SendRequest" runat="server"  OnClick="Button_SendRequest_Click" Text="Send Request" CssClass="btn purpleButton" />
                                    </div>
                            </div>
                           </div>
                        </div>

                        <div class="row">
                             <div class="page-header"">
                                    <h2>Sent Requests</h2>
                              </div>
                             <asp:GridView ID="GridView_Request"  OnRowCommand="GridView_Request_RowCommand" EmptyDataText="No requests found" CssClass="table table-bordered"   OnRowDataBound="GridView_Request_RowDataBound" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" >

                                 <Columns>
                                     <asp:BoundField DataField="Request_recipient" HeaderText ="Email"></asp:BoundField>
                                     <asp:BoundField DataField="Request_DateTime" HeaderText ="Date & Time Sent"></asp:BoundField>
                                     <asp:BoundField DataField="Request_State" HeaderText="Status"></asp:BoundField>
                                     <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton_Delete"  CommandArgument='<%# Eval("RequestId")%>' CommandName="DeleteRequest"  CssClass="btn btn-danger" runat="server">X</asp:LinkButton>
                                         </ItemTemplate>

                                     </asp:TemplateField>
                                 </Columns>

                             </asp:GridView>

                        </div>

                        <div class="page-header"">
                            <h2>Employees</h2>
                        </div>

                        <div class="row">
                            <div class="col-sm-6 col-sm-offset-6">
                                <div class="form-horizontal">

                                <div class="form-group">

                                   
                                    <div class="col-sm-9">
                                     
                                     </div>
                                </div>
                            </div>
                         </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-12">

                                <asp:GridView ID="GridView_Employee" CssClass="table table-bordered"   runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" >


                              
                                </asp:GridView>

                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>



            </div>


            <div class="col-sm-4">

            </div>
        </asp:Panel>



    </div>

</asp:Content>
