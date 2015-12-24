using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeCloud;
using NUnit.Framework;
using BeeCloud.Model;

namespace BeeCloud.Tests
{
    [TestFixture()]
    public class BCPayTests
    {
        [TearDown()]
        public void reset()
        {
            BeeCloud.registerApp(null, null, null);
        }

        #region 支付
        [Test()]
        public void preparePayParametersTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");
            BCBill bill = new BCBill("ALI", 100, "10000000", "UT");
            bill.optional = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            bill.returnUrl = "http://www.test.com";
            bill.billTimeout = 360;

            string paraString = "\"channel\":\"ALI\",\"total_fee\":100,\"bill_no\":\"10000000\",\"title\":\"UT\",\"return_url\":\"http://www.test.com\",\"bill_timeout\":360,\"openid\":null,\"show_url\":null,\"qr_pay_mode\":null,\"optional\":{\"key1\":\"value1\",\"key2\":\"value2\"}}";
            string actual = BCPay.preparePayParameters(bill);
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handleAliWebPayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.ALI_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"html\":\"<form id=\\\"alipaysubmit\\\" name=\\\"alipaysubmit\\\" action=\\\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\\\" method=\\\"get\\\"><input type=\\\"hidden\\\" name=\\\"seller_email\\\" value=\\\"admin@beecloud.cn\\\"/><input type=\\\"hidden\\\" name=\\\"subject\\\" value=\\\"dotNet自来水\\\"/><input type=\\\"hidden\\\" name=\\\"_input_charset\\\" value=\\\"utf-8\\\"/><input type=\\\"hidden\\\" name=\\\"sign\\\" value=\\\"6b69e7e8b890b0f2e5ba282028d583a3\\\"/><input type=\\\"hidden\\\" name=\\\"notify_url\\\" value=\\\"http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_06e5647b-f5f2-4697-aba9-e2bb05608aa8.php\\\"/><input type=\\\"hidden\\\" name=\\\"payment_type\\\" value=\\\"1\\\"/><input type=\\\"hidden\\\" name=\\\"out_trade_no\\\" value=\\\"64a054f5ae0841188f077172b54dedf9\\\"/><input type=\\\"hidden\\\" name=\\\"partner\\\" value=\\\"2088711322600312\\\"/><input type=\\\"hidden\\\" name=\\\"service\\\" value=\\\"create_direct_pay_by_user\\\"/><input type=\\\"hidden\\\" name=\\\"total_fee\\\" value=\\\"0.01\\\"/><input type=\\\"hidden\\\" name=\\\"return_url\\\" value=\\\"http://localhost:50003/return_ali_url.aspx\\\"/><input type=\\\"hidden\\\" name=\\\"exter_invoke_ip\\\" value=\\\"112.86.54.108\\\"/><input type=\\\"hidden\\\" name=\\\"sign_type\\\" value=\\\"MD5\\\"/><input type=\\\"submit\\\" value=\\\"确认\\\" style=\\\"display:none;\\\"></form><script>document.forms['alipaysubmit'].submit();</script>\",\"id\":\"06e5647b-f5f2-4697-aba9-e2bb05608aa8\",\"url\":\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8&seller_email=admin@beecloud.cn&subject=dotNet自来水&_input_charset=utf-8&sign=6b69e7e8b890b0f2e5ba282028d583a3&notify_url=http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_06e5647b-f5f2-4697-aba9-e2bb05608aa8.php&payment_type=1&out_trade_no=64a054f5ae0841188f077172b54dedf9&partner=2088711322600312&service=create_direct_pay_by_user&total_fee=0.01&return_url=http://localhost:50003/return_ali_url.aspx&exter_invoke_ip=112.86.54.108&sign_type=MD5\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.url);
            Assert.IsNotNull(actual.html);
        }

