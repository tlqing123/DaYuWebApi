using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBuy.WebApi.Models
{
    public enum Sate
    {
        Inactive = 0,//未激活
        Activated = 1,//激活
        Freezed = 2//冻结中
    }
}