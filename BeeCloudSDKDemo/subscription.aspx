<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="subscription.aspx.cs" Inherits="BeeCloudSDKDemo.subscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订阅支付</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>1. 请在BeeCloud控制台创建订阅计划</p>
        <p>2. 已创建的订阅计划</p>
    </div>
    <asp:GridView ID="PlansGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="PlansGridView_RowDataBound" OnRowCommand="PlansGridView_RowCommand">
        <Columns>
        <asp:BoundField DataField="id" HeaderText="计划ID" />
        <asp:BoundField DataField="fee" HeaderText="金额(分)" />
        <asp:BoundField DataField="interval" HeaderText="周期单位" />
        <asp:BoundField DataField="name" HeaderText="计划名称" />
        <asp:BoundField DataField="intervalCount" HeaderText="周期时长" />
        <asp:BoundField DataField="trialDays" HeaderText="多久后开始扣款" />
        <asp:BoundField DataField="valid" HeaderText="已启用" />
        <asp:ButtonField CommandName="sub" Text="我要订阅" />
    </Columns>
    </asp:GridView>
    <div>
        <p>3. 已订阅的记录</p>
    </div>
    <asp:GridView ID="SubscriptionGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="SubscriptionGridView_RowDataBound">
        <Columns>
        <asp:BoundField DataField="buyerID" HeaderText="用户ID" />
        <asp:BoundField DataField="planID" HeaderText="订阅计划ID" />
        <asp:BoundField DataField="cardID" HeaderText="银行卡ID" />
        <asp:BoundField DataField="last4" HeaderText="卡后4位" />
        <asp:BoundField DataField="bankName" HeaderText="银行名称" />
        <asp:BoundField DataField="idName" HeaderText="姓名" />
        <asp:BoundField DataField="idNo" HeaderText="身份证号" />
        <asp:BoundField DataField="mobile" HeaderText="手机号" />
        <asp:BoundField DataField="amount" HeaderText="数量" />
        <asp:BoundField DataField="status" HeaderText="状态" />
    </Columns>
    </asp:GridView>
    </form>
</body>
</html>
