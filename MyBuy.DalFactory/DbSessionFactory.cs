using MyBuy.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.DalFactory
{
    /*
    *负责DBSession的唯一，避免重复实现
    * author:xiaoshan
    * date:2017/5/7
    */
   public class DbSessionFactory
    {
        /// <summary>
        /// DbSession(EF创建实例)线程内唯一
        /// </summary>
        /// <returns></returns>
        public static IDbSession CreateDbSession()
        {
            IDbSession dbSession = (IDbSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
