<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="RequestPage.aspx.cs" Inherits="PrestoPay.RequestPage" %>
<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes"   OnClick="Popout_Alert_Yes_Click"/>
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No" OnClick="Popout_Alert_No_Click"  />
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
            <style>
                /* id = # , class = . */
                .SendBtn {
                    background-color: rgb(123,82,171); 
                    border: 1px solid black;
                    /*border-radius: 12px;*/
                    color: white;
                    
                    text-align: center;
                    text-decoration: none;
                    display: inline-block;
                    font-size: 20px;
                    width:100%;
                }

                .RequestBtn {
                    background-color: rgb(200,154,260); 
                    border: 1px solid black;
                    /*border-radius: 12px;*/
                    color: white;
                   
                    text-align: center;
                    text-decoration: none;
                    display: inline-block;
                    font-size: 20px;
                    width:100%;
                    
                }

                .AddPayeeBtn {
                    border: 1px solid black;
                  
                     
                   
                    background-color: rgb(200,154,260);
                    
                }

                .AddRecipientBtn{
                    border: 1px solid black;
                   
                   
                   
                    background-color: rgb(200,154,260);
                }

                .Panel{
                    border: 1px solid black;
                    padding: 75px 100px;
                }

                .NoPadding{
                    padding:0;
                }

                
              #SentAmttb{
                  border-right:0;
              }
                
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <div class ="container-fluid bodyDiv">
    <div class ="row">
        <div class="col-sm-12">
        <div class ="col-sm-2">

        </div>

        <div class ="col-sm-8">
            <div class="row">
                <div class="col-sm-6 NoPadding"><asp:Button class="SendBtn" runat="server" Text="Send" OnClick="SendBtn_Click" />  </div>
                 <div class="col-sm-6 NoPadding">  <asp:Button class="RequestBtn" runat="server" Text="Request" OnClick="RequestBtn_Click" /></div>
            </div>
          
       
              <div class="row Panel">

                  
            

                <asp:Panel class="Panel" ID="SendPanel" runat="server" CssClass="col-sm-12"> <h3>Recipient:        </h3> 
                    <asp:DropDownList ID="SendDDL" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="acc_email" DataValueField="acc_email" onselectedindexchanged="SendDDL_SelectedIndexChanged"  >
                        <asp:ListItem>-- Select An Account --</asp:ListItem>
                    </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PrestoConn %>" SelectCommand="SELECT [acc_email] FROM [Receipient_records] WHERE ([usersession] = @usersession)">
                            <SelectParameters>
                                <asp:SessionParameter Name="usersession" SessionField="useremail" Type="String" />
                            </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button class="AddRecipientBtn" runat="server" Text="Add a Recipient" OnClick="Unnamed3_Click" />
                    <h3>Send Amount:</h3>
                    <div class="input-group">
                    <span class="input-group-addon">$</span>
                        
                        <asp:TextBox ID="SentAmttb" runat="server" CssClass="form-control" width="174"></asp:TextBox>
                        </div>
                        
                        <br />
                    <div><asp:Button ID="SendAmtBtn" runat="server" Text="Send Amount" CssClass="btn purpleButton" CausesValidation="true" OnClick="SendAmtBtn_Click" /></div>
                    
                    
                    

                </asp:Panel>
                <asp:Panel class="Panel" ID="RequestPanel" runat="server" Visible="false" CssClass="col-sm-12"> <h3>Payee:</h3>
                    <asp:DropDownList ID="RequestDDL" runat="server" DataSourceID="SqlDataSource2" OnSelectedIndexChanged="RequestDDL_SelectedIndexChanged" DataTextField="acc_email" DataValueField="acc_email"></asp:DropDownList>
                    <asp:Button class="AddPayeeBtn" runat="server" Text="Add a Payee" OnClick="Unnamed3_Click" />
                    
                    <h3>Request Amount:</h3>
                    <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <asp:TextBox ID="requestAmttb" runat="server" CssClass="form-control" width="174"></asp:TextBox>
                        </div>
                    <br />
                   
                    <asp:Button ID="RequestAmtBtn" runat="server" CssClass="btn purpleButton" Text="Request Amount" OnClick="RequestAmtBtn_Click" CausesValidation="true" />
                    
                    
                </asp:Panel>
              
            </div>
        
        </div>
         

        <div class ="col-sm-2">

        </div>
    </div>
   
    </div>
  

</div>
</asp:Content>
