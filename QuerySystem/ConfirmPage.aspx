<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="QuerySystem.ConfirmPage" %>

<%@ Register Src="~/ShareControls/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <uc1:ucJSScript runat="server" ID="ucJSScript" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <a href="List.aspx">回列表頁</a>
                <div class="col-lg-8">
                    <asp:HiddenField ID="hfID" runat="server" />
                    <h3>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h3>
                    <h5>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h5>
                    <table>
                        <tr>
                            <td>姓名</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>手機</td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" TextMode="Phone" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>
                                <asp:TextBox ID="txtMail" runat="server" TextMode="Email" Enabled="false"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>年齡</td>
                            <td>
                                <asp:TextBox ID="txtAge" runat="server" TextMode="Number" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>
                    <br />
                    <%--<asp:Button ID="btnCancel" class="btn btn-secondary" runat="server" Text="修改" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="確定送出" OnClick="btnSubmit_Click" />--%>
                    <input type="button" id="btnCancel" value="修改" class="btn btn-secondary" />
                    <input type="button" id="btnSubmit" value="送出" class="btn btn-primary" />

                </div>
            </div>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $("input[id*=Q][type=text]").attr('style', 'margin-bottom:25px;');

            $("input[id=btnSubmit]").click(function () {
                if (!confirm("確定送出?"))
                    return false;

                $.ajax({
                    url: "/API/AnswerHandler.ashx?Action=Submit",
                    method: "POST",
                    //data: postData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "success") {
                            window.location = "Stastic.aspx?ID=" + $("#hfID").val();
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            });

            $("input[id=btnCancel]").click(function () {
                if (confirm("想要修改作答?")) {
                    window.location = "Form.aspx?ID=" + $("#hfID").val();
                }
            });
        });
    </script>
</body>
</html>
