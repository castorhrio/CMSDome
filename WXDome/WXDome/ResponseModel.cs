using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDome
{
    public class ResponseModel
    {
        public class WeiXinAccessTokenResult
        {
            public AccessTokenModel SuccessResult { get; set; }

            public bool Result { get; set; }

            public ErrorMessage ErrorResult { get; set; }
        }

        public class AccessTokenModel
        {
            //接口调用凭证
            public string access_token { get; set; }

            //access_token接口调用凭证超时时间，单位（秒）
            public string expires_in { get; set; }

            //用户刷新access_token
            public string refresh_token { get; set; }

            //授权用户唯一标识
            public string openid { get; set; }

            //用户授权的作用域，使用逗号（,）分隔
            public string scope { get; set; }

            //当且仅当该网站应用已获得该用户的userinfo授权时，才会出现该字段。
            public string unionid { get; set; }
        }

        public class ErrorMessage
        {
            public string errcode { get; set; }

            public string errmsg { get; set; }
        }
    }
}
