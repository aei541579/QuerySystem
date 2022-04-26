<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AnswerStastic.aspx.cs" Inherits="QuerySystem.SystemAdmin.AnswerStastic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        flotcontainer {
            width: 500px;
            height: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="plcDynamic" runat="server">

    </asp:PlaceHolder>
    <script>
        $(document).ready(function () {
            $("#Astastic").addClass("active");

            let url = new URLSearchParams(window.location.search);
            var divList = $("div[id*=flotcontainer]").get()
            console.log(divList);
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
        })

        function createChart(data, divNo) {
            var options = {
                series: {
                    pie: {
                        show: true,
                        radius: 1,
                        label: {
                            show: true,
                            radius: 2 / 3,
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
            var thisdiv = $(`div[class=${divNo.className}]`);
            thisdiv.attr("style", "width: 500px; height: 400px;");
            $.plot(thisdiv, data, options);
        }
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/excanvas.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/jquery.flot.min.js"></script>
    <script src="http://static.pureexample.com/js/flot/jquery.flot.pie.min.js"></script>
</asp:Content>
