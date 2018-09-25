<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="PrestoPay.DashBoard" %>
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
     
    <div class="container-fluid bodyDiv">


   
    <div class="row">

            <div class="col-sm-2">


             </div>
            

             <div class="col-sm-8">
                
                 <div> 
                     <center>
                 <asp:Chart ID="Chart1" runat="server" >
                     <Titles>
        <asp:Title Text="Spendings">
        </asp:Title>
    </Titles>
    <Series>
        <asp:Series Name="Series1"  ChartArea="ChartArea1">
            
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1">
            <AxisX Title="Date">
            </AxisX>
            <AxisY Title="Amount">
            </AxisY>
        </asp:ChartArea>
    </ChartAreas>
                     </asp:Chart>
                         </center>
                    </div>

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
                         
                         url: 'DashBoard.aspx/FillTransactionTable',
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
<asp:Content ID="Content3" ContentPlaceHolderID="Popout_Alert_OkButtonContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Popout_YesNoButton" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Popout_Textbox" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Popout_SubmitAndCancelButton" runat="server">
</asp:Content>
