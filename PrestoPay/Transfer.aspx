<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="PrestoPay.Transfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="Content/page-tweak.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">

     <div class="container-fluid bodyDiv">
        <div class="row container-padding">
             
            <div class="col-sm-2">

            </div>

            <div class="col-sm-6">
               
<div class=" form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-sm-3 col-sm-offset-3">Payment To:</label>
                                 <div class="col-sm-3 ">
                                    <asp:TextBox ID="TextBox_paymentTo" runat="server" CssClass="form-control" ></asp:TextBox>
                            
                                 </div>
                                 <div class="col-sm-3 "></div>

                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-3 col-sm-offset-3">Amount:</label>
                                <div class="col-sm-3 ">   
                                    <div class="input-group">
                                        <span class="input-group-addon">$</span>
                                        <asp:TextBox ID="TextBox_amount" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                   
                                 </div>
                                <div class="col-sm-3"></div>

                            </div>
                            <!--
                             <div class="form-group">
                                <label class="control-label col-sm-3 col-sm-offset-3">Purpose of payment:</label>
                                <div class="col-sm-3 ">   
                                    <asp:TextBox ID="TextBox_purpose" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                 </div>
                                <div class="col-sm-3 "></div>

                            </div>
                                -->
                            <div class="form-group">
                                <label class="control-label col-sm-3 col-sm-offset-3">Description:</label>
                                <div class="col-sm-5 ">   
                                    <asp:TextBox ID="TextBox_description" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                 </div>
                                <div class="col-sm-1 "></div>

                            </div>
                            
                            <div class="form-group">
                           
                               <div class="col-sm-6 col-sm-offset-6">
                                <asp:Button ID="Button_pay" runat="server" Text="Pay" CssClass="btn purpleButton" OnClick="Button_pay_Click" />
                                
                                <asp:Button ID="Button_cancel" runat="server" Text="Cancel" CssClass="btn cancelButton" />

                               </div>

                          
                               
                                
                             </div>
</div>

            </div>
              
             <div class="col-sm-3">

            </div>

        </div>
     </div>
</asp:Content>
