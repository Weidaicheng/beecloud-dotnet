## BeeCloud .Net SDK (Open Source)
![pass](https://img.shields.io/badge/Build-pass-green.svg) ![license](https://img.shields.io/badge/license-MIT-brightgreen.svg) ![version](https://img.shields.io/badge/version-v1.0.0-blue.svg)

本SDK是根据[BeeCloud Rest API](https://github.com/beecloud/beecloud-rest-api) 开发的 .net SDK, 适用于 .net framework 3.5及以上平台。可以作为调用BeeCloud Rest API的示例或者直接用于生产。

## 安装
1.从BeeCloud [release](https://github.com/beecloud/beecloud-dotnet/releases)中下载dll文件,然后导入自己工程。（**请注意beta版本不支持从release下载**）

>下载的BeeCloud.xml文件为dll注释文件，请一起放入项目文件夹中，方便查看注释。  
>.net SDK使用了第三方Json库LitJson.dll，请一起引入项目。 

2.按需修改本工程之后编译获得dll文件(包括BeeCloud.dll, BeeCloud.xml,LitJson.dll)，导入自己工程即可。

## 注册
三个步骤，2分钟轻松搞定：
1. 注册开发者：猛击[这里](http://www.beecloud.cn/register)注册成为BeeCloud开发者。
2. 注册应用：使用注册的账号登陆[控制台](http://www.beecloud.cn/dashboard/)后，点击"+创建App"创建新应用
3. 在代码中注册：

```.net
BeeCloud.BeeCloud.registerApp(appid, appsecret);
```

## 使用方法
>具体使用请参考项目中的`BeeCloudSDKDemo`工程

### 1.支付

方法原型：

```.net
public static BCPayResult BCPayByChannel(string channel, int totalFee, string billNo, string title, Dictionary<string,string> optional, string returnUrl,string openId, string showURL, string qrPayMode);
```
调用：

```.net
BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.UN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自制自来水", null, "http://localhost:50003/return.aspx", null, null, "2");
if (result.resultCode == 0)
{
    BCUnWebPayResult payResult = result as BCUnWebPayResult;

}
```
### 2.退款

方法原型：

```.net
public static BCRefundResult BCRefundByChannel(string channel, string refundNo, string billNo, int refundFee, Dictionary<string, string> optional);
```
调用：

```.net
BCRefundResult refundResult = BCPay.BCRefundByChannel(BCPay.RefundChannel.ALI.ToString(), DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), billNo, totalFee, null);
if (refundResult.resultCode == 0)
{
    Response.Redirect(refundResult.url);
}
```
### 3.查询

* 查询支付订单

方法原型：

```.net
public static BCPayQueryResult BCPayQueryByCondition(string channel, string billNo, long? startTime, long? endTime, int? skip, int? limit);
```
调用：

```.net
BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp("ALI", null, null, null, null, 50);
```
* 查询退款订单

方法原型：

```.net
public static BCRefundQuerytResult BCRefundQueryByCondition(string channel, string billNo, string refundNo, long? startTime, long? endTime, int? skip, int? limit);
```
调用：

```.net
BCRefundQuerytResult result = BCPay.BCRefundQueryByCondition("ALI", null, null, null, null, null, 50);
```
* 查询退款状态（只支持微信）

方法原型：

```.net
public static BCRefundStatusQueryResult BCRefundStatusQuery(string channel, string refundNo);
```
调用：

```.net
BCRefundStatusQueryResult result = BCPay.BCRefundStatusQuery("WX", refundNo);
```

## Demo
项目中的`BeeCloudSDKDemo`工程为我们的demo  
在demo工程中添加BeeCloud工程的dll引用，设置demo工程为启动项后F5即可运行调试
>每次修改过BeeCloud工程后请先build BeeCloud工程再运行demo调试

- 关于支付宝的return_url

请参考demo中的`return_ali_url.aspx`
- 关于银联的return_url

请参考demo中的`return_un_url.aspx`
- 关于weekhook的接收

请参考demo中的`notify.asxp`
文档请阅读 [webhook](https://github.com/beecloud/beecloud-webhook)

## 测试
TODO

## 常见问题
- `LitJson.dll`和`ThoughtWorks.QRCode.dll`可以在项目的bin文件夹下获得，推荐使用本项目提供的dll，有遇到过使用不同版本的dll导致出错的情况。
- 网页在手机上如何使用微信支付？  
1.由于微信的限制，现在手机网页只能在微信APP内实现使用微信支付，即微信公众号支付（WX_JSAPI）  
2.WX_JSAPI支付配置相对复杂，请参考[微信文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_1)  
3.示例代码，调用js方法`callpay()`即可使用

```js
function onBridgeReady(){
   WeixinJSBridge.invoke(
       'getBrandWCPayRequest', {
           //以下参数的值由BCPayByChannel方法返回来的数据填入即可
           "appId" : "wx2421b1c4370ec43b",          
           "timeStamp":" 1395712654",              
           "nonceStr" : "e61463f8efa94090b1f366cccfbbb444",
           "package" : "prepay_id=u802345jgfjsdfgsdg888",  
           "signType" : "MD5",     
           "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" 
       },
       function(res){     
           if(res.err_msg == "get_brand_wcpay_request:ok" ) {}     // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
       }
   ); 
}
function callpay()
{
    if (typeof WeixinJSBridge == "undefined"){
        if( document.addEventListener ){
            document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
        }else if (document.attachEvent){
            document.attachEvent('WeixinJSBridgeReady', jsApiCall); 
            document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
        }
    }else{
        jsApiCall();
    }
}
```

## 代码贡献
我们非常欢迎大家来贡献代码，我们会向贡献者致以最诚挚的敬意。

一般可以通过在Github上提交[Pull Request](https://github.com/beecloud/beecloud-dotnet-sdk)来贡献代码。

Pull Request要求

- 代码规范 

- 代码格式化 

- 必须添加测试！ - 如果没有测试（单元测试、集成测试都可以），那么提交的补丁是不会通过的。

- 记得更新文档 - 保证`README.md`以及其他相关文档及时更新，和代码的变更保持一致性。

- 创建feature分支 - 最好不要从你的master分支提交 pull request。

- 一个feature提交一个pull请求 - 如果你的代码变更了多个操作，那就提交多个pull请求吧。

- 清晰的commit历史 - 保证你的pull请求的每次commit操作都是有意义的。如果你开发中需要执行多次的即时commit操作，那么请把它们放到一起再提交pull请求。

## 联系我们
- 如果有什么问题，可以到 **321545822** BeeCloud开发者大联盟QQ群提问
- 更详细的文档，见源代码的注释以及[官方文档](https://beecloud.cn/doc/?index=7)
- 如果发现了bug，欢迎提交[issue](https://github.com/beecloud/beecloud-dotnet-sdk/issues)
- 如果有新的需求，欢迎提交[issue](https://github.com/beecloud/beecloud-dotnet-sdk/issues)

## 代码许可
The MIT License (MIT).
