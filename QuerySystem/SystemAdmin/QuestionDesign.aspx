<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="QuestionDesign.aspx.cs" Inherits="QuerySystem.SystemAdmin.QusetionDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-borderless">
        <tr>
            <td width="15%">問卷名稱</td>
            <td width="75%">
                <asp:TextBox CssClass="form-control"  ID="txtTitle" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>描述內容</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td>開始時間</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtStartTime" runat="server" TextMode="DateTimeLocal"></asp:TextBox></td>
        </tr>
        <tr>
            <td>結束時間</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtEndTime" runat="server" TextMode="DateTimeLocal"></asp:TextBox></td>
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
    <script>
        $(document).ready(function () {
            $("#Qdesign").addClass("active");
        })
    </script>
</asp:Content>
