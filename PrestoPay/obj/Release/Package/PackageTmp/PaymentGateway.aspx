<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" EnableSessionState="True" AutoEventWireup="true" CodeBehind="PaymentGateway.aspx.cs" Inherits="PrestoPay.PaymentGateway" %>
<%@ MasterType VirtualPath="~/Empty.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" OnClick="Popout_Alert_Yes_Click" Text="Yes" />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" OnClick="Popout_Alert_OkButton_Click" UseSubmitBehavior="false" Text="No"  />
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


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        
        .progress{
            width:200px;
            height:20px;
            border:1px solid #ddd;
            background-color:white;
            margin:0 auto;
            margin-top:10px;
        }
        .progress-bar{
             background-color:#7B52AB !important;
        }

        body {
            background-color: #7B52AB;
        }

        .logout-pos {
            position: relative;
            top: -3px;
        }

        .mainBgc{
            background-color:#7B52AB;
        }

        .roundBorders{
            border-radius:5px;
        }

        .payButtons{
            width:30px;
            height:30px;
        }
        .topMargin{
            margin-top:15px;
        }

        .valign-text-top{
            vertical-align:text-top;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyMasterPage" runat="server">


    <div class="container-fluid bodyDiv">
        <div class="row container-padding">
            <div class="col-md-1 ">
            </div>

            <div class="col-md-5">
                <div class=" panel panel-info">

                    <div class="panel-heading">
                        <span class="whiteColor clearMargin boldText">Payment Description </span>

                    </div>
                    <div class="panel-body">

                        <asp:Table ID="TblPaymentSummary" runat="server" CssClass="fullWidth bottomMargin text-center  table-noBorder">

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" CssClass="text-center">
                                    <span class="boldOnly">Payment to: </span>
                                    <asp:Label ID="Label_PaymentTo" runat="server" Font-Underline="True"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2"> &nbsp;</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow CssClass="subTotalRow ">
                                <asp:TableCell CssClass="boldOnly leftAlignText">Description</asp:TableCell>
                                <asp:TableCell CssClass="boldOnly rightAlignText">Amount</asp:TableCell>
                            </asp:TableRow>




                        </asp:Table>


                    </div>
                </div>
            </div>

            <div class="col-md-5 ">
                <div class=" panel panel-info">

                    <div class="panel-heading">

                        <asp:Label ID="Label_panelTitle" runat="server" Text="Login" CssClass="whiteColor clearMargin boldText"></asp:Label>
                        <button runat="server" class="btn purpleButton pull-right logout-pos" id="Button_signout" onserverclick="Button_logout_Click" visible="false">
                            <span class="glyphicon glyphicon-log-out"></span>Log Out

                        </button>
                    </div>
                    <div class="panel-body">




                        <asp:Panel ID="Panel_Login" runat="server" CssClass="center-block" Visible="true">
                            <h1 class="text-center bottomMargin ">PrestoPay</h1>

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Email:</label>
                                    <div class="col-sm-8 ">
                                        <asp:TextBox ID="TextBox_email" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-1 "></div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Password:</label>
                                    <div class="col-sm-8 ">
                                        <asp:TextBox ID="TextBox_password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 "></div>

                                </div>

                                <div class="form-group">

                                    <div class="col-sm-4 col-sm-offset-3">
                                        <asp:Button ID="Button_login" runat="server" Text="Login" CssClass="btn purpleButton" OnClick="Button_login_Click" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-3 pull-right">
                                     
                                    </div>
                                </div>


                            </div>


                        </asp:Panel>

                        <asp:Panel ID="Panel_Payment" runat="server" Visible="false">
                            <div class="row text-center container-padding">
                                <span class="boldOnly">You are logged in as:</span>
                                <asp:Label ID="Label_LoggedInAs" runat="server" Text="" Font-Underline="True"></asp:Label>
                            </div>
                         
                           


                                <asp:Panel ID="Panel_ChoosePaymentMethod"  Visible="true" CssClass="fullWidth" runat="server">

                                      <div class="col-sm-10 col-sm-offset-1">

                                             <div class="text-center row">
                                                    <h2 class="text-center">Pay with:</h2>
                                             </div>

                                    <div class="row" >
                                    
                                    <div class="col-md-6 text-center topMargin " >
                                                 <button id="Button_WalletPay" onserverclick ="Button_WalletPay_ServerClick" class=" btn purpleButton whiteColor" runat="server" >
                                                 <span class="whiteColor">
                                                     <img src="Images/wallet.png" class="payButtons"/>
                                                     Wallet Value
                                                 </span>
                                               </button>
                                          
                                            
                                       </div>

                                   
                                        <div class="col-md-6 text-center topMargin">

                                        
                                              <button id="Button_CreditCardPay"  onserverclick="Button_CreditCardPay_ServerClick" class=" btn purpleButton whiteColor" runat="server" >
                                                 <span class="whiteColor">
                                                     <img src="Images/credit-card.png" class="payButtons"/>
                                                     Credit Card
                                                 </span>
                                               </button>
                                       </div>
    
                                    </div>
                                 
                                  </div>
                                   
                            

                                <div class="col-sm-1">
                                </div>
                                </asp:Panel>


                              

                            

                            
                        <asp:Panel ID="Panel_CardChosen"  Visible="false" CssClass=" fullWidth" runat="server">
                                
                                      <div class="col-sm-10 col-sm-offset-1">

                                   <div class="row" >
                                    
                                    <div class="col-md-10 text-center topMargin col-md-offset-1 " >
                                              
                                        <div class="form-horizontal">

                                            <div class="form-group">

                                                <label class="col-sm-4 control-label">Credit Card: </label> 
                                                    
                                                <div class="col-sm-8">

                                                
                                                <asp:DropDownList ID="DropDownList_CreditCard" EnableViewState="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                             <div class="row topMargin">

                                                 <button id="Button_PayWithCC"  onserverclick="Button_MakePaymentCC_ServerClick"  class=" btn purpleButton whiteColor" runat="server" >
                                                 <span class="whiteColor">
                                                     <img src="Images/credit-card.png" class="valign-text-top" width="15" height="15"/>
                                                     Pay
                                                 </span>
                                               </button>
                                                 <asp:Button runat="server"  ID="Button_CreditcardCancel"  OnClick="Button_Cancel_Click" CssClass="btn" Text="Cancel" />

                                             </div>
                                        </div>
                                       

                                        

                                       </div>
    
                                    </div>
                                 
                                  </div>
                                   
                            

                                <div class="col-sm-1">
                                </div>
                       </asp:Panel>

                              


                        <asp:Panel ID="Panel_WalletChosen" CssClass="fullWidth" Visible="false" runat="server">

                           



                               
                                    
                                    <div class="text-center topMargin fullWidth " >
                                              
                                        <table class=" fullWidth table-noBorder">

                                            <tr class="subTotalRow">
                                                 <td class="boldOnly leftAlignText">
                                                  Current Wallet Balance:
                                                </td>
                                                <td class=" boldOnly rightAlignText">
                                                    <asp:Label ID="Label_CurrentWalletBal" runat="server" Text=""></asp:Label>
                                                </td>

                                            </tr>
                                            <tr class="subTotalRow">
                                                <td class="boldOnly leftAlignText">
                                                   Payment Amount:
                                                </td>
                                                <td class="  rightAlignText">
                                                    <asp:Label ID="Label_PaymentAmount" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                              <tr class=" grandTotalRow">
                                                <td class="boldOnly leftAlignText">
                                                   Wallet Balance After Payment:
                                                </td>
                                                <td class="boldOnly  rightAlignText">
                                                    <asp:Label ID="Label_BalanceAftPayment" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                        <div class="row topMargin">

                                            <button id="Button_MakePayment"  onserverclick="Button_MakePayment_ServerClick" class=" btn purpleButton whiteColor" runat="server" >
                                                 <span class="whiteColor">
                                                     <img src="Images/wallet.png" class="valign-text-top" width="15" height="15"/>
                                                     Pay
                                                 </span>
                                               </button>
                                            <asp:Button runat="server"  ID="Button_Cancel"  OnClick="Button_Cancel_Click" CssClass="btn" Text="Cancel" />

                                        </div>

                                    </div>
    
                            
                                 
                       
                                   
                            

                              
                                

                        </asp:Panel>

                        </asp:Panel>



                    </div>

                </div>

            </div>


            <div class="col-md-1">
            </div>






        </div>

    </div>

</asp:Content>
