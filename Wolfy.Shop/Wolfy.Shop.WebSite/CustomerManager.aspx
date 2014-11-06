<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerManager.aspx.cs" Inherits="Wolfy.Shop.WebSite.CustomerManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <asp:Button runat="server" ID="btnAdd" Text="添加" OnClick="btnAdd_Click" /><asp:Button Text="并发更新" ID="btnSameTimeUpdate" runat="server" OnClick="btnSameTimeUpdate_Click" />
            <asp:Button Text="按ID查询" runat="server" OnClick="btnSearchByID_Click"  ID="btnSearchByID"/>
            <asp:Button Text="按ID查询客户订单和产品" runat="server" OnClick="btnOrderProduct_Click"  ID="btnOrderProduct"/>
            <asp:Button Text="添加客户和订单" ID="btnCsOrder" runat="server" OnClick="btnCsOrder_Click" />
            <asp:Button Text="LazyLoad查询" runat="server"  ID="btnLazyLoad" OnClick="btnLazyLoad_Click"/>
            <br />
            按姓名查询：
            <asp:TextBox runat="server" ID="txtName" />
            <asp:TextBox runat="server" ID="txtAddress" />
            <asp:Button Text="查询" runat="server" ID="btnSearch" OnClick="btnSearch_Click" />

            <div>
                <asp:Repeater runat="server" ID="rptCustomerList">
                    <HeaderTemplate>
                        <table class="table">
                            <tr>
                                <th>姓名</th>
                                <th>姓名</th>
                                <th>地址</th>
                                <th>版本</th>
                                <th>姓名/地址</th>
                                <th>操作</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td><%#Eval("NameAddress.CustomerName") %></td>
                            <td><%#Eval("NameAddress.CustomerAddress") %></td>
                            <td><%#Eval("Version") %></td>
                            <td><%#Eval("NameAddress.NameAddress") %></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkOrder" CommandArgument='<%#Eval("CustomerID") %>' CommandName="Order"  OnClick="lnkOrder_Click">下单</asp:LinkButton>
                                      <asp:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%#Eval("CustomerID") %>' CommandName="delete"  OnClick="lnkOrder_Click">删除</asp:LinkButton>
                                 <asp:LinkButton runat="server" ID="lnkFindOrderProduct" CommandArgument='<%#Eval("CustomerID") %>' CommandName="OrderProduct"  OnClick="lnkOrder_Click">我的订单</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
