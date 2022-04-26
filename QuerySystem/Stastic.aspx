<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="QuerySystem.Stastic" %>

<%@ Register Src="~/ShareControls/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <uc1:ucJSScript runat="server" ID="ucJSScript" />

    <style>
        #flotcontainer {
            width: 500px;
            height: 400px;
        }

        /*div {
            font-size: 20px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-lg-8">
                    <h3>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h3>
                    <h5>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h5>
                    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>
                    <%--<div id="legendPlaceholder"></div>
                    <div id="flotcontainer"></div>--%>
                </div>
                <a href="List.aspx">回列表頁</a>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            let url = new URLSearchParams(window.location.search);
            var divList = $("div[id=flotcontainer]").get()
            for (var divNo of divList) {
                $.ajax({
                    url: `/API/StasticHandler.ashx?ID=${url.get('ID')}&que=${divNo.className}`,
                    method: "GET",
                    dataType: "JSON",
                    async: false,
                    success: function (objData) {
                        createChart(objData, divNo);
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員");
                    }
                });
            }
        });

        function createChart(data, thisdiv) {
            var options = {
                series: {
                    pie: {
                        show: true,
                        radius: 1,
                        label: {
                            show: true,
                            radius: 2/3,
                            formatter: function (label, series) {
                                return '<div style="font-size:12pt;text-align:center;padding:2px;color:black;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
                            },
                            threshold: 0.1
                        }
                    }
                },
                legend: {
                    show: false
                }
            };
            var parentDiv = $(`div[class=${thisdiv.className}]`);
            $.plot(parentDiv, data, options);
            var labelList = parentDiv.find("div").get();
            console.log(labelList);

        }
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/excanvas.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/jquery.flot.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/jquery.flot.pie.min.js"></script>
</body>
</html>
