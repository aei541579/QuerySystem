<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExampleDesign.aspx.cs" Inherits="QuerySystem.SystemAdmin.ExampleDesign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="/JS/jquery.min.js"></script>
    <script src="/JS/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <h2>後台-常用問題管理</h2>
            </div>
            <div class="row">
                <div class="col-lg-2">
                    <a href="List.aspx">問卷管理</a><br />
                    <a href="ExampleList.aspx">常用問題管理</a><br />
                </div>
                <div class="col-lg-8">
                    <table class="table table-borderless">
                        <tr>
                            <td>範例標題</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>問題</td>
                            <td>
                                <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="txtSelection" runat="server"></asp:TextBox>(多個答案以;分隔)
                <asp:Button ID="btnAddQuestion" runat="server" Text="加入" OnClick="btnAddQuestion_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped">
                        <tr>
                            <th></th>
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
                    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>

    </form>
</body>
</html>
