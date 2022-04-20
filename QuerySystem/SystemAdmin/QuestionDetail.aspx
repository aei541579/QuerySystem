<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="QuerySystem.SystemAdmin.QuestionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="ltlAlert" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <table class="table table-borderless">
        <tr>
            <td>種類</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlTemplate" runat="server" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem runat="server" Value="0">自訂問題</asp:ListItem>

                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>問題</td>
            <td width="50%">
                <asp:HiddenField ID="hfEditQID" runat="server" />
                <asp:TextBox ID="txtQuestion" runat="server" class="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlQuestionType" runat="server">
                    <asp:ListItem Value="0">單選方塊</asp:ListItem>
                    <asp:ListItem Value="1">複選方塊</asp:ListItem>
                    <asp:ListItem Value="2" OnClientClick="return txtDisable()">文字</asp:ListItem>
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

    <script>
        $(document).ready(function () {
            $("#Qdetail").addClass("active");

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

</asp:Content>
