<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="QuerySystem.Stastic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <a href="List.aspx">回列表頁</a>
        <div>
            <h2><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
            <asp:Literal ID="ltlContent" runat="server"></asp:Literal><br />
            <asp:PlaceHolder ID="plcDynamic" runat="server">

            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
