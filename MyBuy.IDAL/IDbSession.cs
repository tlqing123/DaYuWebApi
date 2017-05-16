using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.IDAL
{
    /*
     * DbSession的接口
     * author:xiaoshan
     * date:2017/5/7
     */
    public interface IDbSession
    {
        DbContext Db { get; }
        IUserInfoDal UserInfoDal { get; set; }
        /// <summary>
        /// 单元工厂模式
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
