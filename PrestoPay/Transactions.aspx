<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="PrestoPay.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/page-tweak.css" rel="stylesheet" />


    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/r/bs-3.3.5/dt-1.10.8/datatables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/r/bs-3.3.5/dt-1.10.8/datatables.min.js"></script>

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/nav-bootstrap.css" rel="stylesheet" />
    <link href="Content/popout.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>


    <style>
        .navbar {
            border-radius: 0px !important;
        }

        .pagination .active a {
            color: white;
            background-color: #7B52AB !important;
            border-color: #7B52AB !important;
        }

        .pagination li a {
            color: black;
        }

            .pagination li a:hover {
                color: black;
            }

        .dt-right {
            text-align: right;
        }

        .small-nav {
            margin-bottom: 10px;
        }

        .nav li.removepurple:hover {
            background-color: white !important;
        }

         .trans-div {
            display: block;
        }

        .trans-div2 {
            display: none;
        }

        @media (max-width:768px) {
           .trans-div {
                display: none !important;
            }

            .trans-div2 {
                display: block !important;
            }
        }
    </style>
    <link href="daterangepicker/jquery.dataTables.yadcf.css" rel="stylesheet" />
    <script src="daterangepicker/jquery.dataTables.yadcf.js"></script>
    <link href="daterangepicker/daterangepicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clientBody" runat="server">
    <script src="daterangepicker/moment.min.js"></script>
    <script src="daterangepicker/daterangepicker.js"></script>


    <div class="container-fluid bodyDiv">



        <div class="row">

            <div class="col-sm-2">
            </div>


            <div class="col-sm-8">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-horizontal">
                            <div class="form-group">


                                <div class="col-sm-9">
                                    <label>Filter By:</label>
                                    <asp:DropDownList ID="DropDownList_Filter" CssClass="form-control" AutoPostBack="true" runat="server">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Qr Pay</asp:ListItem>
                                        <asp:ListItem>Online Payment</asp:ListItem>
                                        <asp:ListItem>Transfer</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                    </div>
                </div>
                <div class="row">

                    <div class="col-sm-12">
                        <div class="trans-div row">


                            <table id="transactionDetails" class="table table-bordered dt-bootstrap" border="1">
                                <thead>
                                    <tr>
                                        <th>Transaction ID</th>
                                        <th>Email</th>
                                        <th>Date</th>
                                        <th>Description</th>
                                        <th>Receipt(SGD)</th>
                                        <th>Payment(SGD)</th>
                                    </tr>
                                </thead>

                                <tbody>
                                </tbody>


                            </table>
                        </div>



                        <div class="trans-div2 row">

                            <table id="transactionMin" class="table table-bordered dt-bootstrap" border="1">
                                <thead>
                                    <tr>
                                        <th>Transaction ID</th>

                                        <th>Date</th>
                                        <th>Amount (SGD)</th>
                                    </tr>
                                </thead>

                                <tbody>
                                </tbody>


                            </table>

                        </div>



                    </div>
                </div>
            </div>
            <script>
                $(document).ready(function () {
                    //var data = { filter: "abc" };
                    $("#transactionDetails").DataTable({

                        ajax: {

                            url: 'Transactions.aspx/FillTransactionTable',
                            dataSrc: function (data) {
                                return data.d.data;

                            },

                            type: 'POST',

                            contentType: 'application/json; charset=utf-8',

                        },
                        columnDefs: [
                            { className: "dt-right", "targets": [3, 4] },

                        ],
                        paging: true,
                        order: [[0, 'desc']],
                        columns: [
                            { data: "TransactionID" },
                            { data: "Email" },
                            {
                                "data": "Date",
                                "render": function (data) {

                                    var date = new Date(parseInt(JSON.parse(data).substr(6)));
                                    var day = date.getDate();
                                    if (day < 10)
                                        day = "0" + day;
                                    var mnth = date.getMonth() + 1;
                                    if (mnth < 10)
                                        mnth = "0" + mnth;
                                    var year = date.getFullYear();

                                    return "<span class='hidden'>" + year + mnth + day + "</span>" + day + "/" + mnth + "/" + year;
                                },



                            },
                            { data: "Description" },
                            {
                                'data': "Receipt",
                                'render': function (data) {
                                    if (parseFloat(data) <= 0)
                                        return " ";
                                    else
                                        return data.toFixed(2);
                                }
                            },
                            {
                                'data': "Payment", 'render': function (data) {
                                    if (parseFloat(data) <= 0)
                                        return " ";
                                    else
                                        return data.toFixed(2);

                                }
                            }

                        ],
                        autoWidth: false

                    });



                });

                $("#transactionMin").DataTable({

                    ajax: {

                        url: 'Transactions.aspx/FillTransactionMin',
                        dataSrc: function (data) {
                            return data.d.data;

                        },

                        type: 'POST',

                        contentType: 'application/json; charset=utf-8',

                    },
                    columnDefs: [
                        { className: "dt-right", "targets": [2] },

                    ],
                    paging: true,
                    order: [[0, 'desc']],
                    columns: [
                        { data: "TransactionId" },

                        {
                            "data": "Date",
                            "render": function (data) {

                                var date = new Date(parseInt(JSON.parse(data).substr(6)));
                                var day = date.getDate();
                                if (day < 10)
                                    day = "0" + day;
                                var mnth = date.getMonth() + 1;
                                if (mnth < 10)
                                    mnth = "0" + mnth;
                                var year = date.getFullYear();

                                return "<span class='hidden'>" + year + mnth + day + "</span>" + day + "/" + mnth + "/" + year;
                            },



                        },
                        { data: "amt" },



                    ],
                    autoWidth: false

                });


            </script>

            <div class="col-sm-2">
            </div>


        </div>
    </div>
</asp:Content>
