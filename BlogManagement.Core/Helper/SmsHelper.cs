using System;
using System.Collections.Generic;
using System.Text;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json;

namespace BlogManagement.Core.Helper
{
    public class SmsHelper
    {
        private static string accessKeyId = ConfigurationKey.aliyuncs_accessKeyId;
        private static string accessKeySecret = ConfigurationKey.aliyuncs_accessKeySecret;
        /// <summary>
        /// 发送（阿里云短信）
        /// </summary>
        /// <param name="mobile">手机</param>
        /// <param name="code">验证码</param>
        /// <param name="template">模版</param>
        /// <returns></returns>
        public static string SendSmsByAli(string mobile, string code, string template)
        {
            //注册验证码
            //var msg = SmsHelper.SendSmsByAli("15215105588", "123456", "SMS_177245306");
            //找回密码验证码
            //var msg = SmsHelper.SendSmsByAli("15215105588", "987654", "SMS_177245315");

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.AddQueryParameters("PhoneNumbers", mobile);
            request.AddQueryParameters("SignName", "BlogSystem");
            request.AddQueryParameters("TemplateCode", template);
            request.AddQueryParameters("TemplateParam", "{\"code\":\"" + code + "\"}");
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                return MessageHandle(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
            }
            catch (ServerException e)
            {
                LogFactory.GetLogger().Error(e.Message);
                return null;
            }
            catch (ClientException e)
            {
                LogFactory.GetLogger().Error(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 消息处理机制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string MessageHandle(string str)
        {
            SmsMessageModel message = JsonConvert.DeserializeObject<SmsMessageModel>(str);
            string result = "";
            switch (message.Code)
            {
                case "OK":
                    //短信发送成功
                    result = "OK";
                    break;
                case "isp.RAM_PERMISSION_DENY":
                    result = "RAM权限DENY";
                    break;
                case "isv.OUT_OF_SERVICE":
                    result = "业务停机";
                    break;
                case "isv.PRODUCT_UN_SUBSCRIPT":
                    result = "未开通云通信产品的阿里云客户";
                    break;
                case "isv.PRODUCT_UNSUBSCRIBE":
                    result = "产品未开通";
                    break;
                case "isv.ACCOUNT_NOT_EXISTS":
                    result = "账户不存在";
                    break;
                case "isv.ACCOUNT_ABNORMAL":
                    result = "账户异常    ";
                    break;
                case "isv.SMS_TEMPLATE_ILLEGAL":
                    result = "短信模板不合法";
                    break;
                case "isv.SMS_SIGNATURE_ILLEGAL":
                    result = "短信签名不合法";
                    break;
                case "isv.INVALID_PARAMETERS":
                    result = "参数异常";
                    break;
                case "isv.MOBILE_NUMBER_ILLEGAL":
                    result = "非法手机号";
                    break;
                case "isv.MOBILE_COUNT_OVER_LIMIT":
                    result = "手机号码数量超过限制";
                    break;
                case "isv.TEMPLATE_MISSING_PARAMETERS":
                    result = "模板缺少变量";
                    break;
                case "isv.BUSINESS_LIMIT_CONTROL":
                    result = "业务限流";
                    break;
                case "isv.INVALID_JSON_PARAM":
                    result = "JSON参数不合法，只接受字符串值";
                    break;
                case "isv.PARAM_LENGTH_LIMIT":
                    result = "参数超出长度限制";
                    break;
                case "isv.PARAM_NOT_SUPPORT_URL":
                    result = "不支持URL";
                    break;
                case "isv.AMOUNT_NOT_ENOUGH":
                    result = "账户余额不足";
                    break;
                case "isv.TEMPLATE_PARAMS_ILLEGAL":
                    result = "模板变量里包含非法关键字";
                    break;
            }
            return result;
        }

    }
    public class SmsMessageModel
    {
        public string RequestId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
