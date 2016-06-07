using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WXDome
{
    public class WXHelper
    {
        public string GetWeiXinCode(string appId, string uri)
        {
            var state = RandomString();

            //微信用户信息授权
            string url = "https://open.weixin.qq.com/connect/qrconnect?appid="+appId+"&redirect_uri="+ HttpUtility.UrlEncode(uri) + "&response_type=code&scope=snsapi_login&state="+state+"#wechat_redirect";
            return url;
        }

        public string GetWeiXinAccessToken(string appId,string secret,string code)
        {
            string url =
                "https://api.weixin.qq.com/sns/oauth2/access_token?appid="+appId+"&secret="+secret+"&code="+code+"&grant_type=authorization_code";
            string jsonstr = WebHelper.CreateGetHttpResponse(url,null,null).ToString();

            ResponseModel.WeiXinAccessTokenResult result = new ResponseModel.WeiXinAccessTokenResult();
            if (jsonstr.Contains("errcode"))
            {
                ResponseModel.ErrorMessage errorResult = new ResponseModel.ErrorMessage();
            }

            return null;
        }

        public static string RandomString(int length = 11)
        {
            int flag;
            string str = String.Empty;

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                flag = random.Next();
                flag = flag%36;

                if (flag < 10)
                {
                    flag += 48;
                }
                else
                {
                    flag += 55;
                }

                str += ((char) flag).ToString();
            }

            return str;
        }
    }
}
