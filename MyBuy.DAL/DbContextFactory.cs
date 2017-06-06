using MyBuy.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.DAL
{
    /*
    * EF线程唯一，负责的EF的创建
    * author:xiaoshan
    * date:2017/5/7
    */
    public class DbContextFactory
    {
        /// <summary>
        /// 封装EF在线程内唯一
        /// </summary>
        /// <returns>上下文</returns>
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new MyBuyServerEntities();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