        [Test()]
        public void handleWXQrcodePayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.WX_NATIVE.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            string respString = "{\"result_msg\":\"OK\",\"code_url\":\"weixin://wxpay/bizpayurl?pr=TtAYx12\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"id\":\"99f2c898-56e7-451a-8984-f077e0688033\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.codeURL);
        }

        [Test()]
        public void handleUNWebPayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.UN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            bill.returnUrl = "http://localhost:50003/return_un_url.aspx";
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"html\":\"<html><head><meta http-equiv=\\\"Content-Type\\\" content=\\\"text/html; charset=UTF-8\\\"/></head><body><form id = \\\"pay_form\\\" action=\\\"https://gateway.95516.com/gateway/api/frontTransReq.do\\\" method=\\\"post\\\"><input type=\\\"hidden\\\" name=\\\"bizType\\\" id=\\\"bizType\\\" value=\\\"000201\\\"/><input type=\\\"hidden\\\" name=\\\"orderId\\\" id=\\\"orderId\\\" value=\\\"4c8ae26558b0415491fbf97e6023671c\\\"/><input type=\\\"hidden\\\" name=\\\"backUrl\\\" id=\\\"backUrl\\\" value=\\\"http://123.57.71.81:8080/1/pay/callback/UNWeb/c37d661d-7e61-49ea-96a5-68c34e83db3b/d020f6b8-b35f-41a3-b4a8-780846c03725\\\"/><input type=\\\"hidden\\\" name=\\\"txnSubType\\\" id=\\\"txnSubType\\\" value=\\\"01\\\"/><input type=\\\"hidden\\\" name=\\\"signature\\\" id=\\\"signature\\\" value=\\\"Qtc3jybIFV+SYRN2cXPvMx2qZN5hMP+5xr+6IafLID7PNUQamCRrpQ3quygRrm8kiRmpF5/ddwdiNmoLuCb91zkCs24pYZfAUB2TDPnWlgI8QQTyUfa0W8USybwSj1WTfvLpeOL4A2Eawgl35xPaxB4MslQGl2dilOc/MnT8RCn7+bo84XGeCcj+n4OXJvvZK+M7k5sBn1slEm+WqAuy95C8esrjnIRSjq0vc62z/zzhjrPXXE0MXVrUHBQTiKRUn9RwnAq24PzFVaalNQgFCJetXw2MZPQCIWedzQ6TwlyPE34aCOBf0ivtHGxhRhC7b2AG00xoFyHzkB5ArHlMVw==\\\"/><input type=\\\"hidden\\\" name=\\\"frontUrl\\\" id=\\\"frontUrl\\\" value=\\\"http://localhost:50003/return_un_url.aspx\\\"/><input type=\\\"hidden\\\" name=\\\"txnType\\\" id=\\\"txnType\\\" value=\\\"01\\\"/><input type=\\\"hidden\\\" name=\\\"channelType\\\" id=\\\"channelType\\\" value=\\\"08\\\"/><input type=\\\"hidden\\\" name=\\\"certId\\\" id=\\\"certId\\\" value=\\\"69363599447\\\"/><input type=\\\"hidden\\\" name=\\\"encoding\\\" id=\\\"encoding\\\" value=\\\"UTF-8\\\"/><input type=\\\"hidden\\\" name=\\\"version\\\" id=\\\"version\\\" value=\\\"5.0.0\\\"/><input type=\\\"hidden\\\" name=\\\"accessType\\\" id=\\\"accessType\\\" value=\\\"0\\\"/><input type=\\\"hidden\\\" name=\\\"txnTime\\\" id=\\\"txnTime\\\" value=\\\"20151222155314\\\"/><input type=\\\"hidden\\\" name=\\\"merId\\\" id=\\\"merId\\\" value=\\\"898320548160217\\\"/><input type=\\\"hidden\\\" name=\\\"currencyCode\\\" id=\\\"currencyCode\\\" value=\\\"156\\\"/><input type=\\\"hidden\\\" name=\\\"orderDesc\\\" id=\\\"orderDesc\\\" value=\\\"dotNet自来水\\\"/><input type=\\\"hidden\\\" name=\\\"txnAmt\\\" id=\\\"txnAmt\\\" value=\\\"1\\\"/><input type=\\\"hidden\\\" name=\\\"signMethod\\\" id=\\\"signMethod\\\" value=\\\"01\\\"/></form></body><script type=\\\"text/javascript\\\">document.all.pay_form.submit();</script></html>\",\"id\":\"d020f6b8-b35f-41a3-b4a8-780846c03725\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.html);
        }

        [Test()]
        public void handleAliQrcodePayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.ALI_QRCODE.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            bill.qrPayMode = "0";
            bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"html\":\"<form id=\\\"alipaysubmit\\\" name=\\\"alipaysubmit\\\" action=\\\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\\\" method=\\\"get\\\"><input type=\\\"hidden\\\" name=\\\"seller_email\\\" value=\\\"admin@beecloud.cn\\\"/><input type=\\\"hidden\\\" name=\\\"subject\\\" value=\\\"dotNet自来水\\\"/><input type=\\\"hidden\\\" name=\\\"_input_charset\\\" value=\\\"utf-8\\\"/><input type=\\\"hidden\\\" name=\\\"sign\\\" value=\\\"8216a45e51f576cfd6a38f8c8576663a\\\"/><input type=\\\"hidden\\\" name=\\\"notify_url\\\" value=\\\"http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_c4dee3ef-69a1-4cfc-8faa-32c0b0b7c4fc.php\\\"/><input type=\\\"hidden\\\" name=\\\"qr_pay_mode\\\" value=\\\"0\\\"/><input type=\\\"hidden\\\" name=\\\"payment_type\\\" value=\\\"1\\\"/><input type=\\\"hidden\\\" name=\\\"out_trade_no\\\" value=\\\"0548650a2b8e4a5bb2ee4bba56c6fb0c\\\"/><input type=\\\"hidden\\\" name=\\\"partner\\\" value=\\\"2088711322600312\\\"/><input type=\\\"hidden\\\" name=\\\"service\\\" value=\\\"create_direct_pay_by_user\\\"/><input type=\\\"hidden\\\" name=\\\"total_fee\\\" value=\\\"0.01\\\"/><input type=\\\"hidden\\\" name=\\\"return_url\\\" value=\\\"http://localhost:50003/return_ali_url.aspx\\\"/><input type=\\\"hidden\\\" name=\\\"exter_invoke_ip\\\" value=\\\"112.86.54.108\\\"/><input type=\\\"hidden\\\" name=\\\"sign_type\\\" value=\\\"MD5\\\"/><input type=\\\"submit\\\" value=\\\"确认\\\" style=\\\"display:none;\\\"></form><script>document.forms['alipaysubmit'].submit();</script>\",\"id\":\"c4dee3ef-69a1-4cfc-8faa-32c0b0b7c4fc\",\"url\":\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8&seller_email=admin@beecloud.cn&subject=dotNet自来水&_input_charset=utf-8&sign=8216a45e51f576cfd6a38f8c8576663a&notify_url=http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_c4dee3ef-69a1-4cfc-8faa-32c0b0b7c4fc.php&qr_pay_mode=0&payment_type=1&out_trade_no=0548650a2b8e4a5bb2ee4bba56c6fb0c&partner=2088711322600312&service=create_direct_pay_by_user&total_fee=0.01&return_url=http://localhost:50003/return_ali_url.aspx&exter_invoke_ip=112.86.54.108&sign_type=MD5\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.url);
            Assert.IsNotNull(actual.html);
        }

        [Test()]
        public void handleAliWapPayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.ALI_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"html\":\"<form id=\\\"alipaysubmit\\\" name=\\\"alipaysubmit\\\" action=\\\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\\\" method=\\\"get\\\"><input type=\\\"hidden\\\" name=\\\"payment_type\\\" value=\\\"1\\\"/><input type=\\\"hidden\\\" name=\\\"out_trade_no\\\" value=\\\"370e678e72d04d139d1e623453f9aa1a\\\"/><input type=\\\"hidden\\\" name=\\\"partner\\\" value=\\\"2088711322600312\\\"/><input type=\\\"hidden\\\" name=\\\"subject\\\" value=\\\"dotNet自来水\\\"/><input type=\\\"hidden\\\" name=\\\"_input_charset\\\" value=\\\"utf-8\\\"/><input type=\\\"hidden\\\" name=\\\"service\\\" value=\\\"alipay.wap.create.direct.pay.by.user\\\"/><input type=\\\"hidden\\\" name=\\\"total_fee\\\" value=\\\"0.01\\\"/><input type=\\\"hidden\\\" name=\\\"sign\\\" value=\\\"045d7ede0af92f78fd334b726f171465\\\"/><input type=\\\"hidden\\\" name=\\\"return_url\\\" value=\\\"http://localhost:50003/return_ali_url.aspx\\\"/><input type=\\\"hidden\\\" name=\\\"notify_url\\\" value=\\\"http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_d136a0ca-2be9-4ba7-b5e4-d8293e53bea1.php\\\"/><input type=\\\"hidden\\\" name=\\\"sign_type\\\" value=\\\"MD5\\\"/><input type=\\\"hidden\\\" name=\\\"seller_id\\\" value=\\\"2088711322600312\\\"/><input type=\\\"submit\\\" value=\\\"确认\\\" style=\\\"display:none;\\\"></form><script>document.forms['alipaysubmit'].submit();</script>\",\"id\":\"d136a0ca-2be9-4ba7-b5e4-d8293e53bea1\",\"url\":\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8&payment_type=1&out_trade_no=370e678e72d04d139d1e623453f9aa1a&partner=2088711322600312&subject=dotNet自来水&_input_charset=utf-8&service=alipay.wap.create.direct.pay.by.user&total_fee=0.01&sign=045d7ede0af92f78fd334b726f171465&return_url=http://localhost:50003/return_ali_url.aspx&notify_url=http://payservice.beecloud.cn/bcpay/aliPayDynamic/c37d661d-7e61-49ea-96a5-68c34e83db3b_d136a0ca-2be9-4ba7-b5e4-d8293e53bea1.php&sign_type=MD5&seller_id=2088711322600312\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.url);
            Assert.IsNotNull(actual.html);
        }

        [Test()]
        public void handleYeePayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.YEE_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            bill.returnUrl = "http://localhost:50003/return_yee_url.aspx";
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"html\":\"<form name=\\\"yeepay\\\" action=\\\"https://www.yeepay.com/app-merchant-proxy/node\\\" method=\\\"POST\\\"><input type=\\\"hidden\\\" name=\\\"p4_Cur\\\" value=\\\"CNY\\\"/><input type=\\\"hidden\\\" name=\\\"p0_Cmd\\\" value=\\\"Buy\\\"/><input type=\\\"hidden\\\" name=\\\"p3_Amt\\\" value=\\\"0.01\\\"/><input type=\\\"hidden\\\" name=\\\"pa_MP\\\" value=\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b%3A41695744-f7f7-4926-8eee-2d5dfb745af0\\\"/><input type=\\\"hidden\\\" name=\\\"p2_Order\\\" value=\\\"9572742753bf43298eef21a1579dc9c1\\\"/><input type=\\\"hidden\\\" name=\\\"p5_Pid\\\" value=\\\"dotNet%D7%D4%C0%B4%CB%AE\\\"/><input type=\\\"hidden\\\" name=\\\"hmac\\\" value=\\\"972dd9930d6e34d959886339b6a7f349\\\"/><input type=\\\"hidden\\\" name=\\\"p8_Url\\\" value=\\\"http%3A%2F%2Flocalhost%3A50003%2Freturn_yee_url.aspx\\\"/><input type=\\\"hidden\\\" name=\\\"p1_MerId\\\" value=\\\"10012506312\\\"/></form><script>document.yeepay.submit();</script>\",\"id\":\"41695744-f7f7-4926-8eee-2d5dfb745af0\",\"url\":\"https://www.yeepay.com/app-merchant-proxy/node?p4_Cur=CNY&p0_Cmd=Buy&p3_Amt=0.01&pa_MP=c37d661d-7e61-49ea-96a5-68c34e83db3b%3A41695744-f7f7-4926-8eee-2d5dfb745af0&p2_Order=9572742753bf43298eef21a1579dc9c1&p5_Pid=dotNet%D7%D4%C0%B4%CB%AE&hmac=972dd9930d6e34d959886339b6a7f349&p8_Url=http%3A%2F%2Flocalhost%3A50003%2Freturn_yee_url.aspx&p1_MerId=10012506312\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.url);
        }

        [Test()]
        public void handleJSAPIPayResultTest()
        {
            BCBill bill = new BCBill(BCPay.PayChannel.WX_JSAPI.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"id\":\"41695744-f7f7-4926-8eee-2d5dfb745af0\",\"app_id\": \"wxa4ca6ed13385\",\"timestamp\": \"1450771041\", \"nonce_str\": \"znjsj7mw98q1drn251arrr30gq5gz0b1\", \"package\": \"prepay_id=wx20151222155721c70baa1e680187231678\", \"sign_type\": \"MD5\", \"pay_sign\": \"1E58DA3043C3D80AEC7A51B99E32\"}";
            BCBill actual = BCPay.handlePayResult(respString, bill);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.timestamp);
            Assert.IsNotNull(actual.noncestr);
            Assert.IsNotNull(actual.package);
            Assert.IsNotNull(actual.paySign);
            Assert.IsNotNull(actual.signType);
        }

        #endregion

        #region 退款
        [Test()]
        public void prepareRefundParametersTest()
        {
            BCRefund refund = new BCRefund("10000000", "20000000", 100);
            refund.channel = "WX";
            refund.optional = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            string paraString = "\"channel\":\"WX\",\"refund_no\":\"20000000\",\"bill_no\":\"10000000\",\"refund_fee\":100,\"optional\":{\"key1\":\"value1\",\"key2\":\"value2\"},\"need_approval\":false}";
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");
            string actual = BCPay.prepareRefundParameters(refund);
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handleRefundResultTest()
        {
            BCRefund refund = new BCRefund("10000000", "20000000", 100);
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"id\":\"c6bb96d8-f610-48a0-bd72-451886ed3ce9\",\"url\":\"https://mapi.alipay.com/gateway.do?_input_charset=UTF-8&app_id=c37d661d-7e61-49ea-96a5-68c34e83db3b&batch_no=20151222888bb713&batch_num=1&detail_data=2015122221001004460089671321%5E0.01%5E%E5%8D%8F%E8%AE%AE%E9%80%80%E6%AC%BE&notify_url=http%3A%2F%2Fpayservice.beecloud.cn%2Fbcpay%2FaliRefundDynamic%2Fc37d661d-7e61-49ea-96a5-68c34e83db3b_c6bb96d8-f610-48a0-bd72-451886ed3ce9.php&partner=2088711322600312&refund_date=2015-12-22+06%3A53%3A11&seller_user_id=2088711322600312&service=refund_fastpay_by_platform_pwd&sign=fYf7CmvKF4OqLIsyPcGakgck6uGNz9FIZLrywyc5lbYN6GnXu5zdBvLyIBnbjVPi2Vkz0CD9m2Lbrj%2B%2Fdvr5OznzXrpTSQu481rkLIfSobxNLWNZZG3oZaaEYmwFTHJ%2BGDRInDxAE1hQNcDyrZRDco5pzQ9P7pwRusZnrB8QgJg%3D&sign_type=RSA\",\"out_refund_no_ali_ex\":\"20151222888bb7132088711322600312\"}";
            BCRefund actual = BCPay.handleRefundResult(respString, refund);
            Assert.IsNotNull(actual.url);
        }
        #endregion

        #region 退款审核
        [Test()]
        public void prepareApproveRefundParametersTest()
        {
            string paraString = "\"channel\":\"ALI\",\"ids\":[\"001\",\"002\"],\"agree\":true,\"denyReason\":null}";
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");
            string actual = BCPay.prepareApproveRefundParameters("ALI", new List<string> { "001", "002" }, true, null);
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handleApproveRefundResultTest()
        {
            string respString = "{\"result_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"url\":\"https://mapi.alipay.com/gateway.do\",\"result_map\":{\"001\":\"OK\",\"002\":\"OK\"}}";
            BCApproveRefundResult result = BCPay.handleApproveRefundResult(respString, "ALI");
            Assert.IsNotNull(result.url);
            Assert.IsTrue(result.status["001"] == "OK");
        }
        #endregion

        #region 查询
        [Test()]
        public void preparePayQueryByConditionParametersTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");

            BCQueryBillParameter para = new BCQueryBillParameter();
            para.channel = "ALI";
            para.startTime = 1000000000000;
            para.endTime = 1000000000001;
            para.needDetail = true;
            para.result = true;
            para.skip = 10;
            para.limit = 50;

            string paraString = "\"channel\":\"ALI\",\"bill_no\":null,\"start_time\":1000000000000,\"end_time\":1000000000001,\"skip\":10,\"spay_result\":true,\"need_detail\":true,\"limit\":50}";
            string actual = BCPay.preparePayQueryByConditionParameters(para);
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handlePayQueryByConditionResultTest()
        {
            string respString = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"count\":10,\"result_code\":0,\"bills\":[{\"create_time\":1450775961675,\"channel\":\"ALI\",\"bill_no\":\"b91d4b1efbfb2c0c4ace\",\"optional\":\"{\\\"optionalKey1\\\":\\\"optionalValue1\\\",\\\"optionalKey2\\\":\\\"optionalValue2\\\"}\",\"revert_result\":false,\"title\":\"测试通过APICloud支付宝调用\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004460089671321\",\"id\":\"379f118e-d8a5-45d3-9a3f-6278a1365fb4\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_379f118e-d8a5-45d3-9a3f-6278a1365fb4\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"测试通过APICloud支付宝调用\\\",\\\"trade_no\\\":\\\"2015122221001004460089671321\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-22 17:19:37\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"b91d4b1efbfb2c0c4ace\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 17:19:37\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 17:19:37\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"340039c8939c024c7288cb7d105664ejjs\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"cYbY+l42o+SDZ7eaAZnBN2Ueo98fNgitVjz9vRzfZ32bb/jUG1rNrTN0u5Pc3jAqn0ol5NrS1Mt9YaNkqh8zjSTLURIDaEr81If3jgUd3B3ihCnWG+7oCyF32gEf7Touxu/0/lp4DIRHTUV2S484ryLGtRkI8Ww2K3XSdhqeERo=\\\"}\",\"refund_result\":false},{\"create_time\":1450774657431,\"channel\":\"ALI\",\"bill_no\":\"bc20151222165738383\",\"optional\":\"{\\\"userID\\\":\\\"张三\\\",\\\"mobile\\\":\\\"0512-86861620\\\"}\",\"revert_result\":false,\"title\":\"appcan安卓ALI_APP\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004460089613191\",\"id\":\"6d3a74d1-dd93-4d4e-97b7-3f64bf1e74d7\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_6d3a74d1-dd93-4d4e-97b7-3f64bf1e74d7\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"appcan安卓ALI_APP\\\",\\\"trade_no\\\":\\\"2015122221001004460089613191\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-22 16:57:50\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"bc20151222165738383\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 16:57:51\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 16:57:50\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"2fa18135431c2c66d2d049c83d55db4jjs\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"SzydMVmuMpQYMaIcRiiVH04tvck8utqbYdlKoPTGwLJx+6CfcuNrwClMKjnOCXZXqfNlMT4Orv32asTdXL630YrsRJvVN9cf68M97qcEgcbmURlKCDACk7HoW3gwUjo6+BZKCB5sxbgp8wm2tcChDTpHDz3V9PCqeQRdrEOQ4KQ=\\\"}\",\"refund_result\":false},{\"create_time\":1450767636326,\"channel\":\"ALI\",\"bill_no\":\"bc20151222150036920\",\"optional\":\"{\\\"userID\\\":\\\"张三\\\",\\\"mobile\\\":\\\"0512-86861620\\\"}\",\"revert_result\":false,\"title\":\"appcan安卓ALI_APP\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004460086091163\",\"id\":\"46df831c-bc03-49f8-831e-0f0fb41f2ea2\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_46df831c-bc03-49f8-831e-0f0fb41f2ea2\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"appcan安卓ALI_APP\\\",\\\"trade_no\\\":\\\"2015122221001004460086091163\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-22 15:00:46\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"bc20151222150036920\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 15:00:46\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 15:00:46\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"dbeda4341399ee4b2ab80d88960d498jjs\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"opCnPg7DwvPwehXK8ZF39mQJMzNyXjgM5mq+mIHImaSf0MMZOmoAeUYg+gjXu8aaTiYJZYJAZk7lsnevc+EUvOjyyS+MSawwAdOxYXTWsvOoq50+Pp/rUv3I8RrCX58DyzTf0vHMZf30elShcDd985DpnQU3Ffwa1MchEMEXNQI=\\\"}\",\"refund_result\":false},{\"create_time\":1450766977513,\"channel\":\"ALI\",\"bill_no\":\"20151222144938134\",\"optional\":\"{\\\"userID\\\":\\\"张三\\\",\\\"mobile\\\":\\\"0512-86861620\\\"}\",\"revert_result\":false,\"title\":\"appcan\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004400002231881\",\"id\":\"db9ab0e5-4025-4930-9406-96432895aa23\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_db9ab0e5-4025-4930-9406-96432895aa23\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"appcan\\\",\\\"trade_no\\\":\\\"2015122221001004400002231881\\\",\\\"buyer_email\\\":\\\"18874040501\\\",\\\"gmt_create\\\":\\\"2015-12-22 14:49:48\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"20151222144938134\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 14:49:49\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 14:49:49\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088802555047400\\\",\\\"notify_id\\\":\\\"5dda80e1cf8691dee19f8c079e28650j34\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"e07oTpHM4loRr5MuXXOkvfUE5/asp9DmAbz6V06kea9oSMWboFxtmUPaYZWBedFpSES/w91ihoj8gFqqL9+sr1bbw6ATkdTQ/phiyex7U5W+Hg/GMh0ArAbi9ar9Lf6d8carUK6dCknUeAJkdXPd+liaZxfbFFr5fFALHt1Bb14=\\\"}\",\"refund_result\":false},{\"create_time\":1450756919365,\"channel\":\"ALI\",\"bill_no\":\"4476d404849a2f772210\",\"optional\":\"{\\\"optionalKey1\\\":\\\"optionalValue1\\\",\\\"optionalKey2\\\":\\\"optionalValue2\\\"}\",\"revert_result\":false,\"title\":\"测试通过APICloud支付宝调用\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004460086511132\",\"id\":\"61fe8105-f119-4429-9f52-00256a092788\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_61fe8105-f119-4429-9f52-00256a092788\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"测试通过APICloud支付宝调用\\\",\\\"trade_no\\\":\\\"2015122221001004460086511132\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-22 12:02:59\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"4476d404849a2f772210\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 12:02:59\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 12:02:59\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"2d9e32358abf60246d1a52758f29be4jjs\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"f3/g4gS1JQfgjL5tQWobkWv0vg6W9ZfkqHwcrc/WSmUj9C+FHH5bjRA4LBf2eGTSzhN8TYXgf/2DYDfYuCFWO7spAogmQ0Bc5XSUeZgZt30nt5AXMy16lkSeNJ1folqEl4ez1bbLk/t3cfKpmUacoaWKe8FdAukOqgZwV1JY7/A=\\\"}\",\"refund_result\":false},{\"create_time\":1450437876517,\"channel\":\"ALI\",\"bill_no\":\"test1450437860\",\"optional\":\"{\\\"hello\\\":\\\"1\\\"}\",\"revert_result\":false,\"title\":\"test\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015121821001004880071338389\",\"id\":\"da767b91-40f8-4859-af07-5ae630a15cd4\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_da767b91-40f8-4859-af07-5ae630a15cd4\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"test\\\",\\\"trade_no\\\":\\\"2015121821001004880071338389\\\",\\\"buyer_email\\\":\\\"125204230@qq.com\\\",\\\"gmt_create\\\":\\\"2015-12-18 19:25:06\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"test1450437860\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-18 19:25:09\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-18 19:25:08\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088502728900881\\\",\\\"notify_id\\\":\\\"82874f6f24717253604c3c22dbedbb7msg\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"MD5\\\",\\\"sign\\\":\\\"ba0fd8442c517a7c002f3ca4f9fb56bb\\\"}\",\"refund_result\":false},{\"create_time\":1450420761428,\"channel\":\"ALI\",\"bill_no\":\"test1450420479\",\"optional\":\"{\\\"hello\\\":\\\"1\\\"}\",\"revert_result\":false,\"title\":\"test\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015121821001004080082376795\",\"id\":\"7c99df8a-7c58-4170-8070-acfed32c52c9\",\"sub_channel\":\"ALI_WAP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_7c99df8a-7c58-4170-8070-acfed32c52c9\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"test\\\",\\\"trade_no\\\":\\\"2015121821001004080082376795\\\",\\\"buyer_email\\\":\\\"17721259135\\\",\\\"gmt_create\\\":\\\"2015-12-18 14:40:12\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"test1450420479\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-18 14:40:13\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-18 14:40:13\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088022829508084\\\",\\\"notify_id\\\":\\\"3d7055cfb5949ee66ed974fcee57597gm8\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"MD5\\\",\\\"sign\\\":\\\"1ee8bf3c9696460e998f96cac0eab879\\\"}\",\"refund_result\":false},{\"create_time\":1450405510397,\"channel\":\"ALI\",\"bill_no\":\"efdea8f3ff554290902828e6116867b5\",\"optional\":\"{\\\"rui\\\":\\\"feng\\\"}\",\"revert_result\":false,\"title\":\"demo测试\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015121821001004610097640588\",\"id\":\"bd98ce02-94a4-4fcb-9e5e-bb82c0ae55fa\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_bd98ce02-94a4-4fcb-9e5e-bb82c0ae55fa\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"demo测试\\\",\\\"trade_no\\\":\\\"2015121821001004610097640588\\\",\\\"buyer_email\\\":\\\"13861331391\\\",\\\"gmt_create\\\":\\\"2015-12-18 10:26:18\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"efdea8f3ff554290902828e6116867b5\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-18 10:26:33\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-18 10:26:32\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088502992657614\\\",\\\"notify_id\\\":\\\"e6aeb00a0a04a583df0c53197dfcb90kpg\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"MD5\\\",\\\"sign\\\":\\\"88ee8a0ffea183685d8402c13bcd32f5\\\"}\",\"refund_result\":false},{\"create_time\":1450352111108,\"channel\":\"ALI\",\"bill_no\":\"20151217193515614\",\"optional\":\"{\\\"用途\\\":\\\"测试扫码支付\\\",\\\"testEN\\\":\\\"value恩恩\\\"}\",\"revert_result\":false,\"title\":\"安卓支付宝扫码方法支付测试\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015121721001004460078863232\",\"id\":\"cf72995f-119f-46d5-a500-fab60560420f\",\"sub_channel\":\"ALI_SCAN\",\"message_detail\":\"\",\"refund_result\":false},{\"create_time\":1450351770533,\"channel\":\"ALI\",\"bill_no\":\"20151217192935237\",\"optional\":\"{\\\"money\\\":\\\"2\\\",\\\"consumptioncode\\\":\\\"consumptionCode\\\",\\\"客户端\\\":\\\"安卓\\\"}\",\"revert_result\":false,\"title\":\"安卓支付宝支付测试\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015121700001000460065290601\",\"id\":\"3a6f84c5-3efb-4a6e-b05b-5578b5d2db2e\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_3a6f84c5-3efb-4a6e-b05b-5578b5d2db2e\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"安卓支付宝支付测试\\\",\\\"trade_no\\\":\\\"2015121700001000460065290601\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-17 19:29:43\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"20151217192935237\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-17 19:29:44\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-17 19:29:43\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"e580f768b6ac26c047d0fb0997e6d31d4k\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"LwS6F6MWk8CeAdVGAjod03tIdkIe5SMgouYAGGzLfGo/HJ9y5OKv2w4BsQ1gM2Xlx1XsnonCi7553FXDIFm/0fAUO9N6VEeq9hPSS9cPpRwZ40jaCX4m81aHbODPsjD9cZEZ6ZIS1vqaOMhRuToT7lSj9Dl1mO14Fk06agszdu4=\\\"}\",\"refund_result\":false}]}";
            List<BCBill> bills = BCPay.handlePayQueryByConditionResult(respString, true);
            Assert.IsTrue(bills.Count == 10);
            Assert.IsNotNull(bills[0].messageDetail);
        }

        [Test()]
        public void prepareQueryByIdParametersTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");

            string paraString = "{\"app_id\":\"c5d1cba1-5e3f-4ba0-941d-9b0a371fe719\",\"app_sign\":";
            string actual = BCPay.prepareQueryByIdParameters("000000001");
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handlePayQueryByIdResultTest()
        {
            string respString = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"count\":10,\"result_code\":0,\"pay\":{\"create_time\":1450775961675,\"channel\":\"ALI\",\"bill_no\":\"b91d4b1efbfb2c0c4ace\",\"optional\":\"{\\\"optionalKey1\\\":\\\"optionalValue1\\\",\\\"optionalKey2\\\":\\\"optionalValue2\\\"}\",\"revert_result\":false,\"title\":\"测试通过APICloud支付宝调用\",\"spay_result\":true,\"total_fee\":1,\"trade_no\":\"2015122221001004460089671321\",\"id\":\"379f118e-d8a5-45d3-9a3f-6278a1365fb4\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_379f118e-d8a5-45d3-9a3f-6278a1365fb4\\\",\\\"discount\\\":\\\"0.00\\\",\\\"payment_type\\\":\\\"1\\\",\\\"subject\\\":\\\"测试通过APICloud支付宝调用\\\",\\\"trade_no\\\":\\\"2015122221001004460089671321\\\",\\\"buyer_email\\\":\\\"xuanzhui@gmail.com\\\",\\\"gmt_create\\\":\\\"2015-12-22 17:19:37\\\",\\\"notify_type\\\":\\\"trade_status_sync\\\",\\\"quantity\\\":\\\"1\\\",\\\"out_trade_no\\\":\\\"b91d4b1efbfb2c0c4ace\\\",\\\"seller_id\\\":\\\"2088711322600312\\\",\\\"notify_time\\\":\\\"2015-12-22 17:19:37\\\",\\\"trade_status\\\":\\\"TRADE_SUCCESS\\\",\\\"is_total_fee_adjust\\\":\\\"N\\\",\\\"total_fee\\\":\\\"0.01\\\",\\\"gmt_payment\\\":\\\"2015-12-22 17:19:37\\\",\\\"seller_email\\\":\\\"admin@beecloud.cn\\\",\\\"price\\\":\\\"0.01\\\",\\\"buyer_id\\\":\\\"2088902348242460\\\",\\\"notify_id\\\":\\\"340039c8939c024c7288cb7d105664ejjs\\\",\\\"use_coupon\\\":\\\"N\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"sign\\\":\\\"cYbY+l42o+SDZ7eaAZnBN2Ueo98fNgitVjz9vRzfZ32bb/jUG1rNrTN0u5Pc3jAqn0ol5NrS1Mt9YaNkqh8zjSTLURIDaEr81If3jgUd3B3ihCnWG+7oCyF32gEf7Touxu/0/lp4DIRHTUV2S484ryLGtRkI8Ww2K3XSdhqeERo=\\\"}\",\"refund_result\":false}}";
            BCBill actual = BCPay.handlePayQueryByIdResult(respString);
            Assert.IsNotNull(actual.id);
            Assert.IsNotNull(actual.title);
            Assert.IsNotNull(actual.totalFee);
            Assert.IsNotNull(actual.billNo);
            Assert.IsNotNull(actual.result);
            Assert.IsNotNull(actual.channel);
            Assert.IsNotNull(actual.tradeNo);
            Assert.IsNotNull(actual.optional);
            Assert.IsNotNull(actual.messageDetail);
            Assert.IsNotNull(actual.revertResult);
            Assert.IsNotNull(actual.refundResult);
        }

        [Test()]
        public void prepareRefundQueryByConditionParametersTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");

            BCQueryRefundParameter para = new BCQueryRefundParameter();
            para.channel = "ALI";
            para.limit = 2;
            para.needDetail = true;
            para.skip = 10;
            para.startTime = 1000000000000;
            para.endTime = 1100000000000;

            string paraString = "\"channel\":\"ALI\",\"bill_no\":null,\"refund_no\":null,\"start_time\":1000000000000,\"end_time\":1100000000000,\"need_approval\":null,\"need_detail\":true,\"skip\":10,\"limit\":2}";
            string actual = BCPay.prepareRefundQueryByConditionParameters(para);
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handleRefundQueryResultTest()
        {
            string respString = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"count\":10,\"result_code\":0,\"refunds\":[{\"create_time\":1450781591026,\"refund_no\":\"20151222888bb713\",\"channel\":\"ALI\",\"bill_no\":\"b91d4b1efbfb2c0c4ace\",\"optional\":\"\",\"title\":\"测试通过APICloud支付宝调用\",\"result\":false,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"c6bb96d8-f610-48a0-bd72-451886ed3ce9\",\"sub_channel\":\"ALI_APP\",\"message_detail\":\"\"},{\"create_time\":1450255190835,\"refund_no\":\"201512168848995\",\"channel\":\"ALI\",\"bill_no\":\"cbe888e778d447eea3729f1a45bebf99\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":false,\"total_fee\":1,\"refund_fee\":1,\"finish\":false,\"id\":\"c0b60360-7621-4d4d-885d-14ca09aa2b0f\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"\"},{\"create_time\":1450186420381,\"refund_no\":\"201512153492\",\"channel\":\"ALI\",\"bill_no\":\"3727a00b5f9c431890bfea3a39a393e4\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":true,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"8a3516b5-8617-4649-a614-7c96a2f36433\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_8a3516b5-8617-4649-a614-7c96a2f36433\\\",\\\"sign\\\":\\\"gZ7PTJvzyIFqt8puriffoLvsK9qsvvKgRFZXWuMefAqpvqFtp17ot2PjeyV4ZVI1rhjHl49x3L+7OcDz/njBhZgngw/3KxNyiGgSZso2HwkufeDmWDlTrIsSyMT7/phJd+iRvbPLT4pGKenyWmgIEFDG8vJnkkDZpVuagsxALh0=\\\",\\\"result_details\\\":\\\"2015121521001004610097182199^0.01^SUCCESS\\\",\\\"notify_time\\\":\\\"2015-12-15 21:34:15\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"notify_type\\\":\\\"batch_refund_notify\\\",\\\"notify_id\\\":\\\"db2a2d9781649037cdd22ae2921818fkv0\\\",\\\"batch_no\\\":\\\"201512153492\\\",\\\"success_num\\\":\\\"1\\\"}\"},{\"create_time\":1450186393656,\"refund_no\":\"20151215128836\",\"channel\":\"ALI\",\"bill_no\":\"3727a00b5f9c431890bfea3a39a393e4\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":false,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"bfd5a98e-d6cb-48cf-834b-501ac242d5e4\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"\"},{\"create_time\":1450186393533,\"refund_no\":\"2015121550233094\",\"channel\":\"ALI\",\"bill_no\":\"3727a00b5f9c431890bfea3a39a393e4\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":false,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"442df321-5211-4da2-9dcb-b950f87d6260\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"\"},{\"create_time\":1450185414632,\"refund_no\":\"2015121536728\",\"channel\":\"ALI\",\"bill_no\":\"a78cc3c26e7747aa8463c3df1ef0fdba\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":true,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"11dacb20-a69b-4124-83ef-6738ec041e4f\",\"sub_channel\":\"ALI_WEB\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_!e4f8dbb0-832f-44d5-b4fe-b902a1ef36fc\\\",\\\"sign\\\":\\\"bFY1hmCNrT5VfNmqBAg9u9gTNf/7Mg7MwRRSfKf8iTEgKmfo6R4yzQGuRM84UEPgrmU7HHCx3cZWWpJR5DQgPatmJVaoZxFaCV4KW0FSU38/IS4mjk/ghMV23GcO7X2OZ67aTefWDG2623DfOqWTlxnNvdto6jDKo37ekHAzoyQ=\\\",\\\"result_details\\\":\\\"2015121521001004610097980535^0.01^SUCCESS#2015121500001000610063614955^0.01^SUCCESS\\\",\\\"notify_time\\\":\\\"2015-12-15 21:17:34\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"notify_type\\\":\\\"batch_refund_notify\\\",\\\"notify_id\\\":\\\"862df8a893bd443fb6d41095fb600a7neo\\\",\\\"batch_no\\\":\\\"20151215603054\\\",\\\"success_num\\\":\\\"2\\\"}\"},{\"create_time\":1450185411540,\"refund_no\":\"20151215432\",\"channel\":\"ALI\",\"bill_no\":\"214c34bdfee0428f806d1a6da8fbd9f7\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":true,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"c9662b45-e30c-4613-81d5-f384f8c909a8\",\"sub_channel\":\"ALI_WAP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_!e4f8dbb0-832f-44d5-b4fe-b902a1ef36fc\\\",\\\"sign\\\":\\\"bFY1hmCNrT5VfNmqBAg9u9gTNf/7Mg7MwRRSfKf8iTEgKmfo6R4yzQGuRM84UEPgrmU7HHCx3cZWWpJR5DQgPatmJVaoZxFaCV4KW0FSU38/IS4mjk/ghMV23GcO7X2OZ67aTefWDG2623DfOqWTlxnNvdto6jDKo37ekHAzoyQ=\\\",\\\"result_details\\\":\\\"2015121521001004610097980535^0.01^SUCCESS#2015121500001000610063614955^0.01^SUCCESS\\\",\\\"notify_time\\\":\\\"2015-12-15 21:17:34\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"notify_type\\\":\\\"batch_refund_notify\\\",\\\"notify_id\\\":\\\"862df8a893bd443fb6d41095fb600a7neo\\\",\\\"batch_no\\\":\\\"20151215603054\\\",\\\"success_num\\\":\\\"2\\\"}\"},{\"create_time\":1450183799599,\"refund_no\":\"201512154940\",\"channel\":\"ALI\",\"bill_no\":\"d1a5cadd94ea4af7b3c2b11041ca01fd\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":true,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"7ce735b5-837c-45c6-a988-871b045ce229\",\"sub_channel\":\"ALI_WAP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_!eeb8e6a8-3d4b-471e-a617-1fe49bab9759\\\",\\\"sign\\\":\\\"PenQ+mjkz2Zik5Gd1kCa2GIIwQgAL2Q5se7eHyr9gtwtcF2newRuvnnJhBTkQcvh6T7ICSPDDPU76yxF/2g9xnKWe4Wf9xFdzkApoEL971xdR40e1vAMesVQvch7FWbViSldwChZJ3XKxfBc/DkjoLVjdvqXvUcszM9lp/rY3k4=\\\",\\\"result_details\\\":\\\"2015121500001000610063614038^0.01^SUCCESS\\\",\\\"notify_time\\\":\\\"2015-12-16 21:14:34\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"notify_type\\\":\\\"batch_refund_notify\\\",\\\"notify_id\\\":\\\"72adca0dcdf772facd3829a4aa0c348ks8\\\",\\\"batch_no\\\":\\\"20151215356\\\",\\\"success_num\\\":\\\"1\\\"}\"},{\"create_time\":1450183755966,\"refund_no\":\"20151215927265\",\"channel\":\"ALI\",\"bill_no\":\"d3f762d6278c4eefbdc3b8312e944d08\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":false,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"164784ae-aa6b-432b-86b5-98b87dfbadce\",\"sub_channel\":\"ALI_WAP\",\"message_detail\":\"\"},{\"create_time\":1450183426035,\"refund_no\":\"201512151507\",\"channel\":\"ALI\",\"bill_no\":\"2a5e4426f59d41c2a9566fdfe29ee85e\",\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"result\":true,\"total_fee\":1,\"refund_fee\":1,\"finish\":true,\"id\":\"508163f4-22c5-4276-80b4-49711c07ce27\",\"sub_channel\":\"ALI_WAP\",\"message_detail\":\"{\\\"bc_appid\\\":\\\"c37d661d-7e61-49ea-96a5-68c34e83db3b_508163f4-22c5-4276-80b4-49711c07ce27\\\",\\\"sign\\\":\\\"aYrMNlTF6I6Tk75ebcu9oNBDLEzlmd0GvtqpPiSWwc5eqmnFCDt/sQC29DrHqfmt4+PHMdwhmi23bAtQkNkSgl0pbEdXkjSrTgZqB98hmKbJNTj05Syz2d0/lf2brdzQw96JlrF7/lil/h7c5ho4EejjJLWbyXB1IhAPIo+l9S8=\\\",\\\"result_details\\\":\\\"2015121500001000610063613746^0.01^SUCCESS\\\",\\\"notify_time\\\":\\\"2015-12-16 21:08:38\\\",\\\"sign_type\\\":\\\"RSA\\\",\\\"notify_type\\\":\\\"batch_refund_notify\\\",\\\"notify_id\\\":\\\"3db76a1f687f276b3a52cf8215c74cag00\\\",\\\"batch_no\\\":\\\"201512151507\\\",\\\"success_num\\\":\\\"1\\\"}\"}]}";
            List<BCRefund> refunds = BCPay.handleRefundQueryByConditionResult(respString, true);
            Assert.IsTrue(refunds.Count == 10);
        }

        [Test()]
        public void prepareRefundStatusQueryParametersTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2");

            string paraString = "\"channel\":\"WX\",\"refund_no\":\"00000001\"}";
            string actual = BCPay.prepareRefundStatusQueryParameters("WX", "00000001");
            Assert.IsTrue(actual.Contains(paraString));
        }

        [Test()]
        public void handleRefundStatusQueryResultTest()
        {
            string respString = "{\"result_msg\":\"OK\",\"refund_status\":\"FAIL\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0}";
            string actual = BCPay.handleRefundStatusQueryResult(respString);
            Assert.IsTrue(actual == "FAIL");
        }
        #endregion
    }
}
