using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.Common
{
    public class CreateRenderCode
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static string RenderCode()
        {
            Random random = new Random();
            string validateNum = "";
            string s = "0123456789";
            for (int i = 0; i < 5; i++)
            {
                validateNum += s[random.Next(s.Length)];
            }
            return validateNum;
        }
    }
}
