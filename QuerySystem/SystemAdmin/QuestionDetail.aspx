﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="QuerySystem.SystemAdmin.QuestionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-borderless">
        <tr>
            <td>種類</td>
            <td>
                <asp:DropDownList ID="ddlTemplate" runat="server">
                    <asp:ListItem>自訂問題</asp:ListItem>
                    <asp:ListItem>常用問題</asp:ListItem>
                </asp:DropDownList></td>
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

    <script>
        $(document).ready(function () {
            $("#Qdetail").addClass("active");
        })
    </script>

</asp:Content>
