using MyBuy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.DAL
{
    /*
    * 基础用户信息操作
    * author:xiaoshan
    * date:2017/5/7
    */
    public class BaseDal<T> where T : class,new()
    {
        /*
         * 数据操作基类
         * author:xioashan
         * date:2017-5-7
         */
        MyBuyEntities db = new MyBuyEntities();
        #region 加载所有实体
        /// <summary>
        /// 加载所有实体
        /// </summary>
        /// <param name="whereLambda">lambda表达式，用于条件检索</param>
        /// <returns>返回搜索后的实体集</returns>
        public IQueryable<T> LoadEntityes(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where<T>(whereLambda);
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
            var temp = db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
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
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return true;
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
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return true;
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
            db.Set<T>().Add(entity);
            return entity;
        }
        #endregion
    }
}
