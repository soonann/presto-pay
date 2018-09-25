<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="PrestoPayAnalysis.aspx.cs" Inherits="PrestoPay.PrestoPayAnalysis" %>

<%@ MasterType VirtualPath="~/Empty.Master" %>

<asp:Content ID="AlertPopout_Confirm_Buttons" runat="server" ContentPlaceHolderID="Popout_YesNoButton">
    <asp:Button ID="Popout_Alert_Yes" runat="server" CssClass=" btn btn-default" UseSubmitBehavior="false" Text="Yes" />
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
    <style>
        .GV_table{
            width:90%;
            border-color:transparent;

        }

        .GV_header th{
            margin:0 auto;
            text-align:center;
            color:white;
            background-color:white;
            border-bottom:2px solid #ddd;
        }
        .GV_header th a{
            
            color:black;
            
        }

        .GV_pager{

        }


         body{
           
           

         }

         .graphPanel{
             background-color:white;
         border:1px solid #ddd!important;
         height:600px;

         padding-top:30px !important;

         }
         .otherPanel{
             background-color:white;
         }

         .detailsPanel{
             background-color:white;
             width:95%;
             float:left;
             padding-top:30px;
             height:600px;
             border:1px solid #ddd!important;
          
         }

         .chart-middle{
             margin:0 auto;
             text-align:center;
         }

            
         .bigButtons {
              all:unset;
              width:90%;
              height:100px;
             margin:0 auto;
             padding:10px;
          background-color:#553285;
       
             text-align:center;

            }

         .bigButtons:hover{
             background-color:#36175E;
     
             cursor:pointer;
         }

          .bottom-spacing{
              margin-bottom:20px;
          }


          .dashboard-items{
              /*
              border:1px solid #e7ecf1!important;
                  */
              padding:0;
            

          }

          .monthyear-filter{
              padding-top:10px;
              padding-bottom:10px;
              
          }
           
        
          .item-one{
              float:left;
              

          }
          .item-two{
              margin:0 auto;
              text-align:center;
          }
          .item-three{
              float:right;
          }


          .big-text{
              font-size:40px;
              color:white;
          }


          .indus-button{

          }

          .busi-button{
           
          }

          .pers-button{

          }


          .GV_right{
              text-align:right;
              padding-right:40px !important;
          }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EmptyMasterPage" runat="server">
       <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">PrestoPay</a>
                </div>
                <div class="collapse navbar-collapse overflowfix" id="myNavbar">
                    <ul class="nav navbar-nav">
                </ul>
                    <ul class="nav navbar-nav navbar-right removeRightSpace">
                        <li style="padding-left:7px">
                            <a href="#">
                                
                                <span class=""><img class="" src="Images/admin.png" width="15" height="15" style="position:relative; left:-7px;top:-2px;"/><span class="nav-icon-padding" runat="server" id="Nav_User" ></span></span>
                                


                            </a>

                        </li>
                 
                        <li class="logoutButtonLi">
                            <button runat="server" class="none logoutButton" id="Nav_signout" onserverclick ="signOut">
                                <span class="glyphicon glyphicon-log-out"></span>&nbsp;&nbsp;Log Out 

                            </button>

                        </li>
                    </ul>
                </div>
            </div>
        </nav>



     <asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate>
    <div class=" container-fluid bodyDiv">
 
        <div class="row">
          

                                                              
            <div class="col-sm-1">


            </div>

            <div class="col-sm-10">

                <div class="row bottom-spacing">

                    <div class="col-sm-4 dashboard-items">
                        
                        <button ID="Button_Ente" runat="server" Text="Enterprise Users"   class="bigButtons item-one  busi-button" onserverclick="Button_Ent_Click" >
                           <div class="col-sm-6">
                                  <img src="Images/business-contact-256 (1).png" width="70" height ="70" />
                           </div>
                            <div class="col-sm-6 whiteColor">
                              <asp:Label ID="Label_biz" runat="server" Text="" CssClass="big-text"></asp:Label><br/>
                            Enterprises
                            </div>
                         
                     
                          
                               
                        </button>
                     </div>

                    <div class="col-sm-4 dashboard-items  item-two ">

                        <button ID="Button_Indu" runat="server" Text="Industries" class="bigButtons indus-button"   onserverclick="Button_Indus_Click" >
                             <div class="col-sm-6">
                                  <img src="Images/building-256 (1).png" width="70" height ="70" />
                           </div>
                            <div class="col-sm-6 whiteColor">
                              <asp:Label ID="Label_indus" runat="server" Text="" CssClass="big-text"></asp:Label><br/>
                            Industries
                            </div>
                        </button>
                     </div>
                    <div class="col-sm-4 dashboard-items ">

                        <button ID="Button_Per" runat="server" Text="Personal Users"    class="bigButtons item-three pers-button"   onserverclick="Button_Pers_Click">
                                                         <div class="col-sm-6">
                                  <img src="Images/conference-256 (1).png" width="70" height ="70" />
                           </div>
                            <div class="col-sm-6 whiteColor">
                              <asp:Label ID="Label_pers" runat="server" Text="" CssClass="big-text"></asp:Label><br/>
                            Personal Users
                            </div>
                        </button> 
                     </div>

                </div>

            </div>
            
            <div class="col-sm-1">


            </div>


        </div>

        <div class="row">
             <div class="col-sm-5 col-sm-offset-1 dashboard-items">

                       <div class="detailsPanel">
                
                            <asp:GridView ID="GridView_PieCount"   ShowHeaderWhenEmpty="true" AllowPaging="true" Font-Size="Small" AutoGenerateColumns="false"  AllowSorting="true"   OnPageIndexChanging="GridView_PieCount_PageIndexChanging" CssClass="chart-middle  table table-hover GV_table" runat="server" OnSorting="GridView_PieCount_Sorting" EmptyDataText="No records found" HorizontalAlign="Center" >
                                <HeaderStyle CssClass="GV_header" HorizontalAlign="Center" />
                                <PagerStyle CssClass="GV_pager" HorizontalAlign="Right" />


                            </asp:GridView>
                            </div>
             </div>

             <div class="col-sm-5 graphPanel chart-middle dashboard-items" >
                 <asp:Panel ID="Panel_Date" CssClass="monthyear-filter"
                     runat="server">
                <label>
                    Month:
                    <asp:DropDownList ID="DropDownList_Month" runat="server"  OnSelectedIndexChanged="DropDownList_Month_SelectedIndexChanged"  AutoPostBack="True">
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                </asp:DropDownList>
            
                </label>
                  <label>
                      /
                      <asp:DropDownList ID="DropDownList_Year" OnSelectedIndexChanged="DropDownList_Year_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList></label>
                     </asp:Panel>
                     <asp:Chart ID="Chart_Dynamic"   EnableViewState="true"  OnClick="Chart_Dynamic_Click" CssClass="chart-middle" Width="500px" Height="500px" runat="server" Palette="SemiTransparent">
                        <Series>
                                <asp:Series Name="SeriesDynamic"    ChartArea="ChartAreaDynamic"  LabelBorderWidth="1" IsValueShownAsLabel="True"  YAxisType="Primary" XAxisType="Primary" ></asp:Series> 
 
                         </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor ="Transparent" Name="ChartAreaDynamic"  BorderDashStyle="NotSet" BorderColor="White">
                                <AxisY>
                                    <MajorGrid Enabled="False" />

                                </AxisY>
                                <AxisX>
                                    <MajorGrid Enabled="False" />
                                </AxisX>
                                <AxisX2 Enabled="False">
                                </AxisX2>
                                <AxisY2>
                                    <MajorGrid Enabled="False" />
                                </AxisY2>
                            </asp:ChartArea>
                        </ChartAreas>
                          <Legends>
                            <asp:Legend Name="LegendDynamic"   Alignment="Center" Docking="Bottom"  TitleAlignment="Center">
                                <CellColumns>
                                    <asp:LegendCellColumn Name="DynamicLegend" ColumnType="SeriesSymbol">
                                        <Margins Left="15" Right="15" />
                                    </asp:LegendCellColumn>
                                    <asp:LegendCellColumn Name="ColumnPieText" ColumnType="Text" >
                                <Margins Left="15" Right="15"></Margins>
                                        </asp:LegendCellColumn>

                                </CellColumns>
                            </asp:Legend>
                        </Legends>
                      <Titles>
                            <asp:Title Font="Microsoft Sans Serif, 10pt, style=Bold" Name="Title1" Alignment="TopCenter" Text="">
                            </asp:Title>
                        </Titles>
                 </asp:Chart>
            
             </div>
            <div class="col-sm-1">


            </div>
          </div>
    

        <div class="row">


                <div class="col-sm-10 col-sm-offset-1">

                  
               

                  </div>
                  <div class="col-sm-1">

                  </div>


            </div>

      

        </div>
                 </ContentTemplate></asp:UpdatePanel>
</asp:Content>
