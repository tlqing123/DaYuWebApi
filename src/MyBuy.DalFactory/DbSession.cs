using MyBuy.DAL;
using MyBuy.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.DalFactory
{
    /*
     * 数据回话层，实现业务层与数据层的解耦
     * author:xioashan
     * date:2017/5/7
     */
    public class DbSession :IDbSession
    {
        /// <summary>
        /// 创建EF实体
        /// </summary>
        public DbContext Db
        {
            get
            {
                return DbContextFactory.CreateDbContext();
            }
        }

        /// <summary>
        /// IUserInfoDal属性，通过映射实现解耦
        /// </summary>
        private IUserInfoDal _userInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if (_userInfoDal == null)
                {
                    _userInfoDal = AbstractFactory.CreateUserInfoDal();
                }
                return _userInfoDal;
            }
            set { _userInfoDal = value; }
        }

        /// <summary>
        /// 工作单元模式
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
