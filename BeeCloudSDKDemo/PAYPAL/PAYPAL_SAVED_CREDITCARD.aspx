<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAYPAL_SAVED_CREDITCARD.aspx.cs" Inherits="BeeCloudSDKDemo.PAYPAL.PAYPAL_SAVED_CREDITCARD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="payWithSavedCreditcard.aspx" method="post" target="_blank">
    <div>
        已存储用户信息，可以直接付款
    </div>
    <div style="clear: both;">
        <input type="submit" class="button" value="付款"/>
    </div>
    </form>
</body>
</html>
