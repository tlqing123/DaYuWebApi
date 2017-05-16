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
    * 用户操作业务层
    * author:xiaoshan
    * date:2017/5/7
    */
    public class UserInfoServices:BaseServices<tbUser_Info>,IUserInfoServices
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDbSession.UserInfoDal;
        }
    }
}
