<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="PrestoQrPay.aspx.cs" Inherits="PrestoPay.PrestoQrPay" %>
<%@ MasterType VirtualPath="~/Client.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes" OnClick="Popout_Alert_Yes_Click" />
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
    <asp:Button ID="Popout_Prompt_Submit" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Submit"  OnClick="Popout_Prompt_Submit_Click" />
     <asp:Button ID="Popout_Prompt_CancelBtn" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Cancel" OnClick="Popout_Prompt_CancelBtn_Click" />
</asp:Content>




 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <link href="Content/page-tweak.css" rel="stylesheet" />
        <script src="Scripts/QrCodeReader/grid.js"></script>
        <script src="Scripts/QrCodeReader/version.js"></script>
        <script src="Scripts/QrCodeReader/detector.js"></script>
        <script src="Scripts/QrCodeReader/formatinf.js"></script>
        <script  src="Scripts/QrCodeReader/errorlevel.js"></script>
        <script src="Scripts/QrCodeReader/bitmat.js"></script>
        <script  src="Scripts/QrCodeReader/datablock.js"></script>
        <script  src="Scripts/QrCodeReader/bmparser.js"></script>
        <script src="Scripts/QrCodeReader/datamask.js"></script>
        <script src="Scripts/QrCodeReader/rsdecoder.js"></script>
        <script src="Scripts/QrCodeReader/gf256poly.js"></script>
        <script  src="Scripts/QrCodeReader/gf256.js"></script>
        <script  src="Scripts/QrCodeReader/decoder.js"></script>
        <script  src="Scripts/QrCodeReader/qrcode.js"></script>
        <script  src="Scripts/QrCodeReader/findpat.js"></script>
        <script src="Scripts/QrCodeReader/alignpat.js"></script>
        <script src="Scripts/QrCodeReader/databr.js"></script>
        <script src="https://webrtc.github.io/adapter/adapter-latest.js"></script>

        <style>

            .noPadding {
            padding:0;

            }

            .font-12{
                font-size:12px;
            }

            .buttonNavItem {

                padding:0;
                margin:0;
                border:none;
                min-width:100%;
                height:35px;
                font-size:18px;
                color:white;
                text-align:center;
                background-color:#9768D1 ;

            }

            .textNav:hover{
              background-color:#9768D1 !important;
            }


            
            .textNav {
                background-color:#9768D1 !important;
                padding-top:5px;

            }

            .buttonNavItem:hover{
               background-color:#553285; 
            }



            .tabActive {
             background-color:#553285; 
            }

            .maxWidth {
                max-width:500px;
            }

            .QrHeight{
                height:500px;
            }


            
            .image-frame{
             
              
            }



            .scanbtn{
                position:relative;
                top:40%;           
                margin:0 auto;
                text-align:center;

            }


            #errorMessage{
                display:none;
            }

            .font-20{
                font-size:20px;
            }

            .left-padding-9{
                padding-left:9px;
            }

            .margin-center{
                margin:0 auto;
            }

         
            

            .videoWrapper{
                width:400px;
                height:400px;
                margin:0 auto;
                padding:0;
            }

            
            #text{
                color:white;   
                z-index:99;
                position:absolute;
                width:400px;
                height:300px;
                text-align:center;
                margin:0 auto;
                display:none;
            }

            .cover{
                background-color:rgba(0, 0, 0, 0.5);              
                z-index:100;
                position:absolute;
                height:300px;
                width:400px;
                text-align:center;
                margin:0 auto;
                left:0;
                right:0;
            }


            .videoMain{

                padding-bottom:100px
            }

            #videoElement{
                width:400px;
     
                margin:0 auto;
                padding:0;
            }

            @media (max-width: 495px) {
                      .videoWrapper{
                          width:250px;
                        height:187.5px;
                    margin:0 auto;
                    padding:0;
                }

            
                #text{
                    color:white;   
                    z-index:99;
                    position:absolute;
                            width:250px;
                    height:187.5px;
                    text-align:center;
                    margin:0 auto;
                    display:none;
                }

                .cover{
                    background-color:rgba(0, 0, 0, 0.5);              
                    z-index:100;
                    position:absolute;
                            width:250px;
                    height:187.5px;
                    text-align:center;
                    margin:0 auto;
                    left:0;
                    right:0;
                }


                .videoMain{

                    padding-bottom:100px
                }

                #videoElement{
                    width:250px;
                    height:187.5px;
                    margin:0 auto;
                    padding:0;
                }

            }

        </style>

    </asp:Content>
    
     <asp:Content ID="myContent" ContentPlaceHolderID="clientBody" runat="server">


         <asp:HiddenField ID="HiddenField_UserScannedKey" runat="server" OnValueChanged="HiddenField_UserScannedKey_ValueChanged" />


         <div class="container-fluid bodyDiv">

 
            
                <div class="col-md-8 col-md-offset-2">
                   <div class=" panel panel-info row">
                       <div class="panel-heading col-sm-12 noPadding " >
                           <asp:Panel ID="Panel_NavbarBoth" runat="server">
                           <div class="col-sm-6 noPadding">
                               <asp:Button ID="Button_ScanTab" runat="server"  CssClass="buttonNavItem fullWidth" Text="Pay" OnClick="Button_ScanTab_Click" UseSubmitBehavior="False"  />
                           </div>
                           
                           <div class="col-sm-6 noPadding">
                               <asp:Button ID="Button_PayTab" runat="server" Text="Receive"  CssClass="buttonNavItem fullWidth" OnClick="Button_PayTab_Click" UseSubmitBehavior="False" />
                           </div>
                          </asp:Panel>
                             <asp:Panel ID="Panel_NavbarOne" runat="server">
                                 <div class="buttonNavItem fullWidth textNav">Receive</div>
                              </asp:Panel>
                       </div>
                       <div class=" panel-body col-sm-12">
                      <asp:Panel ID="Panel_Pay" runat="server">
                           

                            <canvas id="qr-canvas" style="border:solid 1px black;display:none">

                            </canvas>
                            <div class =" col-sm-12 text-center">
                                <div class="row videoMain">
                                    <div class="row bottomMargin font-20 maxWidth text-center center-block">
                                        <div class="page-header">
                                            <p>Scan your recepient's Presto Qr Code to make payment to them !</p>
                                        </div>
                                    </div>
                       
                                    <div class="videoWrapper">
                                        <div id="cover" class="cover">
                                            <div id="errorMessage" class="scanbtn whiteColor">
                                                Your browser does not support this function !

                                            </div>
                                                <button id="button_ScanQrCode" runat="server" onclick="startScanning(); return false;"  class="scanbtn btn purpleButton whiteColor" >
                                                    Scan

                                                </button> 

                                        </div>
                                        <div id="text">
                                            <span class="text-center left-padding-9">
                                                Scanning ....

                                            </span>
                                            <br/> 
                                            <button class="btn purpleButton whiteColor" onclick="stopScanning();return false;">Cancel</button>

                                        </div>
                                        
                                        <video autoplay="true" id="videoElement" src="Images/Rolling.gif" >

                                        </video>
                                        
                                    </div>
                                   


                                </div>
                                
                            
                            </div>
                        
                            <script src="Scripts/execScanner.js"></script>


                    </asp:Panel>


                     <asp:Panel ID="Panel_Receive"  Visible="false" runat="server">
                         <div class="col-sm-12">
                             <div class="row  text-center">
                                   <div class="row  font-20 maxWidth text-center center-block">
                                    <div class="page-header">

                                           Let your payee scan the Presto QR Code to make payment to you or click here to request with QR Pay  
                                          
                                        <asp:Button ID="Button_RequestTab" runat="server" Text="Request"  CssClass="btn purpleButton"  OnClick="Button_RequestTab_Click" UseSubmitBehavior="False" />
                                            
                                        </div>
                                       
                                       </div>
                                 <div class="row">
                                     
                                          <span class=" font-12">click on the QR code to download it.</span>
                                   
                                      <a   id="staticurl" runat="server" href="#" target="_blank" download>
                                       <asp:Image ID="Image_QrCode" runat="server"  ImageUrl="~/Images/Rolling.gif" CssClass="fullWidth maxWidth center-block text-center margin-center " />
                                 </a>
                                 </div>
                               
                             </div>
                        
                         </div>
                         
                       


                         
                    </asp:Panel>
                    

                    <asp:Panel ID="Panel_RequestAmount"  Visible="false" runat="server">
                         <div class="col-sm-12">
                             <div class="row  text-center">
                                  <div class="row bottomMargin font-20">
                                      <div class="col-sm-8  col-sm-offset-2">
                                            <asp:Button ID="Button_Back" runat="server" Text="Back"  OnClick="Button_Back_Click" CssClass="btn purpleButton" />
                                   <div class="page-header">

                                       Enter the amount you want to be paid and request to let your payee scan the code !
                                     
                                   </div>
                                      </div>

                                      <div class="col-sm-2">


                                      </div>
                                  </div>
                                 <div class="maxWidth QrHeight  margin-center text-center">
                                       
                                             <span class=" font-12">click on the QR code to download it.</span>
                                     
                                      <a   id="requestUrl" runat="server" href="#" target="_blank" download>
                                        <asp:Image ID="Image_Request" runat="server" CssClass="fullWidth maxWidth" />
                                       </a>
                                    
                                 </div>
                              
                             
                             
                              <div class="row">
                              
                                <div class=" col-sm-4 col-sm-offset-4">
                                    <div class="input-group">
                                        <span class="input-group-addon">$</span>
                                        <asp:TextBox ID="TextBox_Request" runat="server" CssClass="form-control"></asp:TextBox>
                                      </div>  
                                </div>
                                <div class="col-sm-4">
                                </div>

                            </div>
                                

                             </div>
                             <div class="row  container-padding text-center">
                                  <asp:Button ID="Button_RequestAmount" runat="server" Text="Request" CssClass="btn purpleButton whiteColor" OnClick="Button_RequestAmount_Click" />
                             </div>
                            
                         </div>
                         
                       


                         
                    </asp:Panel>


                </div>
                         </div>

                </div>

                    <div class="col-md-2 ">

                    </div>


                </div>
           
       
            
      

     </asp:Content>