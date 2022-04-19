<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="QuerySystem.Form" %>

<%@ Register Src="~/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>


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
                <a href="List.aspx">回列表頁</a>
                <div class="col-lg-8">
                    <asp:HiddenField ID="hfID" runat="server" />
                    <h2>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
                    <h4>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h4>
                    <table>
                        <tr>
                            <td>姓名</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>手機</td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" TextMode="Phone"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>
                                <asp:TextBox ID="txtMail" runat="server" TextMode="Email"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>年齡</td>
                            <td>
                                <asp:TextBox ID="txtAge" runat="server" TextMode="Number"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>

                    <br />
                    <br />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    <input type="button" id="btnSubmit" value="送出" />
                </div>
            </div>
        </div>
    </form>

    <script>
        $(document).ready(function () {
            $("input[id=btnSubmit]").click(function () {
                var answer = "";
                var QList = $("input[id*=Q]").get();
                console.log(QList);
                for (var item of QList) {
                    if (item.type == "radio" && item.checked) {
                        answer += item.id + " ";
                    }
                    if (item.type == "checkbox" && item.checked) {
                        answer += item.id + " ";
                    }
                    if (item.type == "text") {
                        answer += `${item.id}_${item.value}` + " ";
                    }
                }
                var postData = {
                    "Answer": answer,
                    "Name": $("#txtName").val(),
                    "Mobile": $("#txtMobile").val(),
                    "Email": $("#txtMail").val(),
                    "Age": $("#txtAge").val()
                };

                $.ajax({
                    url: "/API/AnswerHandler.ashx?ID=" + $("#hfID").val(),
                    method: "POST",
                    data: postData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "success") {
                            window.location = "ConfirmPage.aspx?ID=" + $("#hfID").val();
                        }
                        if (txtMsg == "noAnswer") {
                            alert("請作答");
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            });
        })

    </script>
</body>
</html>
