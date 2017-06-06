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
     * 商品视图的业务层
     * xiaoshan
     * 2017-5-29
     */
    public class ViewGoodsServices : BaseServices<VI_GOODSES>, IViewGoodsServices
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDbSession.ViewGoodsDal;
        }
    }
}
