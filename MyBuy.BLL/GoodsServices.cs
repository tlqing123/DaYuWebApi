using MyBuy.IBLL;
using MyBuy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.BLL
{
    /*
     *商品信息的业务操作类，是操作表
     * xiaoshan
     * 2017-5-28
     */
    public class GoodsServices : BaseServices<tbGoods>, IGoodsServices
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDbSession.GoodsDal;
        }
    }
}
