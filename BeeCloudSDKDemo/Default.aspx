<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BeeCloudSDKDemo._Default" %>
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
                    <input type="radio" value="qralipay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/alis.png" alt=""/>
                </li>
                <li onclick="paySwitch(this)">
                    <input type="radio" value="aliwappay" name="paytype"/>
                    <img src="http://beeclouddoc.qiniudn.com/aliwap.png" alt=""/>
                </li>
            </ul>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="确认付款"/>
        </div>
    </form>
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
            </ul>
        </div>
        <div style="clear: both;">
            <input type="submit" class="button" value="查询"/>
        </div>
    </form>
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
