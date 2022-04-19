<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="AnswerStastic.aspx.cs" Inherits="QuerySystem.SystemAdmin.AnswerStastic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="plcDynamic" runat="server">

    </asp:PlaceHolder>
    <script>
        $(document).ready(function () {
            $("#Astastic").addClass("active");
        })
    </script>

</asp:Content>
