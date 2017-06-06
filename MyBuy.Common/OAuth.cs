using Jose;
using Newtonsoft.Json.Linq;
using Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.Common
{
    public class OAuth
    {
        private static readonly byte[] x = { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };
        private static readonly byte[] y = { 131, 116, 8, 14, 22, 150, 18, 75, 24, 181, 159, 78, 90, 51, 71, 159, 214, 186, 250, 47, 207, 246, 142, 127, 54, 183, 72, 72, 253, 21, 88, 53 };
        private static readonly byte[] d = { 42, 148, 231, 48, 225, 196, 166, 201, 23, 190, 229, 199, 20, 39, 226, 70, 209, 148, 29, 70, 125, 14, 174, 66, 9, 198, 80, 251, 95, 107, 98, 206 };

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateAccessToken(string name)
        {
            var payload = new Dictionary<string, object>(){
               { "name",name},
               {"exp",DateTime.Now.AddDays(3)}
            };
            var privateKey = EccKey.New(x, y, d);
            return Jose.JWT.Encode(payload, privateKey, JwsAlgorithm.ES256);
        }

        /// <summary>
        /// 解析token内的内容
        /// </summary>
        /// <param name="accessToken">签权</param>
        /// <returns>Json对象</returns>
        public static JObject GetData(string accessToken)
        {
            var publicKey = EccKey.New(x, y, d);
            string json = Jose.JWT.Decode(accessToken, publicKey, JwsAlgorithm.ES256);
            return JObject.Parse(json);
            //string OpenId = Obj["openId"].ToString();
            //return OpenId;
        }
    }
}
