using Jose;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using MyBuy.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JWTTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string token = OAuth.CreateAccessToken("dayuweb");        
            JObject obj = OAuth.GetData(token);
            Console.WriteLine(obj["name"].ToString());
            Console.WriteLine(Convert.ToDateTime(obj["exp"]));
            Console.WriteLine(Md5.GetMd5String("123456"));
            Console.WriteLine(token);*/


            Console.WriteLine(OrderNumber.CreateOrderId("G")); 
            Console.ReadKey();

        }
    }
}
