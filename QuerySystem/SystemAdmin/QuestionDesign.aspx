<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="QuestionDesign.aspx.cs" Inherits="QuerySystem.SystemAdmin.QusetionDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="ltlAlert" runat="server" ForeColor="Red"></asp:Label>
    <table class="table table-borderless">
        <tr>
            <td width="15%">問卷名稱</td>
            <td width="75%">
                <asp:TextBox CssClass="form-control" ID="txtTitle" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>描述內容</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td>開始時間</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtStartTime" runat="server" TextMode="Date"></asp:TextBox></td>
        </tr>
        <tr>
            <td>結束時間</td>
            <td>
                <asp:TextBox CssClass="form-control" ID="txtEndTime" runat="server" TextMode="Date"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox ID="ckbActive" runat="server" Text="已啟用" Checked="true" />
                <%--<asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />--%>
                <div style="text-align: end;">
                    <input type="button" id="btnCancel" value="取消" class="btn btn-secondary" />
                    <asp:Button ID="btnSubmit" runat="server" Text="確認送出" OnClick="btnSubmit_Click" class="btn btn-primary" />
                </div>
            </td>
        </tr>
    </table>
    <script>
        $(document).ready(function () {
            $("#Qdesign").addClass("active");

            $("input[id=btnCancel]").click(function () {
                if (confirm("確定要取消編輯?")) {
                    window.location = "List.aspx";
                }
            });
        })
    </script>
</asp:Content>
