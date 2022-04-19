<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExampleList.aspx.cs" Inherits="QuerySystem.SystemAdmin.ExampleList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                    <table>
                        <tr>
                            <td>問卷標題</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td>建立新範本</td>
                            <td>
                                <asp:TextBox ID="txtCreate" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
                            </td>
                        </tr>
                    </table>

                    <table class="table table-striped">
                        <tr>
                            <th></th>
                            <th>#</th>
                            <th>標題</th>
                            <th>建立時間</th>
                        </tr>
                        <asp:Repeater ID="rptTable" runat="server">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("QuestionnaireID") %>' />
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="ckbDel" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <a href="ExampleDesign.aspx?ID=<%#Eval("QuestionnaireID") %>">
                                            <asp:Label ID="lblQueryName" runat="server" Text='<%#Eval("QueryName") %>'></asp:Label></a>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>

    </form>
</body>
</html>
