## BeeCloud .Net SDK (Open Source)
![pass](https://img.shields.io/badge/Build-pass-green.svg) ![license](https://img.shields.io/badge/license-MIT-brightgreen.svg) ![version](https://img.shields.io/badge/version-v1.3.0-blue.svg)


## 简介

本项目的官方GitHub地址是 [https://github.com/beecloud/beecloud-dotnet](https://github.com/beecloud/beecloud-dotnet)

本SDK是根据[BeeCloud Rest API](https://github.com/beecloud/beecloud-rest-api) 开发的 .NET SDK, 适用于 .NET framework 3.5及以上平台。可以作为调用BeeCloud Rest API的示例或者直接用于生产。

## 安装
1.从BeeCloud [release](https://github.com/beecloud/beecloud-dotnet/releases)中下载dll文件,然后导入自己工程。  

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

- 境内支付

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

- 境外支付

方法原型：

```.net
public static BCPayResult BCInternationalPay(string channel, int totalFee, string billNo, string title, string currency, BCCreditCardInfo info,  string creditCardId, string returnUrl)
```

调用：

```.net
//通过登录paypal账号付款
BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_PAYPAL.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", null, null, "http://localhost:50003/paypal/return_paypal_url.aspx");

//使用信用卡付款
BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", info, null, null);

//使用存储的信用卡信息付款
BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_SAVED_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", null, "CARD-1K997489XXXXXXXXXXXXXXX", null);
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
* 查询退款状态（支持微信，易宝，快钱）

方法原型：

```.net
public static BCRefundStatusQueryResult BCRefundStatusQuery(string channel, string refundNo);
```
调用：

```.net
BCRefundStatusQueryResult result = BCPay.BCRefundStatusQuery("WX", refundNo);
```
## 4.批量打款
* 支付宝批量打款

方法原型:

```.net
public static BCTransferResult BCTransfer(string channel, string batchNo, string accountName, List<BCTransferData> transferData)
```
调用：

```.net
BCTransferData data = new BCTransferData();
data.transferId = BCUtil.GetUUID();
data.receiverAccount = "xx@xx.com";
data.receiverName = "某某某";
data.transferFee = 100;
data.transferNote = "note";
BCTransferData data2 = new BCTransferData();
data2.transferId = BCUtil.GetUUID();
data2.receiverAccount = "xx@xx.com";
data2.receiverName = "某某";
data2.transferFee = 100;
data2.transferNote = "note";
List<BCTransferData> list = new List<BCTransferData>();
list.Add(data);
list.Add(data2);
BCTransferResult result = BCPay.BCTransfer(BCPay.TransferChannel.ALI.ToString(), BCUtil.GetUUID(), "毛毛", list);
```

## Demo
项目中的`BeeCloudSDKDemo`工程为我们的demo  
demo使用framework4.5  
在demo工程中添加BeeCloud工程的dll引用，设置demo工程为启动项后F5即可运行调试
>每次修改过BeeCloud工程后请先build BeeCloud工程再运行demo调试

- 关于微信的return_url
微信没有return_url，如果用户需要支付完成做类似同步跳转的形式，需要自行完成。

- 关于支付宝的return_url
请参考demo中的`return_ali_url.aspx`

- 关于银联的return_url
请参考demo中的`return_un_url.aspx`

- 关于weekhook的接收
请参考demo中的`notify.asxp`
文档请阅读 [webhook](https://github.com/beecloud/beecloud-webhook)


## 常见问题
- demo的支付宝为什么报错，报错内容为：调试错误，请回到请求来源地，重新发起请求。错误代码 ILLEGAL_PARTNER？  
由于政策原因，一些渠道的账号不能提供给用户测试，所以有遇到没有权限，渠道未开通等报错是正常的。

- `LitJson.dll`和`ThoughtWorks.QRCode.dll`哪里有下？  
可以在demo项目的bin文件夹下获得，推荐使用本项目提供的dll，有遇到过使用不同版本的dll导致出错的情况。  

- 网页在手机上如何使用微信支付？  
1.由于微信的限制，现在手机网页只能在微信APP内实现使用微信支付，即微信公众号支付（WX_JSAPI）  
2.WX_JSAPI支付配置相对复杂，请参考[微信文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_1)  

- 查询/退款方法没有传channel但是报错了？  
要不传channel完成查询/退款必须保证所有渠道所有订单号不同，否则会报错告诉开发者，要求传入channel以区分。

- 易宝不支持本地调试，需要在服务器上，并配置信任域才可调试。  

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
- 如果有什么问题，可以到BeeCloud开发者1群:**321545822** 或 BeeCloud开发者2群:**427128840** 提问
- 更详细的文档，见源代码的注释以及[官方文档](https://beecloud.cn/doc/?index=7)
- 如果发现了bug，欢迎提交[issue](https://github.com/beecloud/beecloud-dotnet-sdk/issues)
- 如果有新的需求，欢迎提交[issue](https://github.com/beecloud/beecloud-dotnet-sdk/issues)

## 代码许可
The MIT License (MIT).
