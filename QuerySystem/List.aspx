<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="QuerySystem.List" %>

<%@ Register Src="~/ShareControls/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>
<%@ Register Src="~/ShareControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>



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
                <div class="col-lg-8">
                    <table class="table table-borderless">
                        <tr>
                            <td width="10%">問卷標題</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>開始</td>
                            <td>
                                <asp:TextBox ID="txtStartTime" runat="server" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>結束</td>
                            <td>
                                <asp:TextBox ID="txtEndTime" runat="server" TextMode="Date"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped">
                        <tr>
                            <th>#</th>
                            <th>問卷</th>
                            <th>狀態</th>
                            <th>開始時間</th>
                            <th>結束時間</th>
                            <th>觀看統計</th>
                        </tr>
                        <asp:Repeater ID="rptTable" runat="server">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfState" runat="server" Value='<%#Eval("State") %>' />
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <a href="Form.aspx?ID=<%#Eval("QuestionnaireID") %>">
                                            <asp:Label ID="lblQueryName" runat="server" Text='<%#Eval("QueryName") %>'></asp:Label></a>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("State") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("StartTime") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <a href="Stastic.aspx?ID=<%#Eval("QuestionnaireID") %>">前往</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <uc1:ucPager runat="server" ID="ucPager" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
