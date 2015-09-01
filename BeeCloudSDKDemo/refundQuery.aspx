<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="refundQuery.aspx.cs" Inherits="BeeCloudSDKDemo.refundQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="RefundQueryGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="QueryGridView_RowDataBound" OnRowCommand="RefundQueryGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="billNo" HeaderText="订单号" />
            <asp:BoundField DataField="refundNo" HeaderText="订单标题" />
            <asp:BoundField DataField="totalFee" HeaderText="总金额" />
            <asp:BoundField DataField="refundFee" HeaderText="退款金额金额" />
            <asp:BoundField DataField="channel" HeaderText="渠道" />
            <asp:BoundField DataField="finish" HeaderText="是否完成" />
            <asp:BoundField DataField="result" HeaderText="是否成功" />
            <asp:BoundField DataField="createdTime" HeaderText="创建时间" />
            <asp:ButtonField CommandName="refundStatus" Text="查看退款状态" />
        </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
