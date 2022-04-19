<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="QuerySystem.Stastic" %>

<%@ Register Src="~/ucJSScript.ascx" TagPrefix="uc1" TagName="ucJSScript" %>


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
                <a href="List.aspx">回列表頁</a>
                <div class="col-lg-8">
                    <h2>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
                    <h4>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h4>
                    <asp:PlaceHolder ID="plcDynamic" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
