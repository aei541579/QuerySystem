<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="QuestionDesign.aspx.cs" Inherits="QuerySystem.SystemAdmin.QusetionDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>問卷名稱</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>描述內容</td>
            <td>
                <asp:TextBox ID="txtContent" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>開始時間</td>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" TextMode="DateTime"></asp:TextBox></td>
        </tr>
        <tr>
            <td>結束時間</td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" TextMode="DateTime"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" Text="已啟用" Checked="true" />
                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
