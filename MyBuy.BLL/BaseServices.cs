using MyBuy.DalFactory;
using MyBuy.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.BLL
{
    /*
     * 业务层基类
     * author:xiaoshan
     * date:2017/5/7
     */
    public abstract class BaseServices<T> where T : class,new()
    {
        /// <summary>
        /// 当前的DBSession
        /// </summary>
        public IDbSession CurrentDbSession
        {
            get
            {
                return DbSessionFactory.CreateDbSession();
            }
        }
        /// <summary>
        /// 基类(当前数据层类)
        /// </summary>
        public IBaseDal<T> CurrentDal { get; set; }
        /// <summary>
        /// 虚方法
        /// </summary>
        public abstract void SetCurrentDal();
        /// <summary>
        /// 构造方法，子类在实现构造方法的时候，必须复写虚方法
        /// </summary>
        public BaseServices()
        {
            SetCurrentDal();
        }

        #region 加载所有实体
        /// <summary>
        /// 加载所有实体
        /// </summary>
        /// <param name="whereLambda">lambda表达式，用于条件检索</param>
        /// <returns>返回搜索后的实体集</returns>
        public IQueryable<T> LoadEntityes(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntityes(whereLambda);
        }
        #endregion

        #region 分页加载实体
        /// <summary>
        /// 分页加载实体
        /// </summary>
        /// <typeparam name="s">泛型数据类型</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">分页条数</param>
        /// <param name="totalCount">数据总条数</param>
        /// <param name="whereLambda">搜索数据</param>
        /// <param name="orderByLambda">分页依据</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntites<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntites<s>(pageIndex, pageSize, out totalCount, whereLambda, orderByLambda, isAsc);
        }
        #endregion

        #region 删除实体
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDbSession.SaveChanges();
        }
        #endregion

        #region 修改实体
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是否修改成功</returns>
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDbSession.SaveChanges();
        }
        #endregion

        #region 增加实体
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>修改后的实体</returns>
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDbSession.SaveChanges();
            return entity;
        }
        #endregion

    }
}
