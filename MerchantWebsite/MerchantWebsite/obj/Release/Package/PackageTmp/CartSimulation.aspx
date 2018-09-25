<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartSimulation.aspx.cs" Inherits="MerchantWebsite.CartSimulation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MerchantWebsite</title>
   <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/page-tweak.css" rel="stylesheet" />

    <style>
          body{

            background-color:#337ab7;
        }


          .topPadding{
              padding-top:100px;
          }

          .table-noBorder{
            margin:10px;
            width:100%;
            margin:0 auto;
            margin-bottom:10px;
            
            
          }
    </style>
       <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

</head>
<body>
    <h1 class="whiteColor text-center  topPadding">Shop Fresh</h1>
    <form runat="server" class="form-horizontal topPadding "   >
         <asp:ScriptManager ID="ScriptManager1"  EnablePageMethods="true" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanelSimulation">
           <ContentTemplate>
        <div class="row">

            
            <div class=" col-sm-8 col-sm-offset-2 ">
                <div class="col-sm-12">
                      
                   <div class="col-lg-6">
                 
                       
                       <div class=" panel panel-info">

                    <div class="panel-heading">
                      Cart Simulation
                     
                    </div>
                    <div class="panel-body">
                        
                             <asp:Table ID="Table_Items" runat="server" CssClass="table-noBorder">
                                   


                            </asp:Table>
                       
                        <div class="row">
                            <div class="col-sm-offset-8 col-sm-4">
                                <asp:Button ID="Button_Pay" runat="server" Text="Checkout" CssClass="btn btn-primary" OnClick="Button_Checkout_Click" />
                            </div>
                        </div>
                       
                   


                   </div> 
                    </div>
                       </div>
                    <div class="col-lg-6">
                    <div class=" panel panel-info">

                    <div class="panel-heading">
                        Add New Item
                     
                    </div>
                    <div class="panel-body">
                   <div class="form-horizontal">
                        <div class="form-group">

                                <label class="control-label col-sm-4 ">Description</label>
                                <div class="col-sm-8 ">   
                                    <asp:TextBox ID="TextBox_DescriptionNew" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                

                          </div>
                    
                        <div class="form-group">

                                <label class="control-label col-sm-4 ">Price</label>
                                <div class="col-sm-8 ">   
                                    <asp:TextBox ID="TextBox_PriceNew" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                

                            </div>
                   
                            
                            <div class="form-group">
                           
                               <div class="col-sm-4 col-sm-offset-4">
                                    <asp:Button ID="Button_AddNew" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="Button_AddNew_Click" />
                               </div>
                                  <div class="col-sm-offset-1 col-sm-3 pull-right">
                                      
                                  </div>
                             </div>
                        
                          


                    </div>

                        </div>



                        </div>
                  </div>
                    </div>
                  
                </div>
                  <div class="col-sm-2">

                  </div>

            
            </div>
         </ContentTemplate>
             </asp:UpdatePanel>
          

    </form>
</body>
</html>