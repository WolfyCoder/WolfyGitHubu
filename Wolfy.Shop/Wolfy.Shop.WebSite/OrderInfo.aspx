<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="Wolfy.Shop.WebSite.OrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rptOrderProduct" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>订单ID</th>
                            <th>商品名称</th>
                            <th>商品单价</th>
                            <th>下单时间</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal Text="" ID="ltlOrderID" runat="server" /></td>
                        <td><%#Eval("Name") %>></td>
                        <td><%#Eval("Price") %></td>
                        <td>
                            <asp:Literal Text="" ID="ltlOrderDate" runat="server" /></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
