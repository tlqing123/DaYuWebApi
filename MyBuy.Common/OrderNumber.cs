using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.Common
{
    /*
     *生成订单号
     *2017-6-8
     *xiaoshan
     */
    public class OrderNumber
    {
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="type">G标示商品id，O标示订单id</param>
        /// <returns></returns>
        public static string CreateOrderId(string type)
        {

            string OrderId = type.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            return OrderId;
        }
    }
}
