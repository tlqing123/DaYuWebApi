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
    public class DbSession : IDbSession
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
        /// IGoodsDal属性
        /// </summary>
        private IGoodsDal _goodsDal;
        public IGoodsDal GoodsDal
        {
            get
            {
                if (_goodsDal == null)
                {
                    _goodsDal = AbstractFactory.CreateGoodsDal();
                }
                return _goodsDal;
            }
            set { _goodsDal = value; }
        }


        /// <summary>
        /// IViewGoodsDal属性
        /// </summary>
        private IViewGoodsDal _viewGoodsDal;
        public IViewGoodsDal ViewGoodsDal
        {
            get
            {
                if (_viewGoodsDal == null)
                {
                    _viewGoodsDal = AbstractFactory.CreateViewGoodsDal();
                }
                return _viewGoodsDal;
            }
            set { _viewGoodsDal = value; }
        }

        private IViewUserInfoDal _viewUserInfoDal;

        public IViewUserInfoDal ViewUserInfoDal
        {
            get {
                if (_viewUserInfoDal==null)
                {
                    _viewUserInfoDal = AbstractFactory.CreateViewUserInfoDal();
                }
                return _viewUserInfoDal; }
            set { _viewUserInfoDal = value; }
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
