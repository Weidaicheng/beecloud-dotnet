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
            <asp:ButtonField CommandName="sub" Text="我要订阅" />
        </Columns>
        </asp:GridView>
    <div>
        <p></p>
    </div>
    </form>
</body>
</html>
