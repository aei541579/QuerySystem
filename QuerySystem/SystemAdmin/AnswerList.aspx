<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AnswerList.aspx.cs" Inherits="QuerySystem.SystemAdmin.AnswerList" %>

<%@ Register Src="~/ShareControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnExport" runat="server" Text="匯出(.csv)" OnClick="btnExport_Click" class="btn btn-outline-secondary" />
    <table class="table table-striped">
        <tr>
            <th>#</th>
            <th>姓名</th>
            <th>填寫時間</th>
            <th>觀看細節</th>
        </tr>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Literal ID="ltlName" runat="server" Text='<%#Eval("Name") %>'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="ltlTime" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Literal>
                    </td>
                    <td>
                        <a href="AnswerDetail.aspx?PersonID=<%#Eval("PersonID") %>">前往</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    <uc1:ucPager runat="server" ID="ucPager" />
    <script>
        $(document).ready(function () {
            $("#Alist").addClass("active");

            $("input[id*=btnExport]").attr('style','margin:10px;');
        })
    </script>

</asp:Content>
