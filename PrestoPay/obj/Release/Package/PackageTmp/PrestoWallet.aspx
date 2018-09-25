<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PrestoWallet.aspx.cs" Inherits="PrestoPay.PrestoWallet" %>
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
     <link href="Content/page-tweak.css" rel="stylesheet" />

    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/r/bs-3.3.5/jq-2.1.4,dt-1.10.8/datatables.min.css"/>
    <script type="text/javascript" src="https://cdn.datatables.net/r/bs-3.3.5/jqc-1.11.3,dt-1.10.8/datatables.min.js"></script>
     <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/nav-bootstrap.css" rel="stylesheet" />
    <link href="Content/popout.css" rel="stylesheet" />     
    <script src="Scripts/bootstrap.min.js"></script>
    

    <style>
        .navbar{
             border-radius:0px !important;
        }
        .pagination .active a{
             color:white;
             background-color:#7B52AB !important;
             border-color:#7B52AB !important;

        }
        .pagination li a{
            color:black;

        }
        .pagination li a:hover{
            color:black;

        }

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
     <div class="container-fluid bodyDiv">
         <style>

               .AddCreditCardBtn{
                    border: 1px solid black;
                   
                   
                   
                    background-color: rgb(200,154,260);
                }

        .walleticon{
           
        }

         </style>

   
    <div class="row">

            <div class="col-sm-2">


             </div>
            

             <div class="col-sm-8">
                        <center>
                            <span class =""><img class="walleticon" src="Images/walleticon.png" width="50" height="50" /> <h2 style ="position:relative">PresWallet       </h2></span>

                        </center> 
                    <h3>Credit Cards: </h3>
                    
                 <asp:DropDownList ID="CreditCardDDL" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="creditcardnum" DataValueField="creditcardnum" onselectedindexchanged="SendDDL_SelectedIndexChanged"  >
                        <asp:ListItem>-- Select An Account --</asp:ListItem>
                    </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PrestoConn %>" SelectCommand="SELECT [creditcardnum] FROM [creditcard] WHERE ([acc_email] = @usersession)">
                            <SelectParameters>
                                <asp:SessionParameter Name="usersession" SessionField="useremail" Type="String" />
                            </SelectParameters>
                    </asp:SqlDataSource>
                 
                        
                        
                    <asp:Button class="AddCreditCardBtn" runat="server" Text="Link a Credit Card" OnClick="CreditCardBtn_Click" />

                    <div>
                        <h3>Top Up Amount:</h3>
                        
                    <div class="input-group">
                    <span class="input-group-addon">$</span>
                         
                        <asp:TextBox ID="Amttb" runat="server" TextMode="SingleLine" CssClass="form-control" width="174"></asp:TextBox>
                        </div>
                    </div>
                 <br />
                 <div><asp:Button ID="TopUpAmtBtn" runat="server" Text="Top Up Amount" CssClass="btn purpleButton" CausesValidation="true" OnClick="TopUpAmtBtn_Click" /></div>
                 <br />
                     <table id="transactionDetails" class="table table-bordered dt-bootstrap" border="1">
                         <thead>
                             <tr>
                            
                                 <th>Email</th>
                                 <th>Date</th>
                                 <th>Description</th>
                                 <th>Receipt($)</th>
                                 <th>Payment($)</th>
                             </tr>
                         </thead>

                         <tbody>

                         </tbody>

                       
                     </table>


    
             </div>
            
         <script>
             $(document).ready(function () {

                 /*
                    <tfoot>
                            <tr>
                                 <th>Name</th>
                                 <th>Email</th>
                                 <th>Date</th>
                                 <th>Description</th>
                                 <th>Receipt($)</th>
                                 <th>Payment($)</th>
                             </tr>
                          </tfoot>
                 */
                 /*
                 $.ajax({

                     url: "Api/Payment/1",
                     type: "GET",                     
                     success: function (data) {
                         alert("Success" + JSON.stringify(data));
                     },
                     error: function (data) {
                         alert("Error" + JSON.stringify(data));
                     }
                        
                 })


                 */
                 $("#transactionDetails").DataTable({
                     
                     ajax: {
                         
                         url: 'PrestoWallet.aspx/FillTransactionTable',
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
                 /*
                 $('#transactionDetails tfoot th').each(function () {
                     var title = $(this).text();
                     $(this).html('<input type="text" placeholder="Search ' + title + '" />');
                 });

                 // DataTable
                 var table = $('#transactionDetails').DataTable();

                 // Apply the search
                 table.columns().every(function () {
                     var that = this;

                     $('input', this.footer()).on('keyup change', function () {
                         if (that.search() !== this.value) {
                             that
                                 .search(this.value)
                                 .draw();
                         }
                     });
                 });
                 */
                 /*
                 
                 $.ajax({
                     url: "Transactions.aspx/FillTransactionTable",
                     method: "post",
                     contentType: "application/json",
                    // data: "{}",
                    // dataType: "json",
                     success: function (data) {
                         console.log("Success " + JSON.stringify(data.d));

                     },
                     error: function (data) {
                         console.log("Fail " + JSON.stringify(data));

                     }


                 });
                 
                 */
             });
                 
                
                     



         </script>
       
             <div class="col-sm-2">


             </div>


         </div>
       </div> 
</asp:Content>

