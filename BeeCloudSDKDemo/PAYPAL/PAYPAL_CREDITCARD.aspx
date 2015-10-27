<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAYPAL_CREDITCARD.aspx.cs" Inherits="BeeCloudSDKDemo.PAYPAL.PAYPAL_CREDITCARD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="payWithCreditCard.aspx" method="post" target="_blank">
    <div>
        <label>卡号&nbsp;&nbsp;&nbsp;&nbsp;</label><input type="number" name="cardNo" /><br />
        <label>有效期至&nbsp;&nbsp;</label><input type="month" name="expire" /><br />
        <label>CVV&nbsp;&nbsp;&nbsp;&nbsp;</label><input type="number" name="cvv" /><br />
        <label>持卡人名&nbsp;&nbsp;</label><input type="text" name="firstName" /><br />
        <label>持卡人姓&nbsp;&nbsp;</label><input type="text" name="lastName" /><br />
        <label>卡类别&nbsp;&nbsp;&nbsp;</label><input type="text" name="cardType" /><label>visa, mastercard, discover, amex</label><br />
    </div>
    <div style="clear: both;">
        <input type="submit" class="button" value="付款"/>
    </div>
    </form>
</body>
</html>
