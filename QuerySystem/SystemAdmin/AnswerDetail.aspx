<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="QuerySystem.SystemAdmin.AnswerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
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
    </div>
    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>
    <br />
    <a href="#" runat="server" id="ansListLink">回作答列表</a>

    <script>
        $(document).ready(function () {
            $("#Alist").addClass("active");

            $("input[id*=Q][type=text]").attr('style', 'margin-bottom:25px;');
        })
    </script>
</asp:Content>
