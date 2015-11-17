
namespace BeeCloud.Model
{
    /// <summary>
    /// 支付完成结果类
    /// 父类
    /// 保存共有返回值
    /// </summary>
    public class BCPayResult
    {
        //成功发起支付后返回支付表记录唯一标识
        public string id { get; set; }

        /*
           result_code	result_msg	        含义
                    0	OK	                调用成功
                    1	APP_INVALID	        根据app_id找不到对应的APP或者app_sign不正确
                    2	PAY_FACTOR_NOT_SET	支付要素在后台没有设置
                    3	CHANNEL_INVALID	    channel参数不合法
                    4	MISS_PARAM	        缺少必填参数
                    5	PARAM_INVALID	    参数不合法
                    6	CERT_FILE_ERROR	    证书错误
                    7	CHANNEL_ERROR	    渠道内部错误
                    14	RUN_TIME_ERROR	    实时未知错误，请与技术联系帮助查看
         */
        public int resultCode { get; set; }

        //返回信息， OK为正常
        public string resultMsg { get; set; }

        //具体错误信息
        public string errDetail { get; set; }
    }
}
