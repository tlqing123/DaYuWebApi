using MyBuy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.IBLL
{
    /*
     * 商品视图的业务层接口，仅用于查询，禁止数据操作
     * xiaoshan
     * 2017-5-29
     */
    public interface IViewGoodsServices : IBaseServices<VI_GOODSES>
    {
    }
}
