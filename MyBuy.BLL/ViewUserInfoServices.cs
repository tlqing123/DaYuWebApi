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
     *用户信息视图查询业务操作类
     *xiaohsan
     *2017-5-29
     */
    public class ViewUserInfoServices : BaseServices<VI_USER_INFO>, IViewUserInfoServices
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDbSession.ViewUserInfoDal;
        }
    }
}
