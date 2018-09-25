<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="PrestoPay.Notification" %>
<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes"   />
    <asp:Button ID="Popout_Alert_No" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="No"  />
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

     <link href="Content/page-tweak.css" rel="stylesheet" />

    <style>
         .NotificationBtn {
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

          .Panel{
                    border: 1px solid black;
                    padding: 75px 100px;
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
                <div><asp:Button class="NotificationBtn" runat="server" Text="Notification" />  </div>
                 
            </div>
          
       
              <div class="row Panel">

                  
            

                <asp:Panel class="Panel" ID="NotificationPanel" runat="server" CssClass="col-sm-12"> 
                  
                    <asp:Label ID="notificationlbl" runat="server" Text="You just made a transaction"></asp:Label>  
                    
                    
                    

                </asp:Panel>
                
                     <script>
             $(document).ready(function () {

                 


               
                 $("#transactionDetails").DataTable({
                     
                     ajax: {
                         
                         url: 'Transactions.aspx/FillTransactionTable',
                         dataSrc: function (data) {
                             //console.log(JSON.stringify(data.d.data));
                             return data.d.data;
                             /// check for null value
                         },                                         
                         type: 'POST',
                         contentType: 'application/json; charset=utf-8',
                         
                     },
                     
                     paging: true,
                     columns: [
                    
                         { data: "Email" },
                         { data: "Date" },
                         { data: "Description" },
                         {
                             'data': "Receipt",
                             'render': function (data) {
                                 if (parseFloat(data) <= 0) 
                                     return " ";
                                 else                                
                                    return data;
                             }
                         },
                         {
                             'data': "Payment", 'render' : function(data) {
                                 if (parseFloat(data) <= 0)
                                     return " ";
                                 else
                                     return data;
                                 
                             }
                         }
                
                     ],
                     autoWidth: false

                 });
               
             });
                 
                
                     



         </script>
                    
               
              
            </div>
        
        </div>
         

        <div class ="col-sm-2">

        </div>
    </div>
   
    </div>
  

</div>
</asp:Content>
