using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dm.Model.V20151123;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.Common
{
    public class ALiYnEmail
    {
        public static bool SendEmail(string name, string code, string email)
        {

            string HtmlBody = string.Format("<p>Logo</p><hr/><p>Hi，{0}<br/></p><p><br/></p><p>感谢你注册<a href='http://www.dayuweb.cn' target='_self'title='Dayuweb.cn'>www.dayuweb.cn</a></p><p>您的激活码是：</p><p style='text-align: center;'><span style='font-size: 24px;color:#938953'><strong>{1}</strong></span><br/></p><p style='text-align: left;'><span style='font-size:16px;color:#000000'>该激活码有效期为2天，请你及时激活，妥善保管激活码！请勿泄露</span></p><p style='text-align: left;'><span style='font-size:16px;color:#000000'><br/></span></p><p style='text-align: left;'><span style='font-size:16px;color:#000000'>-大鱼互联团队</span></p><hr/><p style='text-align: left;'><span style='font-size:12px;color:#3f3f3f'>如果你没有注册过dayuweb.cn，请忽略&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 获取帮助：https://dayuweb.cn/help</span> </p><p style='text-align: left;'><span style='font-size: 24px;color:#938953'><span style='color: rgb(255, 255, 255);'><span style='font-size: 16px;'>gai</span></span><br/></span></p>", name, code);

            bool isSuccess = false;
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAIHWbwAjMRMrnR", "31nlB0FinmBhwMbVYz4UED8PZx6NSf");
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendMailRequest request = new SingleSendMailRequest();
            try
            {
                request.AccountName = "server@dayuweb.cn";
                request.FromAlias = "大鱼互联";
                request.AddressType = 1;
                request.TagName = "emailCode";
                request.ReplyToAddress = true;
                request.ToAddress = email;
                request.Subject = "账户激活码";
                request.HtmlBody = HtmlBody;
                SingleSendMailResponse httpResponse = client.GetAcsResponse(request);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
