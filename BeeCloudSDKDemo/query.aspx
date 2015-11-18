<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="query.aspx.cs" Inherits="BeeCloudSDKDemo.query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
        <asp:GridView ID="QueryGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="QueryGridView_RowDataBound" OnRowCommand="QueryGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="billNo" HeaderText="订单号" />
            <asp:BoundField DataField="title" HeaderText="订单标题" />
            <asp:BoundField DataField="totalFee" HeaderText="总金额" />
            <asp:BoundField DataField="channel" HeaderText="渠道" />
            <asp:BoundField DataField="result" HeaderText="订单是否成功" />
            <asp:BoundField DataField="createdTime" HeaderText="创建时间" />
            <asp:ButtonField CommandName="refund" Text="申请退款" />
            <asp:BoundField DataField="messageDetail" HeaderText="MessageDetail" NullDisplayText="NULL"/>
            <asp:BoundField DataField="optional" HeaderText="optional" NullDisplayText="NULL" />
            <asp:BoundField DataField="refundResult" HeaderText="已经退款?" />
            <asp:BoundField DataField="revertResult" HeaderText="已撤销？" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
