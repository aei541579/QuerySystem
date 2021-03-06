<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="QuerySystem.Form" %>

<%@ Register Src="~/ShareControls/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="JS/jquery.min.js"></script>
    <uc1:ucJSScript runat="server" ID="ucJSScript" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <%--<a href="List.aspx">回列表頁</a>--%>
                <div class="col-lg-8">
                    <asp:HiddenField ID="hfState" runat="server" />
                    <asp:HiddenField ID="hfID" runat="server" />
                    <h3>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h3>
                    <h5>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h5>
                    <table>
                        <tr>
                            <td>姓名</td>
                            <td>
                                <asp:TextBox ID="txtName" CssClass="Necessary" runat="server"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>手機</td>
                            <td>
                                <asp:TextBox ID="txtMobile" CssClass="Necessary" runat="server" TextMode="Phone"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>
                                <asp:TextBox ID="txtMail" CssClass="Necessary" runat="server" TextMode="Email"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>年齡</td>
                            <td>
                                <asp:TextBox ID="txtAge" CssClass="Necessary" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>
                    <br />
                    <%--<asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" class="btn btn-secondary"/>--%>
                    <input type="button" id="btnCancel" value="取消" class="btn btn-secondary" />
                    <input type="button" id="btnSubmit" value="送出" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </form>

    <script>
        $(document).ready(function () {
            if ($("#hfState").val() == "notyet") {
                alert("此問卷尚未開放");
                window.location = "List.aspx";
            }
            if ($("#hfState").val() == "expire") {
                alert("此問卷已截止作答");
                window.location = "List.aspx";
            }

            $("input[id*=Q][type=text]").attr('style', 'margin-bottom:25px;');

            $("input[id=btnSubmit]").click(function () {
                var inputCorrect = true;
                var Neclist = $(".Necessary").get();
                for (var necItem of Neclist) {
                    if (necItem.tagName == 'INPUT') {
                        if (necItem.value == "") {
                            inputCorrect = false;
                            alert("尚未作答完畢");
                            return;
                        }
                    }
                    if (necItem.tagName == 'TABLE') {
                        var parentTable = $(`#${necItem.id}`);
                        var SList = parentTable.find('input').get();
                        var SChecked = [];
                        for (var selection of SList) {
                            if (selection.checked) {
                                SChecked.push(selection);
                            }
                        }
                        if (SChecked.length == 0) {
                            inputCorrect = false;
                            alert("尚未作答完畢");
                            return;
                        }
                    }
                }
                //inputCorrect = true;


                if (inputCorrect) {
                    var answer = "";
                    var QList = $("input[id*=Q]").get();
                    for (var item of QList) {
                        if (item.type == "radio" && item.checked) {
                            answer += item.id + ";";
                        }
                        if (item.type == "checkbox" && item.checked) {
                            answer += item.id + ";";
                        }
                        if (item.type == "text" && item.value != "") {
                            answer += `${item.id}_${item.value}` + ";";
                        }
                    }
                    var postData = {
                        "Answer": answer,
                        "Profile": `${$("#txtName").val()};${$("#txtMobile").val()};${$("#txtMail").val()};${$("#txtAge").val()}`
                    };

                    $.ajax({
                        url: "/API/AnswerHandler.ashx?ID=" + $("#hfID").val() + "&Action=Confirm",
                        method: "POST",
                        data: postData,
                        success: function (txtMsg) {
                            console.log(txtMsg);
                            if (txtMsg == "success") {
                                window.location = "ConfirmPage.aspx?ID=" + $("#hfID").val();
                            }
                            if (txtMsg == "noAnswer") {
                                alert("您尚未作答");
                            }
                            else {
                                alert(txtMsg);
                            }
                        },
                        error: function (msg) {
                            console.log(msg);
                            alert("通訊失敗，請聯絡管理員。");
                        }
                    });

                }
            });

            $("input[id=btnCancel]").click(function () {
                if (confirm("確定要取消作答?")) {
                    window.location = "List.aspx";
                }
            });

        })
        function pushHistory() {
            var state = {
                title: "title",
                url: window.location.href
            }
            window.history.pushState(state, "title", window.location.href);
        }
        pushHistory();

        window.addEventListener("popstate", function () {
            alert("您將遺失您的作答資料")
                this.window.location = "List.aspx";
            
        }, true);

    </script>
</body>
</html>
