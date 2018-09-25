<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="MerchantWebsite.PageNotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="Content/page-tweak.css" rel="stylesheet" />
    <title></title>

    <style>
            body{

            background-color:#337ab7;
            color:white;
            
        }

            .center-item{
                margin:auto;
                
                width:100%;
                height:100px;
                position:fixed;
                 left:0;
                 right:0;
                 text-align:center;              
                  top:20%;

            }

    </style>
</head>
<body>
    <form id="form" runat="server">
        
    </form>
    <div class="center-item">
            <span style="font-size:50px">
            <img alt="" src="Images/alert.png" /><br />
            Page Not Found !</span>
            <br />
            We couldn&#39;t find the page you were looking for<br />
            404 Error
            <br />
            <br />
            <br />
        </div>
    
</body>
</html>
