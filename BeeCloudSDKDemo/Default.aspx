﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BeeCloudSDKDemo._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title></title>
    <style type="text/css">
        .clear:after {
            content: ".";
            display: block;
            clear: both;
            visibility: hidden;
            line-height: 0;
            height: 0;
        }

        html {
            width: 100%;
        }

        body {
            margin: 0;
            padding: 0;
            width: 100%;
            color: #111;
            font-family: "PingHei", STHeitiSC-Light, "Lucida Grande", "Lucida Sans Unicode", Helvetica, Arial, Verdana, sans-serif;
            font-size: 1em;
        }

        ul {
            list-style: none;
            padding: 0;
            margin: 0;
            width: 100%;
        }

            ul li {
                float: left;
                margin: 0 1em;
            }

                ul li img {
                    cursor: pointer;
                    width: 158px;
                    border: rgba(0, 0, 0, 0.2) 2px solid;
                }

                    ul li img:hover {
                        box-shadow: 0 0 2px #0CA6FC;
                        border: #0CA6FC 2px solid;
                    }

        .button {
            cursor: pointer;
            display: block;
            line-height: 45px;
            text-align: center;
            width: 158px;
            height: 45px;
            margin-top: 1.5em;
            border: rgba(123, 170, 247, 1) 1px solid;
            color: #fff;
            font-size: 1.2em;
            border-top-color: #1992da;
            border-left-color: #0c75bb;
            border-right-color: #0c75bb;
            border-bottom-color: #00589c;
            -webkit-box-shadow: inset 0 1px 1px 0 #6fc5f5;
            -moz-box-shadow: inset 0 1px 1px 0 #6fc5f5;
            box-shadow: inset 0 1px 1px 0 #6fc5f5;
            background: #117ed2;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#37aaea", endColorstr="#117ed2");
            background: -webkit-gradient(linear, left top, left bottom, from(#37aaea), to(#117ed2));
            background: -moz-linear-gradient(top, #37aaea, #117ed2);
            background-image: -o-linear-gradient(top, #37aaea 0, #117ed2 100%);
            background-image: linear-gradient(to bottom, #37aaea 0, #117ed2 100%);
        }

            .button:hover {
                background: #1c5bad;
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#2488d4", endColorstr="#1c5bad");
                background: -webkit-gradient(linear, left top, left bottom, from(#2488d4), to(#1c5bad));
                background: -moz-linear-gradient(top, #2488d4, #1c5bad);
                background-image: -o-linear-gradient(top, #2488d4 0, #1c5bad 100%);
                background-image: linear-gradient(to bottom, #2488d4 0, #1c5bad 100%);
                -webkit-box-shadow: inset 0 1px 1px 0 #64bef1;
                -moz-box-shadow: inset 0 1px 1px 0 #64bef1;
                box-shadow: inset 0 1px 1px 0 #64bef1;
            }

        li.clicked img {
            box-shadow: 0 0 2px #0CA6FC;
            border: #0CA6FC 2px solid;
        }

        input {
            display: none;
        }
    </style>
</head>
<body>
    <div>
        <h2>商品名：在线支付Demo</h2>
        <h4>应付总额： ¥0.01</h4>
        <p>请选择支付方式：</p>
    </div>
    <form action="/pay.aspx" method="post" target="_blank">
        <div>
            <ul class="clear" style="margin-top: 20px">
                <li class="clicked" onclick="paySwitch(this)">
                    <input type="radio" value="alipay" name="paytype" checked="checked"/>
                    <img src="http://beeclouddoc.qiniudn.com/ali.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="wechatQr" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/wechats.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="unionpay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/unionpay.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="unionwappay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-unwap.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="qralipay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/alis.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="aliwappay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/aliwap.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="jdpay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/jd.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="jdwappay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/jdwap.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="ybpay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/yb.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="ybwappay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/ybwap.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="beepay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon_gateway.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="beepayexpress" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon_BcExpress.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bc_native" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-bcwxsm.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bc_ali_qrcode" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-bcalism.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bc_wx_wap" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-bcwxwap.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bc_wx_scan" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-bcwxsk.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bc_ali_scan" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-bczfbsk.png" alt=""/>
                </li>
            </ul>
        </div>
        <div>
            <a href="/wxjsapi/wxjsapi.aspx">微信公众号在微信内支付</a>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="确认付款"/>
        </div>
    </form>
    <div>---------------------------------------------- 订阅：-----------------------------------------------------------</div>
        <div>
            <ul class="clear" style="margin-top: 20px">
                <li onclick="paySwitch(this)">
                    <a href="/subscription.aspx"><img src ="http://beeclouddoc.qiniudn.com/img-subscriptionpay.png" alt=""/></a>
                </li>
            </ul>
        </div>
    <div>------------------------------------------海外支付：-----------------------------------------------------------</div>
        <div>
            <a href="/PAYPAL/PAYPAL.aspx">PayPal支付</a>
        </div>
        <div>
            <a href="/PAYPAL/PAYPAL_CREDITCARD.aspx">PayPal信用卡支付</a>
        </div>
        <div>
            <a href="/PAYPAL/PAYPAL_SAVED_CREDITCARD.aspx">PayPal已存储的信用卡支付</a>
        </div>
    <div>------------------------------------------支付查询：-----------------------------------------------------------</div>
    <form action="/query.aspx" method="post" target="_blank">
        <div>
            <ul class="clear" style="margin-top: 20px">
                <li onclick="paySwitch(this)">
                    <input type="radio" value="aliquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/ali.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="wxquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/wechat.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="unionquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/unionpay.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="jdquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/jd.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="ybquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/yb.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="kqquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/kq.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="beepay" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon_gateway.png" alt=""/>
                </li>
            </ul>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="查询"/>
        </div>
    </form>
    <div>------------------------------------------退款查询：-----------------------------------------------------------</div>
    <form action="/refundQuery.aspx" method="post" target="_blank">
        <div>
            <ul class="clear" style="margin-top: 20px">
                <li onclick="paySwitch(this)">
                    <input type="radio" value="alirefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/ali.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="wxrefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/wechat.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="unionrefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/unionpay.png" alt=""/>
                </li>
                 <li onclick="paySwitch(this)">
                    <input type="radio" value="jdrefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/jd.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="ybrefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/yb.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="kqrefundquery" name="querytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/kq.png" alt=""/>
                </li>
            </ul>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="查询"/>
        </div>
    </form>
    <div>---------------------------------------------- 打款：-----------------------------------------------------------</div>
    <form action="/pay.aspx" method="post" target="_blank">
        <div>
            <ul class="clear" style="margin-top: 20px">
                <li onclick="paySwitch(this)">
                    <input type="radio" value="alitransfers" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/alitransfer.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="alitransfer" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/ali_transfer.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="wxtransfer" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/wx_transfer.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="wxredpack" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/wx_redpack.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="bccjtransfer" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/icon-companypay.png" alt=""/>
                </li>
            </ul>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="付款"/>
        </div>
    </form>
    <div>------------------------------------------------身份实名验证-------------------------------------------------------</div>
    <div>
        <ul class="clear" style="margin-top: 20px">
            <li onclick="paySwitch(this)">
                <a href="/auth.aspx"><img src ="http://beeclouddoc.qiniudn.com/icon-jianquan.png" alt=""/></a>
            </li>
        </ul>
    </div>
</body>
<script type="text/javascript">
    function paySwitch(that) {
        var li = document.getElementsByClassName("clicked");
        console.log(li);
        li[0].childNodes[1].removeAttribute("checked");
        li[0].className = "";
        that.className = "clicked";
        that.childNodes[1].setAttribute("checked", "checked");
    }
</script>
</html>
