<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExampleDesign.aspx.cs" Inherits="QuerySystem.SystemAdmin.ExampleDesign" %>

<%@ Register Src="~/SystemAdmin/ucLeftColumn.ascx" TagPrefix="uc1" TagName="ucLeftColumn" %>
<%@ Register Src="~/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>



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
                <h2>後台-常用問題管理</h2>
            </div>
            <div class="row">
                <div class="col-lg-2">
                    <uc1:ucLeftColumn runat="server" ID="ucLeftColumn" />
                </div>
                <div class="col-lg-8">
                    <asp:Label ID="ltlAlert" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                    <table class="table table-borderless">
                        <tr>
                            <td>標題</td>
                            <td width="50%">
                                <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>問題</td>
                            <td>
                                <asp:HiddenField ID="hfEditQID" runat="server" />
                                <asp:TextBox ID="txtQuestion" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlQuestionType" runat="server">
                                    <asp:ListItem Value="0">單選方塊</asp:ListItem>
                                    <asp:ListItem Value="1">複選方塊</asp:ListItem>
                                    <asp:ListItem Value="2">文字</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="ckbNecessary" runat="server" Text="必填" />
                            </td>
                        </tr>
                        <tr>
                            <td>回答</td>
                            <td>
                                <asp:TextBox ID="txtSelection" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td>(多個答案以;分隔)
                <asp:Button ID="btnAddQuestion" runat="server" Text="加入" OnClick="btnAddQuestion_Click" />
                                <asp:Button ID="btnEditQuestion" runat="server" Text="修改" Visible="false" OnClick="btnEditQuestion_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped">
                        <tr>
                            <th width="10%">
                                <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
                            </th>
                            <th>#</th>
                            <th>問題</th>
                            <th>種類</th>
                            <th>必填</th>
                            <th></th>
                        </tr>
                        <asp:Repeater ID="rptQuestion" runat="server" OnItemCommand="rptQuestion_ItemCommand">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="ckbDel" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("QuestionVal") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckbListNec" runat="server" Checked='<%#Eval("Necessary") %>' Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lkbEdit" runat="server" CommandArgument='<%#Eval("QuestionID") %>' CommandName="lkbEdit">編輯</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>

    </form>
    <script>
        $(document).ready(function () {
            var txt = $("input[id*=txtSelection]");
            var parentDdl = $("select[id*=ddlQuestionType]")
            var txtDdl = parentDdl.find("option[value=2]");
            txtDdl.click(function () {
                txt.val('');
                txt.attr("disabled", "disable");
            })
            var rdbDdl = parentDdl.find("option[value=0]");
            rdbDdl.click(function () {
                txt.removeAttr("disabled", "disable");
            })
            var ckbDdl = parentDdl.find("option[value=1]");
            ckbDdl.click(function () {
                txt.removeAttr("disabled", "disable");
            })
        })
    </script>
</body>
</html>
