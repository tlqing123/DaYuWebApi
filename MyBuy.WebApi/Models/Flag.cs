using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBuy.WebApi.Models
{
    public enum Flag
    {
        OnSale = 0,//上架
        InTrading = 1,//交易中
        Dealed = 2,//交易完成
        SoldOut = 3//下架
    }
}