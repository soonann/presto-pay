<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSummary.aspx.cs" Inherits="MerchantWebsite.PaymentSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MerchantWebsite</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
      <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />
    <link href="Content/page-tweak.css" rel="stylesheet" />
    <style>

           body{

            background-color:#337ab7;
        }

           .topPadding{
              padding-top:200px;
          }

          .table-noBorder{
            margin:10px;
            width:100%;
            margin:0 auto;
            margin-bottom:10px;
            
            
          }

          .yellow{
              color:gold;
          }

          .green{
              color:limegreen;
          }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid topPadding">

            <div class="col-lg-12">

                <div class="col-lg-8 col-lg-offset-2">

                     <div class=" panel panel-info">

                    <div class="panel-heading">
                      Order Summary
                     
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12">
                      
                            
                             <asp:Table ID="Table_Items" runat="server" CssClass="table-noBorder">
                                   
                               
                            </asp:Table>
                       </div>
                        <div class="row">
                            <div class="col-sm-offset-8 col-sm-4">
                               <asp:Button ID="Button_Pay" runat="server" Text="Pay with PrestoPay"  CssClass="btn purpleButton pull-right"  OnClick="Button_Click" />
                            </div>
                        </div>
                       
                   


                   </div> 
                    </div>
                    


                </div>

                 <div class="col-lg-2">



                </div>

            </div>


        </div>
    </form>
</body>
</html>
